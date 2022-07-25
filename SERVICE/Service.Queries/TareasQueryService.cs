using Common.Collection;
using DATA.DTOS;
using DATA.DTOS.Updates;
using Microsoft.EntityFrameworkCore;
using PERSISTENCE;
using DATA.Extensions;
using Service.Queries.DTOS;
using Services.Common.Mapping;
using Services.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Queries
{
    public interface ITareasQueryService
    {
        Task<DataCollection<TareasDTO>> GetAllAsync(int page, int take, IEnumerable<long> Tareas = null);
        Task<TareasDTO> GetAsync(long id);
        Task<UpdateTareasDTO> PutAsync(UpdateTareasDTO Tareas, long id);
        Task<TareasDTO> DeleteAsync(long id);
    }
    
    public class TareasQueryService : ITareasQueryService
    {
        private readonly Context _context;

        public TareasQueryService(Context context)
        {
            _context = context;
        }

        public async Task<DataCollection<TareasDTO>> GetAllAsync(int page, int take, IEnumerable<long> Tareas = null)
        {
            try
            {
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

    }




}