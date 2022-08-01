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
using DATA.Extensions;
using DATA.Models;

namespace Service.Queries
{
    public interface IModelosQueryService
    {
        Task<DataCollection<ModelosDTO>> GetAllAsync(int page, int take, IEnumerable<long> Modelos = null, bool order = false);
        Task<ModelosDTO> GetAsync(long id);
        Task<UpdateModeloDTO> PutAsync(UpdateModeloDTO Modelos, long it);
        Task<ModelosDTO> DeleteAsync(long id);
        Task<UpdateModeloDTO> CreateAsync(UpdateModeloDTO modelo);
    }

    public class ModelosQueryService : IModelosQueryService
    {
        private readonly Context _context;

        public ModelosQueryService(Context context)
        {
            _context = context;
        }

        public async Task<DataCollection<ModelosDTO>> GetAllAsync(int page, int take, IEnumerable<long> modelos = null, bool order = false)
        {
            try
            {
                if (!order)
                {
                    var orderBy = await _context.Modelos
                    .Where(x => modelos == null || modelos.Contains(x.IdModelo))
                    .OrderBy(x => x.IdModelo)
                    .GetPagedAsync(page, take);
                    return orderBy.MapTo<DataCollection<ModelosDTO>>();
                }
                var collection = await _context.Modelos
                .Where(x => modelos == null || modelos.Contains(x.IdModelo))
                .OrderByDescending(x => x.IdModelo)
                .GetPagedAsync(page, take);
                if (!collection.HasItems)
                {
                    throw new EmptyCollectionException("No se encontró ningun Item en la Base de Datos");
                }
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
                var modelo = await _context.Modelos.FindAsync(id);

                if (await _context.Modelos.FindAsync(id) == null)
                {
                    throw new EmptyCollectionException("Error al obtener el Modelo, el Modelo con id" + " " + id + " " + "no existe");
                }
                return modelo.MapTo<ModelosDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<UpdateModeloDTO> PutAsync(UpdateModeloDTO Modelo, long id)
        {
            if (await _context.Modelos.FindAsync(id) == null)
            {
                throw new EmptyCollectionException("Error al eliminar el Modelo, el Modelo con id" + " " + id + " " + "no existe");
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
                var modelo = await _context.Modelos.FindAsync(id);
                if (modelo == null)
                {
                    throw new EmptyCollectionException("Error al eliminar el Modelo, el Modelo con id" + " " + id + " " + "no existe");
                }
                _context.Modelos.Remove(modelo);
                await _context.SaveChangesAsync();
                return modelo.MapTo<ModelosDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el Modelo");
            }

        }
        public async Task<UpdateModeloDTO> CreateAsync(UpdateModeloDTO modelo)
        {
            try
            {
                var newModelo = new Modelos()
                {
                    Modelo = modelo.Modelo,
                    Obs = modelo.Obs,
                    idMarca = modelo.idMarca,
                    IdGrupo = modelo.IdGrupo,
                };
                await _context.Modelos.AddAsync(newModelo);

                await _context.SaveChangesAsync();
                return newModelo.MapTo<UpdateModeloDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el Modelo");
            }

        }

    }

}


