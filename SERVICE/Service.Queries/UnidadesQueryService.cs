using Common.Collection;
using DATA.DTOS;
using DATA.Errors;
using DATA.Extensions;
using DATA.Models;
using Microsoft.EntityFrameworkCore;
using PERSISTENCE;
using Service.Queries.DTOS;
using Services.Common.Mapping;
using Services.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Service.Queries
{/// <summary>
/// //////////////////////////////////////////Se utilizan Queries para hacer peticiones que no modifiquen la base de datos.(Excepto el PUT)
/// </summary>
    public interface IUnidadesQueryService
    {
        Task<DataCollection<UnidadesDTO>> GetAllAsync(int page, int take, IEnumerable<long> unidades = null, bool order = false);
        Task<DATA.DTOS.UnidadesByIdDTO> GetAsync(long id);
        Task<UpdateUnidadDTO> PutAsync(UpdateUnidadDTO unidad, long it);
        Task<UnidadesDTO> DeleteAsync(long id);
        Task<GetResponse> CreateAsync(UpdateUnidadDTO unidad);
    }
    public class UnidadesQueryService : IUnidadesQueryService
    {
        private readonly Context _context;

        public UnidadesQueryService(Context context)
        {
            _context = context;
        }

        public async Task<DataCollection<UnidadesDTO>> GetAllAsync(int page, int take, IEnumerable<long> unidades = null, bool order = false)
        {
            try
            {
                if (!order)
                {
                    var orderBy = await _context.Unidades
                    .Where(x => unidades == null || unidades.Contains(x.IdUnidad))
                    .OrderBy(x => x.IdUnidad)
                    .GetPagedAsync(page, take);
                    return orderBy.MapTo<DataCollection<UnidadesDTO>>();
                }
                var collection = await _context.Unidades
                .Where(x => unidades == null || unidades.Contains(x.IdUnidad))
                .OrderByDescending(x => x.IdUnidad)
                .GetPagedAsync(page, take);
                if (!collection.HasItems)
                {
                    throw new EmptyCollectionException("No se encontró ningun Item en la Base de Datos");
                }
                return collection.MapTo<DataCollection<UnidadesDTO>>();
            }catch(Exception ex)
            {
                throw ex;
            }
            
        }
        public async Task<UnidadesByIdDTO> GetAsync(long id)
        {
            try
            {
                var unidad = await _context.Unidades.FindAsync(id);
                                
                if (await _context.Unidades.FindAsync(id) == null)
                {
                    throw new EmptyCollectionException("Error al obtener la unidad, la unidad con id" + " " + id + " " + "no existe");
                }
                return unidad.MapTo<UnidadesByIdDTO>();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        public async Task<UpdateUnidadDTO> PutAsync(UpdateUnidadDTO unidad, long id)
        {
            if(unidad.idEstadoUnidad == 0 && unidad.idModelo == 0 && unidad.idSituacionUnidad == 0)
            {
                throw new EmptyCollectionException("El Estado, Modelo y Situación de la Unidad son Obligatorios");
            }
            if (unidad.idEstadoUnidad == 0)
            {
                throw new EmptyCollectionException("El Estado de la Unidad es Obligatorio");
            }
            if (unidad.idModelo == 0)
            {
                throw new EmptyCollectionException("El Modelo de la Unidad es Obligatorio");
            }
            if (unidad.idSituacionUnidad == 0)
            {
                throw new EmptyCollectionException("La Situación de la Unidad es Obligatoria");
            }
            if (await _context.Unidades.FindAsync(id) is null)
            {
                throw new EmptyCollectionException("Error al obtener la unidad, la unidad con id" + " " + id + " " + "no existe");
            }
            var unidade = await _context.Unidades.FindAsync(id);

                unidade.NroUnidad = unidad.NroUnidad;
                unidade.Dominio = unidad.Dominio;
                unidade.IntExt = unidad.IntExt;
                unidade.KmAceptadosDesfazados = unidad.KmAceptadosDesfazados;
                unidade.HsAceptadasDesfazadas = unidad.HsAceptadasDesfazadas;
                unidade.Motor = unidad.Motor;
                unidade.Chasis = unidad.Chasis;
                unidade.AñoModelo = unidad.AñoModelo;
                unidade.Descripcion = unidad.Descripcion;
                unidade.Foto = unidad.Foto;
                unidade.PromedioConsumo = unidad.PromedioConsumo;
                unidade.UnidadMedida = unidad.UnidadMedida;
                unidade.IdTipoCombustible = unidad.IdTipoCombustible;
                unidade.UnidadMedTrabajo = unidad.UnidadMedTrabajo;
                unidade.Capacidad = unidad.Capacidad;
                unidade.IdUnidadDeMedida = unidad.IdUnidadDeMedida;
                unidade.Obs = unidad.Obs;
                unidade.Tacografo = unidad.Tacografo;
                unidade.Radicacion = unidad.Radicacion;
                unidade.Titular = unidad.Titular;
                unidade.AcreedorPrendario = unidad.AcreedorPrendario;
                unidade.MarcaTacografo = unidad.MarcaTacografo;
                unidade.CodigoRadio = unidad.CodigoRadio;
                unidade.CodigoLlave = unidad.CodigoLlave;
                unidade.IdModeloChasis = unidad.IdModeloChasis;
                unidade.IdTraza = unidad.IdTraza;
                unidade.IdEsquema = unidad.IdEsquema;
                unidade.TieneConceptosSinRealizar = unidad.TieneConceptosSinRealizar;
                unidade.IdTipoLlanta = unidad.IdTipoLlanta;
                unidade.Potencia = unidad.Potencia;
                unidade.HsKmsDiaTrabajo = unidad.HsKmsDiaTrabajo;
                unidade.LtsDiaTrabajo = unidad.LtsDiaTrabajo;
                unidade.LitrosxTraza = unidad.LitrosxTraza;
                unidade.idNombreEquipamiento = unidad.idNombreEquipamiento;
                unidade.HabilitaOtraUnidadMedida = unidad.HabilitaOtraUnidadMedida;
                unidade.idEstadoUnidad = unidad.idEstadoUnidad;
                unidade.idModelo = unidad.idModelo;
                unidade.idSituacionUnidad = unidad.idSituacionUnidad;


                await _context.SaveChangesAsync();
                
            return unidad.MapTo<UpdateUnidadDTO>();
        }
        public async Task<UnidadesDTO> DeleteAsync(long id)
        {
            var unidade = await _context.Unidades.FindAsync(id);
            if (unidade == null)
            {
               throw new EmptyCollectionException("Error al eliminar la unidad, la unidad con id" + " " + id + " " + "no existe");
            }
            
            _context.Unidades.Remove(unidade);
                
            await _context.SaveChangesAsync();
            return unidade.MapTo<UnidadesDTO>();                        
        }
        public async Task<GetResponse> CreateAsync(UpdateUnidadDTO unidad)
        {
            try
            {
                if (unidad.NroUnidad is null || unidad.NroUnidad == "")
                {
                    var ex = new EmptyCollectionException("Debe ingresar el Número de la Unidad");

                    return new GetResponse()
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Message = ex.ToString(),
                        Result = null
                    };
                }
                if (unidad.idEstadoUnidad == 0 && unidad.idModelo == 0 && unidad.idSituacionUnidad == 0)
                {
                    var ex = new EmptyCollectionException("El Estado de la Unidad, Modelo y SItuación de la Unidad son obligatorios");

                    return new GetResponse()
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Message = ex.ToString(),
                        Result = null
                    };
                }
                var newUnidad = new Unidades()
                {
                    NroUnidad = unidad.NroUnidad,
                    Dominio = unidad.Dominio,
                    IntExt = unidad.IntExt,
                    KmAceptadosDesfazados = unidad.KmAceptadosDesfazados,
                    HsAceptadasDesfazadas = unidad.HsAceptadasDesfazadas,
                    Motor = unidad.Motor,
                    Chasis = unidad.Chasis,
                    AñoModelo = unidad.AñoModelo,
                    Descripcion = unidad.Descripcion,
                    Foto = unidad.Foto,
                    PromedioConsumo = unidad.PromedioConsumo,
                    UnidadMedida = unidad.UnidadMedida,
                    IdTipoCombustible = unidad.IdTipoCombustible,
                    UnidadMedTrabajo = unidad.UnidadMedTrabajo,
                    Capacidad = unidad.Capacidad,
                    IdUnidadDeMedida = unidad.IdUnidadDeMedida,
                    Obs = unidad.Obs,
                    Tacografo = unidad.Tacografo,
                    Radicacion = unidad.Radicacion,
                    Titular = unidad.Titular,
                    AcreedorPrendario = unidad.AcreedorPrendario,
                    MarcaTacografo = unidad.MarcaTacografo,
                    CodigoRadio = unidad.CodigoRadio,
                    CodigoLlave = unidad.CodigoLlave,
                    IdModeloChasis = unidad.IdModeloChasis,
                    IdTraza = unidad.IdTraza,
                    IdEsquema = unidad.IdEsquema,
                    TieneConceptosSinRealizar = unidad.TieneConceptosSinRealizar,
                    IdTipoLlanta = unidad.IdTipoLlanta,
                    Potencia = unidad.Potencia,
                    HsKmsDiaTrabajo = unidad.HsKmsDiaTrabajo,
                    LtsDiaTrabajo = unidad.LtsDiaTrabajo,
                    LitrosxTraza = unidad.LitrosxTraza,
                    idNombreEquipamiento = unidad.idNombreEquipamiento,
                    HabilitaOtraUnidadMedida = unidad.HabilitaOtraUnidadMedida,
                    idEstadoUnidad = unidad.idEstadoUnidad,
                    idModelo = unidad.idModelo,
                    idSituacionUnidad = unidad.idSituacionUnidad,
                };
                await _context.Unidades.AddAsync(newUnidad);

                await _context.SaveChangesAsync();
                var nuevaUnidad =  newUnidad.MapTo<UpdateUnidadDTO>();

                return new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success",
                    Result = nuevaUnidad
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la Unidad");
            }

        }

    }
    
}
