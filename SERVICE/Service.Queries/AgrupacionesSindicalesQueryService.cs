using Common.Collection;
using Microsoft.EntityFrameworkCore;
using PERSISTENCE;
using DATA.DTOS;
using DATA.DTOS.Updates;
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
    public interface IAgrupacionesSindicalesQueryService
    {
        Task<DataCollection<AgrupacionesSindicalesDTO>> GetAllAsync(int page, int take, IEnumerable<int> AgrupacionesSindicales = null);
        Task<AgrupacionesSindicalesDTO> GetAsync(int id);
        Task<UpdateAgrupacionSindicalDTO> PutAsync(UpdateAgrupacionSindicalDTO AgrupacionSindical, int it);
        Task<AgrupacionesSindicalesDTO> DeleteAsync(int id);
    }

    public class AgrupacionesSindicalesQueryService : IAgrupacionesSindicalesQueryService
    {
        private readonly Context _context;

        public AgrupacionesSindicalesQueryService(Context context)
        {
            _context = context;
        }

        public async Task<DataCollection<AgrupacionesSindicalesDTO>> GetAllAsync(int page, int take, IEnumerable<int> agrupaciones = null)
        {
            try
            {
                var collection = await _context.AgrupacionesSindicales
                .Where(x => agrupaciones == null || agrupaciones.Contains(x.IdAgrupacionSindical))
                .OrderByDescending(x => x.IdAgrupacionSindical)
                .GetPagedAsync(page, take);

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
                return (await _context.AgrupacionesSindicales.SingleAsync(x => x.IdAgrupacionSindical == id)).MapTo<AgrupacionesSindicalesDTO>();
            }
            catch (Exception ex)
            {
                if (_context.AgrupacionesSindicales.SingleAsync(x => x.IdAgrupacionSindical == id) == null)
                {
                    throw new Exception("Error al obtener la Agrupacion Sindical, la Agrupacion Sindical con id" + " " + id + " " + "no existe");
                }
                throw new Exception("Error al obtener la Agrupacion Sindical");
            }

        }
        public async Task<UpdateAgrupacionSindicalDTO> PutAsync(UpdateAgrupacionSindicalDTO AgrupacionSindical, int id)
        {
            if (_context.AgrupacionesSindicales.SingleAsync(x => x.IdAgrupacionSindical == id).Result == null)
            {
                throw new Exception("La Agrupacion Sindical con id" + " " + id + " " + ",no existe");
            }
            var agrupacion = await _context.AgrupacionesSindicales.SingleAsync(x => x.IdAgrupacionSindical == id);
            agrupacion.Descripcion = AgrupacionSindical.Descripcion;
            agrupacion.Obs = AgrupacionSindical.Obs;

            await _context.SaveChangesAsync();

            return AgrupacionSindical.MapTo<UpdateAgrupacionSindicalDTO>();
        }
        public async Task<AgrupacionesSindicalesDTO> DeleteAsync(int id)
        {
            try
            {
                var agrupacion = await _context.AgrupacionesSindicales.SingleAsync(x => x.IdAgrupacionSindical == id);
                _context.AgrupacionesSindicales.Remove(agrupacion);
                await _context.SaveChangesAsync();
                return agrupacion.MapTo<AgrupacionesSindicalesDTO>();
            }
            catch (Exception ex)
            {
                if (_context.AgrupacionesSindicales.SingleAsync(x => x.IdAgrupacionSindical == id) == null)
                {
                    throw new Exception("Error al eliminar la Agrupacion Sindical, la Agupación Sindical con id" + " " + id + " " + "no existe");
                }
                throw new Exception("Error al eliminar la Agrupacion Sindical");
            }

        }

    }




}