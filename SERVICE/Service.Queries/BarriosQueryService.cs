using Common.Collection;
using DATA.DTOS;
using DATA.DTOS.Updates;
using DATA.Extensions;
using DATA.Models;
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
    public interface IBarriosQueryService
    {
        Task<DataCollection<BarriosDTO>> GetAllAsync(int page, int take, IEnumerable<int> Barrio = null);
        Task<BarriosDTO> GetAsync(int id);
        Task<UpdateBarrioDTO> PutAsync(UpdateBarrioDTO Barrio, int it);
        Task<BarriosDTO> DeleteAsync(int id);
        Task<UpdateBarrioDTO> CreateAsync(UpdateBarrioDTO barrio);
    }
    public class BarriosQueryService : IBarriosQueryService
    {
        private readonly Context _context;

        public BarriosQueryService(Context context)
        {
            _context = context;
        }

        public async Task<DataCollection<BarriosDTO>> GetAllAsync(int page, int take, IEnumerable<int> Barrio = null)
        {
            try
            {
                var collection = await _context.Barrios
                .Where(x => Barrio == null || Barrio.Contains(x.IdBarrio))
                .OrderByDescending(x => x.IdBarrio)
                .GetPagedAsync(page, take);
                if (!collection.HasItems)
                {
                    throw new EmptyCollectionException("No se encontró ningun Item en la Base de Datos");
                }
                return collection.MapTo<DataCollection<BarriosDTO>>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los Barrios");
            }

        }

        public async Task<BarriosDTO> GetAsync(int id)
        {
            try
            {
                if (await _context.Barrios.FindAsync(id) == null)
                {
                    throw new EmptyCollectionException("Error al obtener el Barrio, el Barrio con id" + " " + id + " " + "no existe");
                }
                return (await _context.Barrios.SingleAsync(x => x.IdBarrio == id)).MapTo<BarriosDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<UpdateBarrioDTO> PutAsync(UpdateBarrioDTO Barrio, int id)
        {
            if (await _context.Barrios.FindAsync(id) == null)
            {
                throw new EmptyCollectionException("Error al actualizar el Barrio, el Barrio con id" + " " + id + " " + "no existe");
            }
            var barrio = await _context.Barrios.FindAsync(id);

            barrio.Barrio = Barrio.Barrio;
            barrio.Obs = Barrio.Obs;
            barrio.IdLocalidad = Barrio.IdLocalidad;
            

            await _context.SaveChangesAsync();

            return Barrio.MapTo<UpdateBarrioDTO>();
        }
        public async Task<BarriosDTO> DeleteAsync(int id)
        {
            try
            {
                var barrio = await _context.Barrios.FindAsync(id);
                if (barrio == null)
                {
                    throw new EmptyCollectionException("Error al eliminar el Barrio, el Barrio con id" + " " + id + " " + "no existe");
                }
                _context.Barrios.Remove(barrio);

                await _context.SaveChangesAsync();

                return barrio.MapTo<BarriosDTO>();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public async Task<UpdateBarrioDTO> CreateAsync(UpdateBarrioDTO barrio)
        {
            try
            {
                var newBarrio = new Barrios()
                {
                    Barrio = barrio.Barrio,
                    Obs = barrio.Obs,
                    IdLocalidad = barrio.IdLocalidad,
                    
                };
                await _context.Barrios.AddAsync(newBarrio);

                await _context.SaveChangesAsync();
                return newBarrio.MapTo<UpdateBarrioDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la Agrupación");
            }

        }
    }
}
