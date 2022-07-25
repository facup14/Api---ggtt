﻿using Common.Collection;
using DATA.DTOS;
using DATA.DTOS.Updates;
using Microsoft.EntityFrameworkCore;
using PERSISTENCE;
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
    public interface IAlicuotasIVAQueryService
    {
        Task<DataCollection<AlicuotasIVADTO>> GetAllAsync(int page, int take, IEnumerable<int> AlicuotasIVA = null);
        Task<AlicuotasIVADTO> GetAsync(int id);
        Task<UpdateAlicuotasIVADTO> PutAsync(UpdateAlicuotasIVADTO AlicuotasIVA, int id);
        Task<AlicuotasIVADTO> DeleteAsync(int id);
    }
    
    public class AlicuotasIVAQueryService : IAlicuotasIVAQueryService
    {
        private readonly Context _context;

        public AlicuotasIVAQueryService(Context context)
        {
            _context = context;
        }

        public async Task<DataCollection<AlicuotasIVADTO>> GetAllAsync(int page, int take, IEnumerable<int> AlicuotasIVA = null)
        {
            try
            {
                var collection = await _context.AlicuotasIVA
                .Where(x => AlicuotasIVA == null || AlicuotasIVA.Contains(x.IdAlicuota))
                .OrderByDescending(x => x.IdAlicuota)
                .GetPagedAsync(page, take);
                if (!collection.HasItems)
                {
                    throw new EmptyCollectionException("No se encontró ningun Item en la Base de Datos");
                }
                return collection.MapTo<DataCollection<AlicuotasIVADTO>>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las Agrupaciones Sindicales");
            }

        }

        public async Task<AlicuotasIVADTO> GetAsync(int id)
        {
            try
            {
                if (await _context.AlicuotasIVA.FindAsync(id) == null)
                {
                    throw new EmptyCollectionException("Error al obtener la Agrupacion Sindical, la Agrupacion Sindical con id" + " " + id + " " + "no existe");
                }
                return (await _context.AlicuotasIVA.SingleAsync(x => x.IdAlicuota == id)).MapTo<AlicuotasIVADTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<UpdateAlicuotasIVADTO> PutAsync(UpdateAlicuotasIVADTO AlicuotasIVA, int id)
        {
            if (await _context.AlicuotasIVA.FindAsync(id) == null)
            {
                throw new EmptyCollectionException("Error al actualizar la Agrupacion Sindical, la Agrupacion Sindical con id" + " " + id + " " + "no existe");
            }
            var alicuotas = await _context.AlicuotasIVA.FindAsync(id);

            alicuotas.Detalle = AlicuotasIVA.Detalle;
            alicuotas.Alicuota = AlicuotasIVA.Alicuota;
            alicuotas.NumeroCUIT = AlicuotasIVA.NumeroCUIT;
            alicuotas.AlicuotaRecargo = AlicuotasIVA.AlicuotaRecargo;

            await _context.SaveChangesAsync();

            return AlicuotasIVA.MapTo<UpdateAlicuotasIVADTO>();
        }
        public async Task<AlicuotasIVADTO> DeleteAsync(int id)
        {
            try
            {
                var alicuotas = await _context.AlicuotasIVA.FindAsync(id);
                if (alicuotas == null)
                {
                    throw new EmptyCollectionException("Error al eliminar la Agrupacion Sindical, la Agrupacion Sindical con id" + " " + id + " " + "no existe");
                }
                _context.AlicuotasIVA.Remove(alicuotas);
                
                await _context.SaveChangesAsync();
                
                return alicuotas.MapTo<AlicuotasIVADTO>();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }




}