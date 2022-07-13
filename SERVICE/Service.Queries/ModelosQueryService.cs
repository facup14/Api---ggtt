using Common.Collection;
using Microsoft.EntityFrameworkCore;
using PERSISTENCE;
using DATA.DTOS;
using DATA.DTOS.Updates;
using Service.Queries.DTOS;
using Services.Common.Mapping;
using Services.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Queries
{
    public interface IModelosQueryService
    {
        Task<DataCollection<ModelosDTO>> GetAllAsync(int page, int take, IEnumerable<long> Modelos = null);
        Task<ModelosDTO> GetAsync(long id);
        Task<UpdateModeloDTO> PutAsync(UpdateModeloDTO Modelos, long it);
        Task<ModelosDTO> DeleteAsync(long id);
    }

    public class ModelosQueryService : IModelosQueryService
    {
        private readonly Context _context;

        public ModelosQueryService(Context context)
        {
            _context = context;
        }

        public async Task<DataCollection<ModelosDTO>> GetAllAsync(int page, int take, IEnumerable<long> modelos = null)
        {
            try
            {
                var collection = await _context.Modelos
                .Where(x => modelos == null || modelos.Contains(x.IdModelo))
                .OrderByDescending(x => x.IdModelo)
                .GetPagedAsync(page, take);

                return collection.MapTo<DataCollection<ModelosDTO>>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los modelos");
            }

        }

        public async Task<ModelosDTO> GetAsync(long id)
        {
            try
            {
                return (await _context.Modelos.SingleAsync(x => x.IdModelo == id)).MapTo<ModelosDTO>();
            }
            catch (Exception ex)
            {
                if (_context.Modelos.SingleAsync(x => x.IdModelo == id) == null)
                {
                    throw new Exception("Error al obtener modelos, el modelo  con id" + " " + id + " " + "no existe");
                }
                throw new Exception("Error al obtener el modelo");
            }

        }
        public async Task<UpdateModeloDTO> PutAsync(UpdateModeloDTO Modelo, long id)
        {
            if (_context.Modelos.SingleAsync(x => x.IdModelo == id).Result == null)
            {
                throw new Exception("El modelo con id" + " " + id + " " + ",no existe");
            }
            var modelo = await _context.Modelos.SingleAsync(x => x.IdModelo == id);
            modelo.Modelo = Modelo.Modelo;
            modelo.Obs = Modelo.Obs;

            await _context.SaveChangesAsync();

            return Modelo.MapTo<UpdateModeloDTO>();
        }
        public async Task<ModelosDTO> DeleteAsync(long id)
        {
            try
            {
                var modelo = await _context.Modelos.SingleAsync(x => x.IdModelo == id);
                _context.Modelos.Remove(modelo);
                await _context.SaveChangesAsync();
                return modelo.MapTo<ModelosDTO>();
            }
            catch (Exception ex)
            {
                if (_context.Modelos.SingleAsync(x => x.IdModelo == id) == null)
                {
                    throw new Exception("Error al eliminar el modelo, el modelo con id" + " " + id + " " + "no existe");
                }
                throw new Exception("Error al eliminar el modelo");
            }

        }

    }

}


