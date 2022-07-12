using Common.Collection;
using Microsoft.EntityFrameworkCore;
using PERSISTENCE;
using Service.Queries.DTOS;
using Services.Common.Mapping;
using Services.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Queries
{
    public interface IConveniosQueryService
    {
        Task<DataCollection<ConveniosDTO>> GetAllAsync(int page, int take, IEnumerable<int> Convenios = null);
        Task<ConveniosDTO> GetAsync(int id);
        Task<UpdateConvenioDTO> PutAsync(UpdateConvenioDTO Convenio, int it);
        Task<ConveniosDTO> DeleteAsync(int id);
    }

    public class ConveniosQueryService : IConveniosQueryService
    {
        private readonly Context _context;

        public ConveniosQueryService(Context context)
        {
            _context = context;
        }

        public async Task<DataCollection<ConveniosDTO>> GetAllAsync(int page, int take, IEnumerable<int> convenios = null)
        {
            try
            {
                var collection = await _context.Convenios
                .Where(x => convenios == null || convenios.Contains(x.IdConvenio))
                .OrderByDescending(x => x.IdConvenio)
                .GetPagedAsync(page, take);

                return collection.MapTo<DataCollection<ConveniosDTO>>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las Convenios");
            }

        }

        public async Task<ConveniosDTO> GetAsync(int id)
        {
            try
            {
                return (await _context.Convenios.SingleAsync(x => x.IdConvenio == id)).MapTo<ConveniosDTO>();
            }
            catch (Exception ex)
            {
                if (_context.Convenios.SingleAsync(x => x.IdConvenio == id) == null)
                {
                    throw new Exception("Error al obtener el convenio, lel convenio con id" + " " + id + " " + "no existe");
                }
                throw new Exception("Error al obtener el convenio");
            }

        }
        public async Task<UpdateConvenioDTO> PutAsync(UpdateConvenioDTO Convenio, int id)
        {
            if (_context.Convenios.SingleAsync(x => x.IdConvenio == id).Result == null)
            {
                throw new Exception("El convenio con id" + " " + id + " " + ",no existe");
            }
            var convenio = await _context.Convenios.SingleAsync(x => x.IdConvenio == id);
            convenio.Descripcion = Convenio.Descripcion;
            convenio.Obs = Convenio.Obs;

            await _context.SaveChangesAsync();

            return Convenio.MapTo<UpdateConvenioDTO>();
        }
        public async Task<ConveniosDTO> DeleteAsync(int id)
        {
            try
            {
                var convenio = await _context.Convenios.SingleAsync(x => x.IdConvenio == id);
                _context.Convenios.Remove(convenio);
                await _context.SaveChangesAsync();
                return convenio.MapTo<ConveniosDTO>();
            }
            catch (Exception ex)
            {
                if (_context.Convenios.SingleAsync(x => x.IdConvenio == id) == null)
                {
                    throw new Exception("Error al eliminar el Convenio, el convenio con id" + " " + id + " " + "no existe");
                }
                throw new Exception("Error al eliminar el convenio");
            }

        }

    }




}