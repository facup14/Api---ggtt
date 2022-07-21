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
    public interface ICentrodeCostoQueryService
    {
        Task<DataCollection<CentrodeCostoDTO>> GetAllAsync(int page, int take, IEnumerable<long> CentrodeCosto = null);
        Task<CentrodeCostoDTO> GetAsync(long id);
        Task<UpdateCentrodeCostoDTO> PutAsync(UpdateCentrodeCostoDTO CentrodeCosto, long it);
        Task<CentrodeCostoDTO> DeleteAsync(long id);
        Task<UpdateCentrodeCostoDTO> CreateAsync(UpdateCentrodeCostoDTO centroDeCosto);
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
                var collection = await _context.CentroDeCosto
                .Where(x => centros == null || centros.Contains(x.idCentrodeCosto))
                .OrderByDescending(x => x.idCentrodeCosto)
                .GetPagedAsync(page, take);
                if (!collection.HasItems)
                {
                    throw new EmptyCollectionException("No se encontró ningun Item en la Base de Datos");
                }
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
                if (await _context.CentroDeCosto.FindAsync(id) == null)
                {
                    throw new EmptyCollectionException("Error al obtener el Centro de Costo, el Centro de Costo con id" + " " + id + " " + "no existe");
                }
                return (await _context.CentroDeCosto.FindAsync(id)).MapTo<CentrodeCostoDTO>();
            }
            catch(Exception ex)
            {
                throw new Exception("Error al obtener el Centro de Costo");
            }

        }
        public async Task<UpdateCentrodeCostoDTO> PutAsync(UpdateCentrodeCostoDTO CentrodeCosto, long id)
        {
            if (await _context.CentroDeCosto.FindAsync(id) == null)
            {
                throw new EmptyCollectionException("Error al actualizar el Centro de Costo, el Centro de Costo con id" + " " + id + " " + "no existe");
            }
            var centro = await _context.CentroDeCosto.FindAsync(id);
            centro.CentrodeCosto = CentrodeCosto.CentrodeCosto;
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
                var centro = await _context.CentroDeCosto.FindAsync(id);
                if (centro == null)
                {
                    throw new EmptyCollectionException("Error al eliminar el Centro de Costo, el Centro de Costo con id" + " " + id + " " + "no existe");
                }
                _context.CentroDeCosto.Remove(centro);
                await _context.SaveChangesAsync();
                return centro.MapTo<CentrodeCostoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el centro de costo");
            }

        }
        public async Task<UpdateCentrodeCostoDTO> CreateAsync(UpdateCentrodeCostoDTO centroDeCosto)
        {
            try
            {
                var newCentrodeCosto = new CentroDeCosto()
                {
                    CentrodeCosto = centroDeCosto.CentrodeCosto,
                    Obs = centroDeCosto.Obs,
                    Tipo = centroDeCosto.Tipo,
                    idEstadoUnidad = centroDeCosto.idEstadoUnidad,
                    CodigoBas = centroDeCosto.CodigoBas
                };
                await _context.CentroDeCosto.AddAsync(newCentrodeCosto);

                await _context.SaveChangesAsync();
                return newCentrodeCosto.MapTo<UpdateCentrodeCostoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la Agrupación");
            }

        }

    }
}