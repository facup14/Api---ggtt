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
{/// <summary>
/// //////////////////////////////////////////Se utilizan Queries para hacer peticiones que no modifiquen la base de datos.(Excepto el PUT)
/// </summary>
    public interface IUnidadesQueryService
    {
        Task<DataCollection<UnidadesDTO>> GetAllAsync(int page, int take, IEnumerable<int> unidades = null);
        Task<UnidadesDTO> GetAsync(int id);
        Task<UpdateUnidadDTO> PutAsync(UpdateUnidadDTO unidad, int it);
        Task<UnidadesDTO> DeleteAsync(int id);
    }
    public class UnidadesQueryService : IUnidadesQueryService
    {
        private readonly Context _context;

        public UnidadesQueryService(Context context)
        {
            _context = context;
        }

        public async Task<DataCollection<UnidadesDTO>> GetAllAsync(int page, int take, IEnumerable<int> unidades = null)
        {
            try
            {
                var collection = await _context.Unidades
                .Where(x => unidades == null || unidades.Contains(x.IdUnidad))
                .OrderByDescending(x => x.IdUnidad)
                .GetPagedAsync(page, take);

                return collection.MapTo<DataCollection<UnidadesDTO>>();
            }catch(Exception ex)
            {
                throw new Exception("Error al obtener las unidades");
            }
            
        }

        public async Task<UnidadesDTO> GetAsync(int id)
        {
            try
            {
               return (await _context.Unidades.SingleAsync(x => x.IdUnidad == id)).MapTo<UnidadesDTO>(); 
            }catch(Exception ex)
            {
                if (_context.Unidades.SingleAsync(x => x.IdUnidad == id) == null)
                {
                    throw new Exception("Error al obtener la unidad, la unidad con id" + " " + id + " " + "no existe");
                }
                throw new Exception("Error al obtener la unidad");
            }
            
        }
        public async Task<UpdateUnidadDTO> PutAsync(UpdateUnidadDTO unidad, int id)
        {
            if(_context.Unidades.SingleAsync(x => x.IdUnidad == id) == null) {
                throw new Exception("La unidad con id" + " " + id + " " + ",no existe");
            }
            var unidade = await _context.Unidades.SingleAsync(x => x.IdUnidad == id);
            unidade.NroUnidad = unidad.NroUnidad;
            unidade.Dominio = unidad.Dominio;
            unidade.Motor = unidad.Motor;
            unidade.Chasis = unidad.Chasis;
            unidade.Titular = unidad.Titular;
            unidade.idEstadoUnidad = unidad.idEstadoUnidad;
            unidade.idModelo = unidad.idModelo;
            unidade.idSituacionUnidad = unidad.idSituacionUnidad;
                
            await _context.SaveChangesAsync();
                
            return unidad.MapTo<UpdateUnidadDTO>();
        }
        public async Task<UnidadesDTO> DeleteAsync(int id)
        {
            try
            {
                var unidade = await _context.Unidades.SingleAsync(x => x.IdUnidad == id);
                _context.Unidades.Remove(unidade);
                await _context.SaveChangesAsync();
                return unidade.MapTo<UnidadesDTO>();
            }catch(Exception ex)
            {
                if (_context.Unidades.SingleAsync(x => x.IdUnidad == id) == null)
                {
                    throw new Exception("Error al eliminar la unidad, la unidad con id" + " " + id + " " + "no existe");
                }
                throw new Exception("Error al eliminar la unidad");
            }
            
        }
        
    }
    
}
