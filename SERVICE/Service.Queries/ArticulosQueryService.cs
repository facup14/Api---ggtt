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
using System.Text;
using System.Threading.Tasks;

namespace Service.Queries
{
    public interface IArticulosQueryService
    {
        Task<DataCollection<ArticulosDTO>> GetAllAsync(int page, int take, IEnumerable<long> Articulos = null);
        Task<ArticulosDTO> GetAsync(long id);
        Task<UpdateArticuloDTO> PutAsync(UpdateArticuloDTO Articulo, long it);
        Task<ArticulosDTO> DeleteAsync(long id);
        Task<UpdateArticuloDTO> CreateAsync(UpdateArticuloDTO Articulo);
    }
    public class ArticulosQueryService : IArticulosQueryService
    {
        private readonly Context _context;

        public ArticulosQueryService(Context context)
        {
            _context = context;
        }

        public async Task<DataCollection<ArticulosDTO>> GetAllAsync(int page, int take, IEnumerable<long> Articulos = null)
        {
            try
            {
                var collection = await _context.Articulos
                .Where(x => Articulos == null || Articulos.Contains(x.IdArticulo))
                .OrderByDescending(x => x.IdArticulo)
                .GetPagedAsync(page, take);
                if (!collection.HasItems)
                {
                    throw new EmptyCollectionException("No se encontró ningun Item en la Base de Datos");
                }
                return collection.MapTo<DataCollection<ArticulosDTO>>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los Articulos");
            }

        }

        public async Task<ArticulosDTO> GetAsync(long id)
        {
            try
            {
                if (await _context.Articulos.FindAsync(id) == null)
                {
                    throw new EmptyCollectionException("Error al obtener el Articulo, el Articulo con id" + " " + id + " " + "no existe");
                }
                return (await _context.Articulos.FindAsync(id)).MapTo<ArticulosDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el Articulo");
            }

        }
        public async Task<UpdateArticuloDTO> PutAsync(UpdateArticuloDTO Articulo, long id)
        {
            if (await _context.Articulos.FindAsync(id) == null)
            {
                throw new EmptyCollectionException("Error al actualizar el Articulo, el Articulo con id" + " " + id + " " + "no existe");
            }
            var articulo = await _context.Articulos.SingleAsync(x => x.IdArticulo == id);
            articulo.DetalleArticulo = Articulo.DetalleArticulo;
            articulo.CodigoFabrica = Articulo.CodigoFabrica;
            articulo.Costo = Articulo.Costo;
            articulo.Obs = Articulo.Obs;

            await _context.SaveChangesAsync();

            return Articulo.MapTo<UpdateArticuloDTO>();
        }
        public async Task<ArticulosDTO> DeleteAsync(long id)
        {
            try
            {
                var articulo = await _context.Articulos.SingleAsync(x => x.IdArticulo == id);
                if (articulo == null)
                {
                    throw new EmptyCollectionException("Error al eliminar el Articulo, el Articulo con id" + " " + id + " " + "no existe");
                }
                _context.Articulos.Remove(articulo);
                await _context.SaveChangesAsync();
                return articulo.MapTo<ArticulosDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el Articulo");
            }

        }
        public async Task<UpdateArticuloDTO> CreateAsync(UpdateArticuloDTO Articulo)
        {
            try
            {
                var newArticulo = new Articulos()
                {
                    DetalleArticulo = Articulo.DetalleArticulo,
                    CodigoFabrica = Articulo.CodigoFabrica,
                    Costo = Articulo.Costo,
                    Obs = Articulo.Obs,
                };
                await _context.Articulos.AddAsync(newArticulo);

                await _context.SaveChangesAsync();
                return newArticulo.MapTo<UpdateArticuloDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el Articulo");
            }

        }
    }
}
