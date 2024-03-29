﻿using Common.Collection;
using DATA.DTOS;
using DATA.DTOS.Updates;
using Microsoft.EntityFrameworkCore;
using PERSISTENCE;
using Services.Common.Mapping;
using Services.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DATA.Extensions;
using DATA.Models;
using DATA.Errors;
using System.Net;

namespace Service.Queries
{
    public interface IEmpresasQueryService
    {
        Task<DataCollection<EmpresasDTO>> GetAllAsync(int page, int take, IEnumerable<int> Empresas = null, bool order = false);
        Task<EmpresasDTO> GetAsync(int id);
        Task<UpdateEmpresaDTO> PutAsync(UpdateEmpresaDTO Empresa, int it);
        Task<EmpresasDTO> DeleteAsync(int id);
        Task<GetResponse> CreateAsync(UpdateEmpresaDTO empresa);
    }

    public class EmpresasQueryService : IEmpresasQueryService
    {
        private readonly Context _context;

        public EmpresasQueryService(Context context)
        {
            _context = context;
        }

        public async Task<DataCollection<EmpresasDTO>> GetAllAsync(int page, int take, IEnumerable<int> empresas = null, bool order = false)
        {
            try
            {
                if (!order)
                {
                    var Ascend = await _context.Empresas
                                        .Where(x => empresas == null || empresas.Contains(x.IdEmpresa))
                                        .OrderBy(x => x.IdEmpresa)
                                        .GetPagedAsync(page, take);
                    return Ascend.MapTo<DataCollection<EmpresasDTO>>();
                }
                var collection = await _context.Empresas
                                        .Where(x => empresas == null || empresas.Contains(x.IdEmpresa))
                                        .OrderByDescending(x => x.IdEmpresa)
                                        .GetPagedAsync(page, take);
                return collection.MapTo<DataCollection<EmpresasDTO>>();
                if (!collection.HasItems)
                {
                    throw new EmptyCollectionException("No se encontraron Items en la Base de Datos");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las Empresas");
            }

        }

        public async Task<EmpresasDTO> GetAsync(int id)
        {
            try
            {
                var empresa = await _context.Empresas.FindAsync(id);

                if (await _context.Empresas.FindAsync(id) == null)
                {
                    throw new EmptyCollectionException("Error al obtener la Empresa, la Empresa con id" + " " + id + " " + "no existe");
                }
                return empresa.MapTo<EmpresasDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<UpdateEmpresaDTO> PutAsync(UpdateEmpresaDTO Empresa, int id)
        {
            if (await _context.Empresas.FindAsync(id) == null)
            {
                throw new EmptyCollectionException("Error al actualizar la Empresa, la Empresa con id" + " " + id + " " + "no existe");
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
                if (empresa == null)
                {
                    throw new EmptyCollectionException("Error al eliminar la Empresa, la Empresa con id" + " " + id + " " + "no existe");
                }
                _context.Empresas.Remove(empresa);
                await _context.SaveChangesAsync();
                return empresa.MapTo<EmpresasDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la Empresa");
            }

        }
        public async Task<GetResponse> CreateAsync(UpdateEmpresaDTO empresa)
        {
            try
            {
                if (empresa.Descripcion is null || empresa.Descripcion == "")
                {
                    var ex = new EmptyCollectionException("Debe ingresar una Descripción");

                    return new GetResponse()
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Message = ex.ToString(),
                        Result = null
                    };
                }
                var newEmpresa = new Empresas()
                {
                    Descripcion = empresa.Descripcion,
                    Obs = empresa.Obs,
                };
                await _context.Empresas.AddAsync(newEmpresa);

                await _context.SaveChangesAsync();
                var nuevaEmpresa =  newEmpresa.MapTo<UpdateEmpresaDTO>();

                return new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success",
                    Result = nuevaEmpresa
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la Empresa");
            }

        }

    }
}
