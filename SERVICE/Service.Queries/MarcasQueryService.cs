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
    public interface IMarcasQueryService
    {
        Task<DataCollection<MarcasDTO>> GetAllAsync(int page, int take, IEnumerable<long> Marcas = null);
        Task<MarcasDTO> GetAsync(long id);
        Task<UpdateMarcaDTO> PutAsync(UpdateMarcaDTO Convenio, long it);
        Task<MarcasDTO> DeleteAsync(long id);
    }

    public class MarcasQueryService : IMarcasQueryService
    {
        private readonly Context _context;

        public MarcasQueryService(Context context)
        {
            _context = context;
        }

        public async Task<DataCollection<MarcasDTO>> GetAllAsync(int page, int take, IEnumerable<long> marcas = null)
        {
            try
            {
                var collection = await _context.Marcas
                .Where(x => marcas == null || marcas.Contains(x.IdMarca))
                .OrderByDescending(x => x.IdMarca)
                .GetPagedAsync(page, take);

                return collection.MapTo<DataCollection<MarcasDTO>>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las Convenios");
            }

        }

        public async Task<MarcasDTO> GetAsync(long id)
        {
            try
            {
                return (await _context.Marcas.SingleAsync(x => x.IdMarca == id)).MapTo<MarcasDTO>();
            }
            catch (Exception ex)
            {
                if (_context.Convenios.SingleAsync(x => x.IdConvenio == id) == null)
                {
                    throw new Exception("Error al obtener marcas, la marca  con id" + " " + id + " " + "no existe");
                }
                throw new Exception("Error al obtener  la marca");
            }

        }
        public async Task<UpdateMarcaDTO> PutAsync(UpdateMarcaDTO Marca, long id)
        {
            if (_context.Marcas.SingleAsync(x => x.IdMarca == id).Result == null)
            {
                throw new Exception("La marca con id" + " " + id + " " + ",no existe");
            }
            var marca = await _context.Marcas.SingleAsync(x => x.IdMarca == id);
            marca.Marca = Marca.Marca;
            marca.Obs = Marca.Obs;

            await _context.SaveChangesAsync();

            return Marca.MapTo<UpdateMarcaDTO>();
        }
        public async Task<MarcasDTO> DeleteAsync(long id)
        {
            try
            {
                var marca = await _context.Marcas.SingleAsync(x => x.IdMarca == id);
                _context.Marcas.Remove(marca);
                await _context.SaveChangesAsync();
                return marca.MapTo<MarcasDTO>();
            }
            catch (Exception ex)
            {
                if (_context.Marcas.SingleAsync(x => x.IdMarca == id) == null)
                {
                    throw new Exception("Error al eliminar la marca, la marca con id" + " " + id + " " + "no existe");
                }
                throw new Exception("Error al eliminar la marca");
            }

        }

    }

}


