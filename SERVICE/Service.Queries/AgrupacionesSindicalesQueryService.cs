using DATA.DTOS;
using DATA.DTOS.Updates;
using PERSISTENCE;
using DATA.Extensions;
using Services.Common.Mapping;
using Services.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DATA.Models;
using Common.Collection;

namespace Service.Queries
{
    public interface IAgrupacionesSindicalesQueryService
    {
        Task<DataCollection<AgrupacionesSindicalesDTO>> GetAllAsync(int page, int take, IEnumerable<int> AgrupacionesSindicales = null, bool order = false);
        Task<AgrupacionesSindicalesDTO> GetAsync(int id);
        Task<UpdateAgrupacionSindicalDTO> PutAsync(UpdateAgrupacionSindicalDTO AgrupacionSindical, int it);
        Task<AgrupacionesSindicalesDTO> DeleteAsync(int id);
        Task<UpdateAgrupacionSindicalDTO> CreateAsync(UpdateAgrupacionSindicalDTO agrupacion);
    }

    public class AgrupacionesSindicalesQueryService : IAgrupacionesSindicalesQueryService
    {
        private readonly Context _context;

        public AgrupacionesSindicalesQueryService(Context context)
        {
            _context = context;
        }

        public async Task<DataCollection<AgrupacionesSindicalesDTO>> GetAllAsync(int page, int take, IEnumerable<int> agrupaciones = null, bool order = false)
        {
            try
            {
                if (!order)
                {
                    var orderBy = await _context.AgrupacionesSindicales
                    .Where(x => agrupaciones == null || agrupaciones.Contains(x.IdAgrupacionSindical))
                    .OrderBy(x => x.IdAgrupacionSindical)
                    .GetPagedAsync(page, take);
                    return orderBy.MapTo<DataCollection<AgrupacionesSindicalesDTO>>();
                }
                var collection = await _context.AgrupacionesSindicales
                .Where(x => agrupaciones == null || agrupaciones.Contains(x.IdAgrupacionSindical))
                .OrderByDescending(x => x.IdAgrupacionSindical)
                .GetPagedAsync(page, take);
                if (!collection.HasItems)
                {
                    throw new EmptyCollectionException("No se encontró ningun Item en la Base de Datos");
                }
                return collection.MapTo<DataCollection<AgrupacionesSindicalesDTO>>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las Agrupaciones Sindicales");
            }

        }

        public async Task<AgrupacionesSindicalesDTO> GetAsync(int id)
        {
            try
            {
                if (await _context.AgrupacionesSindicales.FindAsync(id) == null)
                {
                    throw new EmptyCollectionException("Error al obtener la Agrupacion Sindical, la Agrupacion Sindical con id" + " " + id + " " + "no existe");
                }
                return (await _context.AgrupacionesSindicales.SingleAsync(x => x.IdAgrupacionSindical == id)).MapTo<AgrupacionesSindicalesDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<UpdateAgrupacionSindicalDTO> PutAsync(UpdateAgrupacionSindicalDTO AgrupacionSindical, int id)
        {
            if (await _context.AgrupacionesSindicales.FindAsync(id) == null)
            {
                throw new EmptyCollectionException("Error al actualizar la Agrupacion Sindical, la Agrupacion Sindical con id" + " " + id + " " + "no existe");
            }
            var agrupacion = await _context.AgrupacionesSindicales.FindAsync(id);
            
            agrupacion.Descripcion = AgrupacionSindical.Descripcion;
            agrupacion.Obs = AgrupacionSindical.Obs;

            await _context.SaveChangesAsync();

            return AgrupacionSindical.MapTo<UpdateAgrupacionSindicalDTO>();
        }
        public async Task<AgrupacionesSindicalesDTO> DeleteAsync(int id)
        {
            try
            {
                var agrupacion = await _context.AgrupacionesSindicales.FindAsync(id);
                if (agrupacion == null)
                {
                    throw new EmptyCollectionException("Error al eliminar la Agrupacion Sindical, la Agrupacion Sindical con id" + " " + id + " " + "no existe");
                }
                _context.AgrupacionesSindicales.Remove(agrupacion);
                
                await _context.SaveChangesAsync();
                
                return agrupacion.MapTo<AgrupacionesSindicalesDTO>();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public async Task<UpdateAgrupacionSindicalDTO> CreateAsync(UpdateAgrupacionSindicalDTO agrupacion)
        {
            try
            {
                var newAgrupacion = new AgrupacionesSindicales()
                {
                    Descripcion = agrupacion.Descripcion,
                    Obs = agrupacion.Obs,
                };
                await _context.AgrupacionesSindicales.AddAsync(newAgrupacion);

                await _context.SaveChangesAsync();
                return newAgrupacion.MapTo<UpdateAgrupacionSindicalDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la Agrupación");
            }

        }

    }




}