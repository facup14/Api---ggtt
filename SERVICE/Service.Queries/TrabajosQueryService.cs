using Common.Collection;
using DATA.DTOS;
using DATA.DTOS.Updates;
using DATA.Extensions;
using Microsoft.EntityFrameworkCore;
using PERSISTENCE;
using Services.Common.Mapping;
using Services.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATA.Models;

namespace Service.Queries
{
    public interface ITrabajosQueryService
    {
        Task<DataCollection<TrabajosDTO>> GetAllAsync(int page, int take, IEnumerable<long> trabajos = null, bool order = false);
        Task<TrabajosDTO> GetAsync(long id);
        Task<UpdateTrabajoDTO> PutAsync(UpdateTrabajoDTO choferDto, long id);
        Task<TrabajosDTO> DeleteAsync(long id);
        Task<UpdateTrabajoDTO> CreateAsync(UpdateTrabajoDTO trabajo);
    }
       
    public class TrabajosQueryService:ITrabajosQueryService
    {
        private readonly Context _context;

        public TrabajosQueryService(Context context)
        {
            _context = context;
        }

        public async Task<DataCollection<TrabajosDTO>> GetAllAsync(int page, int take, IEnumerable<long> trabajos = null, bool order = false)
        {
            try
            {
                if (!order)
                {
                    var orderBy = await _context.Trabajos
                    .Where(x => trabajos == null || trabajos.Contains(x.IdTrabajo))
                    .OrderBy(x => x.IdTrabajo)
                    .GetPagedAsync(page, take);
                    return orderBy.MapTo<DataCollection<TrabajosDTO>>();
                }
                var collection = await _context.Trabajos
                .Where(x => trabajos == null || trabajos.Contains(x.IdTrabajo))
                .OrderByDescending(x => x.IdTrabajo)
                .GetPagedAsync(page, take);
                if (!collection.HasItems)
                {
                    throw new EmptyCollectionException("No se encontró ningun Item en la Base de Datos");
                }
                return collection.MapTo<DataCollection<TrabajosDTO>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<TrabajosDTO> GetAsync(long id)
        {
            try
            {
                var trabajo = await _context.Trabajos.FindAsync(id);

                if (await _context.Trabajos.FindAsync(id) == null)
                {
                    throw new EmptyCollectionException("Error al obtener el Trabajo, el Trabajo con id" + " " + id + " " + "no existe");
                }
                return trabajo.MapTo<TrabajosDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<UpdateTrabajoDTO> PutAsync(UpdateTrabajoDTO TrabajoDTO, long id)
        {
            if (TrabajoDTO.Descripcion == "")
            {
                throw new EmptyCollectionException("La descripcion del Trabajo es obligatoria");
            }
            if (await _context.Trabajos.FindAsync(id) is null)
            {
                throw new EmptyCollectionException("Error al actualizar el Trabajo, el Trabajo con id" + " " + id + " " + "no existe");
            }

            var trabajo = await _context.Trabajos.FindAsync(id);

            trabajo.Descripcion = TrabajoDTO.Descripcion;
            trabajo.Obs = TrabajoDTO.Obs;
            trabajo.TipoTrabajo = TrabajoDTO.TipoTrabajo;
            trabajo.IdRubro = TrabajoDTO.IdRubro;
    

            await _context.SaveChangesAsync();

            return TrabajoDTO.MapTo<UpdateTrabajoDTO>();
        }
        public async Task<TrabajosDTO> DeleteAsync(long id)
        {
            var trabajo = await _context.Trabajos.FindAsync(id);
            if (trabajo == null)
            {
                throw new EmptyCollectionException("Error al eliminar el Trabajo, el Trabajo con id" + " " + id + " " + "no existe");
            }

            _context.Trabajos.Remove(trabajo);

            await _context.SaveChangesAsync();
            return trabajo.MapTo<TrabajosDTO>();
        }

        public async Task<UpdateTrabajoDTO> CreateAsync(UpdateTrabajoDTO trabajo)
        {
            try
            {
                var newTrabajo = new Trabajos()
                {
                    Descripcion = trabajo.Descripcion,
                    TipoTrabajo = trabajo.TipoTrabajo,
                    IdRubro = trabajo.IdRubro,
                    Obs = trabajo.Obs,
                };
                await _context.Trabajos.AddAsync(newTrabajo);

                await _context.SaveChangesAsync();
                return newTrabajo.MapTo<UpdateTrabajoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el Trabajo");
            }

        }
    }
}
