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

namespace Service.Queries
{
    public interface IValoresMedicionesQueryService
    {
        Task<DataCollection<ValoresMedicionesDTO>> GetAllAsync(int page, int take, IEnumerable<int> ValoresMediciones = null);
        Task<ValoresMedicionesDTO> GetAsync(int id);
        Task<UpdateValoresMedicionesDTO> PutAsync(UpdateValoresMedicionesDTO ValorMedicion, int it);
        Task<ValoresMedicionesDTO> DeleteAsync(int id);
        Task<UpdateValoresMedicionesDTO> CreateAsync(UpdateValoresMedicionesDTO valores);
    }

    public class ValoresMedicionesQueryService : IValoresMedicionesQueryService
    {
        private readonly Context _context;

        public ValoresMedicionesQueryService(Context context)
        {
            _context = context;
        }

        public async Task<DataCollection<ValoresMedicionesDTO>> GetAllAsync(int page, int take, IEnumerable<int> ValoresMediciones = null)
        {
            try
            {
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
        public async Task<UpdateValoresMedicionesDTO> CreateAsync(UpdateValoresMedicionesDTO valores)
        {
            try
            {
                var newValorM = new ValoresMediciones()
                {
                    ValorMedicion = valores.ValorMedicion,
                    Obs = valores.Obs,
                };
                await _context.ValoresMediciones.AddAsync(newValorM);

                await _context.SaveChangesAsync();
                return newValorM.MapTo<UpdateValoresMedicionesDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el Valor Medicional");
            }

        }

    }




}