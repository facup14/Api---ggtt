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
    public interface ICentrodeCostoQueryService
    {
        Task<DataCollection<CentrodeCostoDTO>> GetAllAsync(int page, int take, IEnumerable<long> CentrodeCosto = null);
        Task<CentrodeCostoDTO> GetAsync(long id);
        Task<UpdateCentrodeCostoDTO> PutAsync(UpdateCentrodeCostoDTO CentrodeCosto, long it);
        Task<CentrodeCostoDTO> DeleteAsync(long id);
    }
    public class CentrodeCostoQueryService : ICentrodeCostoQueryService
    {

        private readonly Context _context;

        public CentrodeCostoQueryService(Context context)
        {
            _context = context;
        }

        public async Task<DataCollection<CentrodeCostoDTO>> GetAllAsync(int page, int take, IEnumerable<long> centros = null)
        {
            try
            {
                var collection = await _context.CentrodeCosto
                .Where(x => centros == null || centros.Contains(x.idCentrodeCosto))
                .OrderByDescending(x => x.idCentrodeCosto)
                .GetPagedAsync(page, take);

                return collection.MapTo<DataCollection<CentrodeCostoDTO>>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los Centro de Costo");
            }

        }

        public async Task<CentrodeCostoDTO> GetAsync(long id)
        {
            try
            {
                return (await _context.CentrodeCosto.SingleAsync(x => x.idCentrodeCosto == id)).MapTo<CentrodeCostoDTO>();
            }
            catch (Exception ex)
            {
                if (_context.CentrodeCosto.SingleAsync(x => x.idCentrodeCosto == id).Result == null)
                {
                    throw new Exception("Error al obtener le centro de costo, le centro de costo con id" + " " + id + " " + "no existe");
                }
                throw new Exception("Error al obtener el centro de costo");
            }

        }
        public async Task<UpdateCentrodeCostoDTO> PutAsync(UpdateCentrodeCostoDTO CentrodeCosto, long id)
        {
            if (_context.CentrodeCosto.SingleAsync(x => x.idCentrodeCosto == id).Result == null)
            {
                throw new Exception("El centro de costo con id" + " " + id + " " + ",no existe");
            }
            var centro = await _context.CentrodeCosto.SingleAsync(x => x.idCentrodeCosto == id);
            centro.CentroDeCosto = CentrodeCosto.CentrodeCosto;
            centro.Tipo = CentrodeCosto.Tipo;
            centro.CodigoBas = CentrodeCosto.CodigoBas;
            centro.Obs = CentrodeCosto.Obs;
            centro.idEstadoUnidad = CentrodeCosto.idEstadoUnidad;

            await _context.SaveChangesAsync();

            return CentrodeCosto.MapTo<UpdateCentrodeCostoDTO>();
        }
        public async Task<CentrodeCostoDTO> DeleteAsync(long id)
        {
            try
            {
                var centro = await _context.CentrodeCosto.SingleAsync(x => x.idCentrodeCosto == id);
                _context.CentrodeCosto.Remove(centro);
                await _context.SaveChangesAsync();
                return centro.MapTo<CentrodeCostoDTO>();
            }
            catch (Exception ex)
            {
                if (_context.CentrodeCosto.SingleAsync(x => x.idCentrodeCosto == id).Result == null)
                {
                    throw new Exception("Error al eliminar en centro de costo, el centro de costo con id" + " " + id + " " + "no existe");
                }
                throw new Exception("Error al eliminar el centro de costo");
            }

        }

    }
}