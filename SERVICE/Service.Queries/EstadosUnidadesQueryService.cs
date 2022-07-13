using Common.Collection;
using DATA.DTOS;
using DATA.DTOS.Updates;
using Microsoft.EntityFrameworkCore;
using PERSISTENCE;
using Services.Common.Mapping;
using Services.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Queries
{
    public interface IEstadosUnidadesQueryService
    {
        Task<DataCollection<EstadosUnidadesDTO>> GetAllAsync(int page, int take, IEnumerable<long> EstadosUnidades = null);
        Task<EstadosUnidadesDTO> GetAsync(long id);
        Task<UpdateEstadoUnidadDTO> PutAsync(UpdateEstadoUnidadDTO EstadosUnidades, long it);
        Task<EstadosUnidadesDTO> DeleteAsync(long id);
    }

    public class EstadosUnidadesQueryService : IEstadosUnidadesQueryService
    {
        private readonly Context _context;

        public EstadosUnidadesQueryService(Context context)
        {
            _context = context;
        }

        public async Task<DataCollection<EstadosUnidadesDTO>> GetAllAsync(int page, int take, IEnumerable<long> estadosunidades = null)
        {
            try
            {
                var collection = await _context.EstadosUnidades
                .Where(x => estadosunidades == null || estadosunidades.Contains(x.IdEstadoUnidad))
                .OrderByDescending(x => x.IdEstadoUnidad)
                .GetPagedAsync(page, take);

                return collection.MapTo<DataCollection<EstadosUnidadesDTO>>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los estados");
            }

        }

        public async Task<EstadosUnidadesDTO> GetAsync(long id)
        {
            try
            {
                return (await _context.EstadosUnidades.SingleAsync(x => x.IdEstadoUnidad == id)).MapTo<EstadosUnidadesDTO>();
            }
            catch (Exception ex)
            {
                if (_context.EstadosUnidades.SingleAsync(x => x.IdEstadoUnidad == id) == null)
                {
                    throw new Exception("Error al obtener el estados, la estado con id" + " " + id + " " + "no existe");
                }
                throw new Exception("Error al obtener el estado");
            }

        }
        public async Task<UpdateEstadoUnidadDTO> PutAsync(UpdateEstadoUnidadDTO EstadoUnidad, long id)
        {
            if (_context.EstadosUnidades.SingleAsync(x => x.IdEstadoUnidad == id).Result == null)
            {
                throw new Exception("El estado con id" + " " + id + " " + ",no existe");
            }
            var estadounidad = await _context.EstadosUnidades.SingleAsync(x => x.IdEstadoUnidad == id);
            estadounidad.Estado = EstadoUnidad.Estado;
            estadounidad.Obs = EstadoUnidad.Obs;

            await _context.SaveChangesAsync();

            return EstadoUnidad.MapTo<UpdateEstadoUnidadDTO>();
        }
        public async Task<EstadosUnidadesDTO> DeleteAsync(long id)
        {
            try
            {
                var estadounidad = await _context.EstadosUnidades.SingleAsync(x => x.IdEstadoUnidad == id);
                _context.EstadosUnidades.Remove(estadounidad);
                await _context.SaveChangesAsync();
                return estadounidad.MapTo<EstadosUnidadesDTO>();
            }
            catch (Exception ex)
            {
                if (_context.EstadosUnidades.SingleAsync(x => x.IdEstadoUnidad == id) == null)
                {
                    throw new Exception("Error al eliminar estado, el estado con id" + " " + id + " " + "no existe");
                }
                throw new Exception("Error al eliminar el estado");
            }

        }

    }




}