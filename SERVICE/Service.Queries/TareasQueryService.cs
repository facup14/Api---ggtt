using Common.Collection;
using Microsoft.EntityFrameworkCore;
using PERSISTENCE;
using DATA.Extensions;
using Services.Common.Mapping;
using Services.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DATA.DTOS;
using DATA.DTOS.Updates;
using DATA.Models;
using DATA.Errors;
using System.Net;

namespace Service.Queries
{
    public interface ITareasQueryService
    {
        Task<DataCollection<TareasDTO>> GetAllAsync(int page, int take, IEnumerable<long> Tareas = null, bool order = false);
        Task<TareasDTO> GetAsync(long id);
        Task<UpdateTareasDTO> PutAsync(UpdateTareasDTO Tareas, long id);
        Task<TareasDTO> DeleteAsync(long id);
        Task<GetResponse> CreateAsync(UpdateTareasDTO tarea);
    }
    
    public class TareasQueryService : ITareasQueryService
    {
        private readonly Context _context;

        public TareasQueryService(Context context)
        {
            _context = context;
        }

        public async Task<DataCollection<TareasDTO>> GetAllAsync(int page, int take, IEnumerable<long> Tareas = null, bool order = false)
        {
            try
            {
                if (!order)
                {
                    var orderBy = await _context.Tareas
                    .Where(x => Tareas == null || Tareas.Contains(x.IdTarea))
                    .OrderBy(x => x.IdTarea)
                    .GetPagedAsync(page, take);
                    return orderBy.MapTo<DataCollection<TareasDTO>>();
                }
                var collection = await _context.Tareas
                .Where(x => Tareas == null || Tareas.Contains(x.IdTarea))
                .OrderByDescending(x => x.IdTarea)
                .GetPagedAsync(page, take);
                if (!collection.HasItems)
                {
                    throw new EmptyCollectionException("No se encontró ningun Item en la Base de Datos");
                }
                return collection.MapTo<DataCollection<TareasDTO>>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las Agrupaciones Sindicales");
            }

        }

        public async Task<TareasDTO> GetAsync(long id)
        {
            try
            {
                if (await _context.Tareas.FindAsync(id) == null)
                {
                    throw new EmptyCollectionException("Error al obtener la Tarea, la Tarea con id" + " " + id + " " + "no existe");
                }
                return (await _context.Tareas.SingleAsync(x => x.IdTarea == id)).MapTo<TareasDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<UpdateTareasDTO> PutAsync(UpdateTareasDTO Tareas, long id)
        {
            if (await _context.Tareas.FindAsync(id) == null)
            {
                throw new EmptyCollectionException("Error al actualizar la Tarea, la Tarea con id" + " " + id + " " + "no existe");
            }
            var tareas = await _context.Tareas.FindAsync(id);

            tareas.Descripcion = Tareas.Descripcion;
            tareas.Obs = Tareas.Obs;


            await _context.SaveChangesAsync();

            return Tareas.MapTo<UpdateTareasDTO>();
        }
        public async Task<TareasDTO> DeleteAsync(long id)
        {
            try
            {
                var tareas = await _context.Tareas.FindAsync(id);
                if (tareas == null)
                {
                    throw new EmptyCollectionException("Error al eliminar la Tarea, la Tarea con id" + " " + id + " " + "no existe");
                }
                _context.Tareas.Remove(tareas);
                
                await _context.SaveChangesAsync();
                
                return tareas.MapTo<TareasDTO>();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public async Task<GetResponse> CreateAsync(UpdateTareasDTO tarea)
        {
            try
            {
                if (tarea.Descripcion is null || tarea.Descripcion == "")
                {
                    var ex = new EmptyCollectionException("Debe ingresar la Descripción");

                    return new GetResponse()
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Message = ex.ToString(),
                        Result = null
                    };
                }
                var newTarea = new Tareas()
                {
                    Descripcion = tarea.Descripcion,
                    Obs = tarea.Obs,
                };
                await _context.Tareas.AddAsync(newTarea);

                await _context.SaveChangesAsync();
                var nuevaTarea =  newTarea.MapTo<UpdateTareasDTO>();

                return new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success",
                    Result = nuevaTarea
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la Tarea");
            }

        }

    }




}