using Common.Collection;
using DATA.DTOS;
using DATA.DTOS.Updates;
using Microsoft.EntityFrameworkCore;
using PERSISTENCE;
using DATA.DTOS;
using DATA.DTOS.Updates;
using DATA.Extensions;
using Service.Queries.DTOS;
using Services.Common.Mapping;
using Services.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DATA.Models;
using DATA.Errors;
using System.Net;

namespace Service.Queries
{
    public interface IValoresMedicionesQueryService
    {
        Task<DataCollection<ValoresMedicionesDTO>> GetAllAsync(int page, int take, IEnumerable<int> ValoresMediciones = null, bool order = false);
        Task<ValoresMedicionesDTO> GetAsync(int id);
        Task<UpdateValoresMedicionesDTO> PutAsync(UpdateValoresMedicionesDTO ValorMedicion, int it);
        Task<ValoresMedicionesDTO> DeleteAsync(int id);
        Task<GetResponse> CreateAsync(UpdateValoresMedicionesDTO valores);
    }

    public class ValoresMedicionesQueryService : IValoresMedicionesQueryService
    {
        private readonly Context _context;

        public ValoresMedicionesQueryService(Context context)
        {
            _context = context;
        }

        public async Task<DataCollection<ValoresMedicionesDTO>> GetAllAsync(int page, int take, IEnumerable<int> ValoresMediciones = null, bool order = false)
        {
            try
            {
                if (!order)
                {
                    var orderBy = await _context.ValoresMediciones
                    .Where(x => ValoresMediciones == null || ValoresMediciones.Contains(x.IdValorMedicion))
                    .OrderBy(x => x.IdValorMedicion)
                    .GetPagedAsync(page, take);
                    return orderBy.MapTo<DataCollection<ValoresMedicionesDTO>>();
                }
                var collection = await _context.ValoresMediciones
                .Where(x => ValoresMediciones == null || ValoresMediciones.Contains(x.IdValorMedicion))
                .OrderByDescending(x => x.IdValorMedicion)
                .GetPagedAsync(page, take);
                if (!collection.HasItems)
                {
                    throw new EmptyCollectionException("No se encontró ningun Item en la Base de Datos");
                }
                return collection.MapTo<DataCollection<ValoresMedicionesDTO>>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las Agrupaciones Sindicales");
            }

        }

        public async Task<ValoresMedicionesDTO> GetAsync(int id)
        {
            try
            {
                if (await _context.ValoresMediciones.FindAsync(id) == null)
                {
                    throw new EmptyCollectionException("Error al obtener la Agrupacion Sindical, la Agrupacion Sindical con id" + " " + id + " " + "no existe");
                }
                return (await _context.ValoresMediciones.SingleAsync(x => x.IdValorMedicion == id)).MapTo<ValoresMedicionesDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<UpdateValoresMedicionesDTO> PutAsync(UpdateValoresMedicionesDTO ValoresMediciones, int id)
        {
            if (await _context.ValoresMediciones.FindAsync(id) == null)
            {
                throw new EmptyCollectionException("Error al actualizar la Agrupacion Sindical, la Agrupacion Sindical con id" + " " + id + " " + "no existe");
            }
            var valores = await _context.ValoresMediciones.FindAsync(id);

            valores.ValorMedicion = ValoresMediciones.ValorMedicion;
            valores.Obs = ValoresMediciones.Obs;

            await _context.SaveChangesAsync();

            return ValoresMediciones.MapTo<UpdateValoresMedicionesDTO>();
        }
        public async Task<ValoresMedicionesDTO> DeleteAsync(int id)
        {
            try
            {
                var valores = await _context.ValoresMediciones.FindAsync(id);
                if (valores == null)
                {
                    throw new EmptyCollectionException("Error al eliminar la Agrupacion Sindical, la Agrupacion Sindical con id" + " " + id + " " + "no existe");
                }
                _context.ValoresMediciones.Remove(valores);
                
                await _context.SaveChangesAsync();
                
                return valores.MapTo<ValoresMedicionesDTO>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public async Task<GetResponse> CreateAsync(UpdateValoresMedicionesDTO valores)
        {
            try
            {
                if (valores.ValorMedicion is null || valores.ValorMedicion == "")
                {
                    var ex = new EmptyCollectionException("Debe ingresar el Valor de la Medición");

                    return new GetResponse()
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Message = ex.ToString(),
                        Result = null
                    };
                }
                var newValorM = new ValoresMediciones()
                {
                    ValorMedicion = valores.ValorMedicion,
                    Obs = valores.Obs,
                };
                await _context.ValoresMediciones.AddAsync(newValorM);

                await _context.SaveChangesAsync();
                var nuevoValor = newValorM.MapTo<UpdateValoresMedicionesDTO>();

                return new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success",
                    Result = nuevoValor
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el Valor Medicional");
            }

        }

    }




}