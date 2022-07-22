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
    public interface IRepuestosQueryService
    {
        Task<DataCollection<RepuestosDTO>> GetAllAsync(int page, int take, IEnumerable<long> repuestos = null, bool order = false);
        Task<RepuestosByIdDTO> GetAsync(long id);
        Task<UpdateRepuestosDTO> PutAsync(UpdateRepuestosDTO repuesto, long id);
        Task<RepuestosDTO> DeleteAsync(long id);
        Task<UpdateRepuestosDTO> CreateAsync(UpdateRepuestosDTO repuestos);
    }
    public class RepuestosQueryService : IRepuestosQueryService
    {
        private readonly Context _context;

        public RepuestosQueryService(Context context)
        {
            _context = context;
        }
        
        public async Task<DataCollection<RepuestosDTO>> GetAllAsync(int page, int take, IEnumerable<long> repuestos = null, bool order = false)
        {
            try
            {
                if (!order)
                {
                    var orderBy = await _context.Repuestos
                    .Where(x => repuestos == null || repuestos.Contains(x.IdRepuesto))
                    .OrderByDescending(x => x.IdRepuesto)
                    .GetPagedAsync(page, take);
                    return orderBy.MapTo<DataCollection<RepuestosDTO>>();
                }
                var collection = await _context.Repuestos
                .Where(x => repuestos == null || repuestos.Contains(x.IdRepuesto))
                .OrderByDescending(x => x.IdRepuesto)
                .GetPagedAsync(page, take);
                if (!collection.HasItems)
                {
                    throw new EmptyCollectionException("No se encontró ningun Item en la Base de Datos");
                }
                return collection.MapTo<DataCollection<RepuestosDTO>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<RepuestosByIdDTO> GetAsync(long id)
        {
            try
            {
                var unidad = await _context.Repuestos.SingleAsync(x => x.IdRepuesto == id);

                if (await _context.Repuestos.SingleAsync(x => x.IdRepuesto == id) == null)
                {
                    throw new EmptyCollectionException("Error al obtener el Repuesto, el Repuesto con id" + " " + id + " " + "no existe");
                }
                return unidad.MapTo<RepuestosByIdDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<UpdateRepuestosDTO> PutAsync(UpdateRepuestosDTO repuesto, long id)
        {
            if (repuesto.IdUnidadDeMedida == 0)
            {
                throw new EmptyCollectionException("La Unidad de Medida es Obligatoria");
            }
            var repuestos = await _context.Repuestos.FindAsync(id);


            repuestos.Detalle = repuesto.Detalle;
            repuestos.Precio = repuesto.Precio;
            repuestos.UnidadMedida = repuesto.UnidadMedida;
            repuestos.Tipo = repuesto.Tipo;
            repuestos.Marca = repuesto.Marca;
            repuestos.Obs = repuesto.Obs;
            repuestos.CodRepuesto = repuesto.CodRepuesto;
            repuestos.IdMarcaRepuesto = repuesto.IdMarcaRepuesto;
            repuestos.IdUnidadDeMedida = repuesto.IdUnidadDeMedida;
            repuestos.NroParte = repuesto.NroParte;
            repuestos.NroSerie = repuesto.NroSerie;
            repuestos.Descripcion = repuesto.Descripcion;
            repuestos.Importado = repuesto.Importado;
            repuestos.IdSubRubroRepuesto = repuesto.IdSubRubroRepuesto;
            repuestos.IdProveedor = repuesto.IdProveedor;
            repuestos.StockMinimo = repuesto.StockMinimo;
            repuestos.StockMaximo = repuesto.StockMaximo;
            repuestos.PuntoPedido = repuesto.PuntoPedido;
            repuestos.Baja = repuesto.Baja;
            repuestos.PorcentajeGananciaAplicada = repuesto.PorcentajeGananciaAplicada;
            repuestos.IdCodigo = repuesto.IdCodigo;
            repuestos.IdRepuestoCwa = repuesto.IdRepuestoCwa;
            repuestos.CodCwa = repuesto.CodCwa;
            repuestos.TipoCwa = repuesto.TipoCwa;
            repuestos.Imagen = repuesto.Imagen;
            repuestos.NombreImagen = repuesto.NombreImagen;
            repuestos.CodArticuloTango = repuesto.CodArticuloTango;
            repuestos.TiempoReposicion = repuesto.TiempoReposicion;
            repuestos.HabilitarEmailStock = repuesto.HabilitarEmailStock;
            repuestos.CodigoBarras = repuesto.CodigoBarras;
            repuestos.TipoValorStock = repuesto.TipoValorStock;


            await _context.SaveChangesAsync();

            return repuestos.MapTo<UpdateRepuestosDTO>();
        }
        public async Task<RepuestosDTO> DeleteAsync(long id)
        {
            var repuesto = await _context.Repuestos.FindAsync(id);
            if (repuesto == null)
            {
                throw new EmptyCollectionException("Error al eliminar el Repuesto, el Repuesto con id" + " " + id + " " + "no existe");
            }

            _context.Repuestos.Remove(repuesto);

            await _context.SaveChangesAsync();
            return repuesto.MapTo<RepuestosDTO>();
        }
        public async Task<UpdateRepuestosDTO> CreateAsync(UpdateRepuestosDTO repuestos)
        {
            try
            {
                var newRepuesto = new Repuestos()
                {
                    Detalle = repuestos.Detalle,
                    Precio = repuestos.Precio,
                    UnidadMedida = repuestos.UnidadMedida,
                    Tipo = repuestos.Tipo,
                    Marca = repuestos.Marca,
                    Obs = repuestos.Obs,
                    CodRepuesto = repuestos.CodRepuesto,
                    IdMarcaRepuesto = repuestos.IdMarcaRepuesto,
                    IdUnidadDeMedida = repuestos.IdUnidadDeMedida,
                    NroParte = repuestos.NroParte,
                    NroSerie = repuestos.NroSerie,
                    Descripcion = repuestos.Descripcion,
                    Importado = repuestos.Importado,
                    IdSubRubroRepuesto = repuestos.IdSubRubroRepuesto,
                    IdProveedor = repuestos.IdProveedor,
                    StockMinimo = repuestos.StockMinimo,
                    StockMaximo = repuestos.StockMaximo,
                    PuntoPedido = repuestos.PuntoPedido,
                    Baja = repuestos.Baja,
                    PorcentajeGananciaAplicada = repuestos.PorcentajeGananciaAplicada,
                    IdCodigo = repuestos.IdCodigo,
                    IdRepuestoCwa = repuestos.IdRepuestoCwa,
                    CodCwa = repuestos.CodCwa,
                    TipoCwa = repuestos.TipoCwa,
                    Imagen = repuestos.Imagen,
                    NombreImagen = repuestos.NombreImagen,
                    CodArticuloTango = repuestos.CodArticuloTango,
                    TiempoReposicion = repuestos.TiempoReposicion,
                    HabilitarEmailStock = repuestos.HabilitarEmailStock,
                    CodigoBarras = repuestos.CodigoBarras,
                    TipoValorStock = repuestos.TipoValorStock,
            };
                await _context.Repuestos.AddAsync(newRepuesto);

                await _context.SaveChangesAsync();
                return newRepuesto.MapTo<UpdateRepuestosDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el Repuesto");
            }

        }
    }
}
