using Common.Collection;
using DATA.DTOS;
using DATA.DTOS.Updates;
using DATA.Errors;
using DATA.Extensions;
using DATA.Models;
using PERSISTENCE;
using Services.Common.Mapping;
using Services.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Service.Queries
{
    public interface IEquipamientoQueryService
    {
        Task<DataCollection<EquipamientoDTO>> GetAllAsync(int page, int take, IEnumerable<long> equipamiento = null, bool order = false);
        Task<EquipamientoDTO> GetAsync(long id);
        Task<UpdateEquipamientoDTO> PutAsync(UpdateEquipamientoDTO equipamiento, long id);
        Task<EquipamientoDTO> DeleteAsync(long id);
        Task<GetResponse> CreateAsync(UpdateEquipamientoDTO equipamiento);
    }
    public class EquipamientoQueryService : IEquipamientoQueryService
    {
        private readonly Context _context;
        public EquipamientoQueryService(Context context)
        {
            _context = context;
        }
        public async Task<DataCollection<EquipamientoDTO>> GetAllAsync(int page, int take, IEnumerable<long> equipamiento = null, bool order = false)
        {
            try
            {
                if (!order)
                {
                    var orderBy = await _context.Equipamientos
                    .Where(x => equipamiento == null || equipamiento.Contains(x.IdEquipamiento))
                    .OrderBy(x => x.IdEquipamiento)
                    .GetPagedAsync(page, take);
                    return orderBy.MapTo<DataCollection<EquipamientoDTO>>();
                }
                var collection = await _context.Equipamientos
                .Where(x => equipamiento == null || equipamiento.Contains(x.IdEquipamiento))
                .OrderByDescending(x => x.IdEquipamiento)
                .GetPagedAsync(page, take);
                if (!collection.HasItems)
                {
                    throw new EmptyCollectionException("No se encontró ningun Item en la Base de Datos");
                }
                return collection.MapTo<DataCollection<EquipamientoDTO>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<EquipamientoDTO> GetAsync(long id)
        {
            try
            {
                var unidad = await _context.Equipamientos.FindAsync(id);

                if (await _context.Equipamientos.FindAsync(id) == null)
                {
                    throw new EmptyCollectionException("Error al obtener el Equipamiento, el Equipamiento con id" + " " + id + " " + "no existe");
                }
                return unidad.MapTo<EquipamientoDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<UpdateEquipamientoDTO> PutAsync(UpdateEquipamientoDTO equipamiento, long id)
        {
            if (await _context.Equipamientos.FindAsync(id) == null)
            {
                throw new EmptyCollectionException("Error al actualizar el Titulo, el Titulo con id" + " " + id + " " + "no existe");
            }
            var updateEquipamiento = await _context.Equipamientos.FindAsync(id);

            updateEquipamiento.idArticulo = equipamiento.idArticulo;
            updateEquipamiento.idNombreEquipamiento = equipamiento.idNombreEquipamiento;
            updateEquipamiento.Cantidad = equipamiento.Cantidad;


            await _context.SaveChangesAsync();
            
            return equipamiento.MapTo<UpdateEquipamientoDTO>();
        }
        public async Task<EquipamientoDTO> DeleteAsync(long id)
        {
            var equipamiento = await _context.Equipamientos.FindAsync(id);
            if (equipamiento == null)
            {
                throw new EmptyCollectionException("Error al eliminar el Titulo, el Titulo con id" + " " + id + " " + "no existe");
            }

            _context.Equipamientos.Remove(equipamiento);

            await _context.SaveChangesAsync();

            return equipamiento.MapTo<EquipamientoDTO>();
        }
        public async Task<GetResponse> CreateAsync(UpdateEquipamientoDTO equipamiento)
        {
            try
            {
                if (equipamiento.idNombreEquipamiento == 0)
                {
                    var ex = new EmptyCollectionException("Debe ingresar un Nombre de Equipamiento");

                    return new GetResponse()
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Message = ex.ToString(),
                        Result = null
                    };
                }
                if (equipamiento.Cantidad.ToString() == "")
                {
                    var ex = new EmptyCollectionException("Debe ingresar una Cantidad");

                    return new GetResponse()
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Message = ex.ToString(),
                        Result = null
                    };
                }
                var newEquipamiento = new Equipamientos()
                {
                    idNombreEquipamiento = equipamiento.idNombreEquipamiento,
                    idArticulo = equipamiento.idArticulo,
                    Cantidad = equipamiento.Cantidad,
                };
                await _context.Equipamientos.AddAsync(newEquipamiento);

                await _context.SaveChangesAsync();
                var nuevoEquipamiento =  newEquipamiento.MapTo<UpdateEquipamientoDTO>();

                return new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success",
                    Result = nuevoEquipamiento
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el Equipamiento");
            }

        }
    }
}
