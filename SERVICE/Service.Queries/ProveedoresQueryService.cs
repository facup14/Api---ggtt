﻿using Common.Collection;
using DATA.DTOS;
using DATA.DTOS.Updates;
using DATA.Extensions;
using PERSISTENCE;
using Services.Common.Mapping;
using Services.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Queries
{
    public interface IProveedoresQueryService
    {
        Task<DataCollection<ProveedoresDTO>> GetAllAsync(int page, int take, IEnumerable<int> Proveedores = null);
        Task<ProveedoresDTO> GetAsync(int id);
        Task<UpdateProveedoresDTO> PutAsync(UpdateProveedoresDTO proveedores, int id);
        Task<ProveedoresDTO> DeleteAsync(int id);
    }
    public class ProveedoresQueryService : IProveedoresQueryService
    {
        private readonly Context _context;
        public ProveedoresQueryService(Context context)
        {
            _context = context;
        }
        public async Task<DataCollection<ProveedoresDTO>> GetAllAsync(int page, int take, IEnumerable<int> Proveedores = null)
        {
            try
            {
                var collection = await _context.Proveedores
                .Where(x => Proveedores == null || Proveedores.Contains(x.IdProveedor))
                .OrderByDescending(x => x.IdProveedor)
                .GetPagedAsync(page, take);
                if (!collection.HasItems)
                {
                    throw new EmptyCollectionException("No se encontró ningun Item en la Base de Datos");
                }
                return collection.MapTo<DataCollection<ProveedoresDTO>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<ProveedoresDTO> GetAsync(int id)
        {
            try
            {
                var Proveedores = await _context.Proveedores.FindAsync(id);

                if (await _context.Proveedores.FindAsync(id) == null)
                {
                    throw new EmptyCollectionException("Error al obtener la Variable de Unidad, la Variable con id" + " " + id + " " + "no existe");
                }
                return Proveedores.MapTo<ProveedoresDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<UpdateProveedoresDTO> PutAsync(UpdateProveedoresDTO proveedores, int id)
        {
            if (await _context.Proveedores.FindAsync(id) == null)
            {
                throw new EmptyCollectionException("Error al obtener La Unidad de Medida, la Unidad con id" + " " + id + " " + "no existe");
            }
            var updateProveedor = await _context.Proveedores.FindAsync(id);

            updateProveedor.RazonSocial = proveedores.RazonSocial;
            updateProveedor.NCuit = proveedores.NCuit;
            updateProveedor.Telefono = proveedores.Telefono;
            updateProveedor.Celular = proveedores.Celular;
            updateProveedor.Contacto = proveedores.Contacto;
            updateProveedor.Email = proveedores.Email;
            updateProveedor.ChequesA = proveedores.ChequesA;
            updateProveedor.Web = proveedores.Web;
            updateProveedor.Obs = proveedores.Obs;

            await _context.SaveChangesAsync();

            return proveedores.MapTo<UpdateProveedoresDTO>();
        }
        public async Task<ProveedoresDTO> DeleteAsync(int id)
        {
            var Proveedor = await _context.Proveedores.FindAsync(id);
            if (Proveedor == null)
            {
                throw new EmptyCollectionException("Error al eliminar el Titulo, el Titulo con id" + " " + id + " " + "no existe");
            }

            _context.Proveedores.Remove(Proveedor);

            await _context.SaveChangesAsync();

            return Proveedor.MapTo<ProveedoresDTO>();
        }
    }
}