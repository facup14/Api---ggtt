using Common.Collection;
using DATA.DTOS;
using DATA.DTOS.Updates;
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
    public interface IEmpresasQueryService
    {
        Task<DataCollection<EmpresasDTO>> GetAllAsync(int page, int take, IEnumerable<int> Empresas = null);
        Task<EmpresasDTO> GetAsync(int id);
        Task<UpdateEmpresaDTO> PutAsync(UpdateEmpresaDTO Empresa, int it);
        Task<EmpresasDTO> DeleteAsync(int id);
    }

    public class EmpresasQueryService : IEmpresasQueryService
    {
        private readonly Context _context;

        public EmpresasQueryService(Context context)
        {
            _context = context;
        }

        public async Task<DataCollection<EmpresasDTO>> GetAllAsync(int page, int take, IEnumerable<int> empresas = null)
        {
            try
            {
                var collection = await _context.Empresas
                .Where(x => empresas == null || empresas.Contains(x.IdEmpresa))
                .OrderByDescending(x => x.IdEmpresa)
                .GetPagedAsync(page, take);

                return collection.MapTo<DataCollection<EmpresasDTO>>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las Agrupaciones Sindicales");
            }

        }

        public async Task<EmpresasDTO> GetAsync(int id)
        {
            try
            {
                return (await _context.Empresas.SingleAsync(x => x.IdEmpresa == id)).MapTo<EmpresasDTO>();
            }
            catch (Exception ex)
            {
                if (_context.Empresas.SingleAsync(x => x.IdEmpresa == id) == null)
                {
                    throw new Exception("Error al obtener el convenio, lel convenio con id" + " " + id + " " + "no existe");
                }
                throw new Exception("Error al obtener el convenio");
            }

        }
        public async Task<UpdateEmpresaDTO> PutAsync(UpdateEmpresaDTO Empresa, int id)
        {
            if (_context.Empresas.SingleAsync(x => x.IdEmpresa == id).Result == null)
            {
                throw new Exception("El empresa con id" + " " + id + " " + ",no existe");
            }
            var empresa = await _context.Empresas.SingleAsync(x => x.IdEmpresa == id);
            empresa.Descripcion = Empresa.Descripcion;
            empresa.Obs = Empresa.Obs;

            await _context.SaveChangesAsync();

            return Empresa.MapTo<UpdateEmpresaDTO>();
        }
        public async Task<EmpresasDTO> DeleteAsync(int id)
        {
            try
            {
                var empresa = await _context.Empresas.SingleAsync(x => x.IdEmpresa == id);
                _context.Empresas.Remove(empresa);
                await _context.SaveChangesAsync();
                return empresa.MapTo<EmpresasDTO>();
            }
            catch (Exception ex)
            {
                if (_context.Empresas.SingleAsync(x => x.IdEmpresa == id) == null)
                {
                    throw new Exception("Error al eliminar empresa, el convenio con id" + " " + id + " " + "no existe");
                }
                throw new Exception("Error al eliminar el empresa");
            }

        }

    }
}
