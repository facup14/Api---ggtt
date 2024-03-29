﻿using Common.Collection;
using DATA.DTOS;
using DATA.DTOS.Updates;
using DATA.Errors;
using DATA.Extensions;
using DATA.Models;
using Microsoft.EntityFrameworkCore;
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
    public interface IConveniosQueryService
    {
        Task<DataCollection<ConveniosDTO>> GetAllAsync(int page, int take, IEnumerable<int> Convenios = null, bool order = false);
        Task<ConveniosDTO> GetAsync(int id);
        Task<UpdateConvenioDTO> PutAsync(UpdateConvenioDTO Convenio, int it);
        Task<ConveniosDTO> DeleteAsync(int id);
        Task<GetResponse> CreateAsync(UpdateConvenioDTO convenio);
    }

    public class ConveniosQueryService : IConveniosQueryService
    {
        private readonly Context _context;

        public ConveniosQueryService(Context context)
        {
            _context = context;
        }

        public async Task<DataCollection<ConveniosDTO>> GetAllAsync(int page, int take, IEnumerable<int> convenios = null, bool order = false)
        {
            try
            {
                if (!order)
                {
                    var orderBy = await _context.Convenios
                    .Where(x => convenios == null || convenios.Contains(x.IdConvenio))
                    .OrderBy(x => x.IdConvenio)
                    .GetPagedAsync(page, take);
                    return orderBy.MapTo<DataCollection<ConveniosDTO>>();
                }
                var collection = await _context.Convenios
                .Where(x => convenios == null || convenios.Contains(x.IdConvenio))
                .OrderByDescending(x => x.IdConvenio)
                .GetPagedAsync(page, take);
                if (!collection.HasItems)
                {
                    throw new EmptyCollectionException("No se encontró ningun Item en la Base de Datos");
                }
                return collection.MapTo<DataCollection<ConveniosDTO>>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los Convenios");
            }

        }

        public async Task<ConveniosDTO> GetAsync(int id)
        {
            try
            {
                if (await _context.Convenios.FindAsync(id) == null)
                {
                    throw new EmptyCollectionException("Error al obtener el Convenio, el Convenio con id" + " " + id + " " + "no existe");
                }
                return (await _context.Convenios.FindAsync(id)).MapTo<ConveniosDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el Convenio");
            }

        }
        public async Task<UpdateConvenioDTO> PutAsync(UpdateConvenioDTO Convenio, int id)
        {
            if (await _context.Convenios.FindAsync(id) == null)
            {
                throw new EmptyCollectionException("Error al actualizar el Convenio, el Convenio con id" + " " + id + " " + "no existe");
            }
            var convenio = await _context.Convenios.FindAsync(id);
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
                if (convenio == null)
                {
                    throw new EmptyCollectionException("Error al eliminar el Convenio, el Convenio con id" + " " + id + " " + "no existe");
                }
                _context.Convenios.Remove(convenio);
                await _context.SaveChangesAsync();
                return convenio.MapTo<ConveniosDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el Convenio");
            }

        }
        public async Task<GetResponse> CreateAsync(UpdateConvenioDTO convenio)
        {
            try
            {
                if (convenio.Descripcion == null || convenio.Descripcion == "")
                {
                    var ex = new EmptyCollectionException("Debe ingresar una Descripción");

                    return new GetResponse()
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Message = ex.ToString(),
                        Result = null
                    };
                }
                var newConvenio = new Convenios()
                {
                    Descripcion = convenio.Descripcion,
                    Obs = convenio.Obs,
                };
                await _context.Convenios.AddAsync(newConvenio);
                
                await _context.SaveChangesAsync();
                var newConvenios = newConvenio.MapTo<UpdateConvenioDTO>();
                return new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Success",
                    Result = newConvenios
                };
            }     
            catch (Exception ex)
            {
                throw new Exception("Error al crear el Convenio");
            }

        }

    }




}