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
    public interface ICallesQueryService
    {
        Task<DataCollection<CallesDTO>> GetAllAsync(int page, int take, IEnumerable<int> Calle = null);
        Task<CallesDTO> GetAsync(int id);
        Task<UpdateCallesDTO> PutAsync(UpdateCallesDTO Calle, int it);
        Task<CallesDTO> DeleteAsync(int id);
        Task<UpdateCallesDTO> CreateAsync(UpdateCallesDTO calle);
    }
    public class CallesQueryService : ICallesQueryService
    {
        private readonly Context _context;

        public CallesQueryService(Context context)
        {
            _context = context;
        }

        public async Task<DataCollection<CallesDTO>> GetAllAsync(int page, int take, IEnumerable<int> Calle = null)
        {
            try
            {
                var collection = await _context.Calles
                .Where(x => Calle == null || Calle.Contains(x.IdCalle))
                .OrderByDescending(x => x.IdCalle)
                .GetPagedAsync(page, take);
                if (!collection.HasItems)
                {
                    throw new EmptyCollectionException("No se encontró ningun Item en la Base de Datos");
                }
                return collection.MapTo<DataCollection<CallesDTO>>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las Calles");
            }

        }

        public async Task<CallesDTO> GetAsync(int id)
        {
            try
            {
                if (await _context.Calles.FindAsync(id) == null)
                {
                    throw new EmptyCollectionException("Error al obtener la Calle, la Calle con id" + " " + id + " " + "no existe");
                }
                return (await _context.Calles.SingleAsync(x => x.IdCalle == id)).MapTo<CallesDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<UpdateCallesDTO> PutAsync(UpdateCallesDTO Calle, int id)
        {
            if (await _context.Calles.FindAsync(id) == null)
            {
                throw new EmptyCollectionException("Error al actualizar la Calle, la Calle con id" + " " + id + " " + "no existe");
            }
            var calle = await _context.Calles.FindAsync(id);

            calle.Calle = Calle.Calle;
            calle.Obs = Calle.Obs;


            await _context.SaveChangesAsync();

            return Calle.MapTo<UpdateCallesDTO>();
        }
        public async Task<CallesDTO> DeleteAsync(int id)
        {
            try
            {
                var calle = await _context.Calles.FindAsync(id);
                if (calle == null)
                {
                    throw new EmptyCollectionException("Error al eliminar la Calle, la Calle con id" + " " + id + " " + "no existe");
                }
                _context.Calles.Remove(calle);

                await _context.SaveChangesAsync();

                return calle.MapTo<CallesDTO>();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public async Task<UpdateCallesDTO> CreateAsync(UpdateCallesDTO calle)
        {
            try
            {
                var newCalle = new Calles()
                {
                    Calle = calle.Calle,
                    Obs = calle.Obs,
                    
                };
                await _context.Calles.AddAsync(newCalle);

                await _context.SaveChangesAsync();
                return newCalle.MapTo<UpdateCallesDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la Calle");
            }

        }
    }
}
