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
    public interface IDomiciliosQueryService
    {
        Task<DataCollection<DomiciliosDTO>> GetAllAsync(int page, int take, IEnumerable<int> domicilios = null, bool orderNume = false);
        Task<DomiciliosDTO> GetAsync(int id);
        Task<UpdateDomiciliosDTO> PutAsync(UpdateDomiciliosDTO Domicilio, int id);
        Task<DomiciliosDTO> DeleteAsync(int id);
        Task<UpdateDomiciliosDTO> CreateAsync(UpdateDomiciliosDTO domicilio);
    }
    public class DomiciliosQueryService : IDomiciliosQueryService
    {
        private readonly Context _context;

        public DomiciliosQueryService(Context context)
        {
            _context = context;
        }

        public async Task<DataCollection<DomiciliosDTO>> GetAllAsync(int page, int take, IEnumerable<int> domicilios = null, bool orderNume = false)
        {
            try
            {
                if (orderNume)
                {
                    var orderBy = await _context.Domicilios
                    .Where(x => domicilios == null || domicilios.Contains(x.IdDomicilio))
                    .OrderBy(x => x.IdDomicilio)
                    .GetPagedAsync(page, take);
                    return orderBy.MapTo<DataCollection<DomiciliosDTO>>();
                }
                var collection = await _context.Domicilios
                .Where(x => domicilios == null || domicilios.Contains(x.IdDomicilio))
                .OrderByDescending(x => x.IdDomicilio)
                .GetPagedAsync(page, take);
                if (!collection.HasItems)
                {
                    throw new EmptyCollectionException("No se encontró ningun Item en la Base de Datos");
                }
                return collection.MapTo<DataCollection<DomiciliosDTO>>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los Domicilios");
            }

        }

        public async Task<DomiciliosDTO> GetAsync(int id)
        {
            try
            {
                if (await _context.Domicilios.FindAsync(id) == null)
                {
                    throw new EmptyCollectionException("Error al obtener el Domicilio, el Domicilio con id" + " " + id + " " + "no existe");
                }
                return (await _context.Domicilios.FindAsync(id)).MapTo<DomiciliosDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el Estado");
            }

        }
        public async Task<UpdateDomiciliosDTO> PutAsync(UpdateDomiciliosDTO Domicilio, int id)
        {
            if (await _context.Domicilios.FindAsync(id) == null)
            {
                throw new EmptyCollectionException("Error al actualizar el Domicilio, el Domicilio con id" + " " + id + " " + "no existe");
            }
            var domicilio = await _context.Domicilios.SingleAsync(x => x.IdDomicilio == id);
            domicilio.Predeterminado = Domicilio.Predeterminado;
            domicilio.IdProveedor = Domicilio.IdProveedor;
            domicilio.IdCalle = Domicilio.IdCalle;
            domicilio.Numero = Domicilio.Numero;
            domicilio.IdBarrio = Domicilio.IdBarrio;
            domicilio.Dpto = Domicilio.Dpto;
            domicilio.IdCliente = Domicilio.IdCliente;

            await _context.SaveChangesAsync();

            return Domicilio.MapTo<UpdateDomiciliosDTO>();
        }
        public async Task<DomiciliosDTO> DeleteAsync(int id)
        {
            try
            {
                var estadounidad = await _context.Domicilios.SingleAsync(x => x.IdDomicilio == id);
                if (estadounidad == null)
                {
                    throw new EmptyCollectionException("Error al eliminar el Domicilio, el Domicilio con id" + " " + id + " " + "no existe");
                }
                _context.Domicilios.Remove(estadounidad);
                await _context.SaveChangesAsync();
                return estadounidad.MapTo<DomiciliosDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el Domicilio");
            }

        }
        public async Task<UpdateDomiciliosDTO> CreateAsync(UpdateDomiciliosDTO domicilio)
        {
            try
            {
                var newDomicilio = new Domicilios()
                {
                    Predeterminado = domicilio.Predeterminado,
                    IdProveedor = domicilio.IdProveedor,
                    IdCalle = domicilio.IdCalle,
                    Numero = domicilio.Numero,
                    IdBarrio = domicilio.IdBarrio,
                    Dpto = domicilio.Dpto,
                    IdCliente = domicilio.IdCliente,
                };
                await _context.Domicilios.AddAsync(newDomicilio);

                await _context.SaveChangesAsync();
                return newDomicilio.MapTo<UpdateDomiciliosDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la Agrupación");
            }

        }
    }
}
