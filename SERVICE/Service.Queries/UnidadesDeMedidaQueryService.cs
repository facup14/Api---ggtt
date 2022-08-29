using Common.Collection;
using DATA.DTOS;
using DATA.DTOS.Updates;
using DATA.Errors;
using DATA.Extensions;
using DATA.Models;
using PERSISTENCE;
using Services.Common.Mapping;
using Services.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Service.Queries
{
    public interface IUnidadesDeMedidaQueryService
    {
        Task<DataCollection<UnidadesDeMedidaDTO>> GetAllAsync(int page, int take, IEnumerable<long> unidadesMedida = null, bool order = false);
        Task<UnidadesDeMedidaDTO> GetAsync(long id);
        Task<UpdateUnidadDeMedidaDTO> PutAsync(UpdateUnidadDeMedidaDTO titulo, long id);
        Task<UnidadesDeMedidaDTO> DeleteAsync(long id);
        Task<GetResponse> CreateAsync(UpdateUnidadDeMedidaDTO unidadesMedida);
    }
    public class UnidadesDeMedidaQueryService : IUnidadesDeMedidaQueryService
    {
        private readonly Context _context;
        public UnidadesDeMedidaQueryService(Context context)
        {
            _context = context;
        }
        public async Task<DataCollection<UnidadesDeMedidaDTO>> GetAllAsync(int page, int take, IEnumerable<long> unidadesMedida = null, bool order = false)
        {
            try
            {
                if (!order)
                {
                    var orderBy = await _context.UnidadesDeMedida
                    .Where(x => unidadesMedida == null || unidadesMedida.Contains(x.IdUnidadDeMedida))
                    .OrderBy(x => x.IdUnidadDeMedida)
                    .GetPagedAsync(page, take);
                    return orderBy.MapTo<DataCollection<UnidadesDeMedidaDTO>>();
                }
                var collection = await _context.UnidadesDeMedida
                .Where(x => unidadesMedida == null || unidadesMedida.Contains(x.IdUnidadDeMedida))
                .OrderByDescending(x => x.IdUnidadDeMedida)
                .GetPagedAsync(page, take);
                if (!collection.HasItems)
                {
                    throw new EmptyCollectionException("No se encontró ningun Item en la Base de Datos");
                }
                return collection.MapTo<DataCollection<UnidadesDeMedidaDTO>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<UnidadesDeMedidaDTO> GetAsync(long id)
        {
            try
            {
                var unidadMedida = await _context.UnidadesDeMedida.FindAsync(id);

                if (await _context.UnidadesDeMedida.FindAsync(id) == null)
                {
                    throw new EmptyCollectionException("Error al obtener la Unidad de Medida, la Unidad con id" + " " + id + " " + "no existe");
                }
                return unidadMedida.MapTo<UnidadesDeMedidaDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<UpdateUnidadDeMedidaDTO> PutAsync(UpdateUnidadDeMedidaDTO titulo, long id)
        {
            if (await _context.UnidadesDeMedida.FindAsync(id) == null)
            {
                throw new EmptyCollectionException("Error al obtener La Unidad de Medida, la Unidad con id" + " " + id + " " + "no existe");
            }
            var updateTitulo = await _context.UnidadesDeMedida.FindAsync(id);

            updateTitulo.UnidadDeMedida = titulo.UnidadDeMedida;


            await _context.SaveChangesAsync();

            return titulo.MapTo<UpdateUnidadDeMedidaDTO>();
        }
        public async Task<UnidadesDeMedidaDTO> DeleteAsync(long id)
        {
            var unidadMedida = await _context.UnidadesDeMedida.FindAsync(id);
            if (unidadMedida == null)
            {
                throw new EmptyCollectionException("Error al eliminar el Titulo, el Titulo con id" + " " + id + " " + "no existe");
            }

            _context.UnidadesDeMedida.Remove(unidadMedida);

            await _context.SaveChangesAsync();

            return unidadMedida.MapTo<UnidadesDeMedidaDTO>();
        }
        public async Task<GetResponse> CreateAsync(UpdateUnidadDeMedidaDTO unidadesMedida)
        {
            try
            {
                if (unidadesMedida.UnidadDeMedida is null || unidadesMedida.UnidadDeMedida == "")
                {
                    var ex = new EmptyCollectionException("Debe ingresar la Unidad de Medida");

                    return new GetResponse()
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Message = ex.ToString(),
                        Result = null
                    };
                }
                var newUnidadesMedida = new UnidadesDeMedida()
                {
                    UnidadDeMedida = unidadesMedida.UnidadDeMedida,
                };
                await _context.UnidadesDeMedida.AddAsync(newUnidadesMedida);

                await _context.SaveChangesAsync();
                var nuevaUnidadMedida =  newUnidadesMedida.MapTo<UpdateUnidadDeMedidaDTO>();

                return new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success",
                    Result = nuevaUnidadMedida
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la Unidad de Medida");
            }

        }
    }
}
