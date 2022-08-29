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
    public interface ITalleresQueryService
    {
        Task<DataCollection<TalleresDTO>> GetAllAsync(int page, int take, IEnumerable<long> equipamiento = null, bool order = false);
        Task<TalleresDTO> GetAsync(long id);
        Task<UpdateTalleresDTO> PutAsync(UpdateTalleresDTO taller, long id);
        Task<TalleresDTO> DeleteAsync(long id);
        Task<GetResponse> CreateAsync(UpdateTalleresDTO taller);
    }
    public class TalleresQueryService : ITalleresQueryService
    {
        private readonly Context _context;
        public TalleresQueryService(Context context)
        {
            _context = context;
        }
        public async Task<DataCollection<TalleresDTO>> GetAllAsync(int page, int take, IEnumerable<long> equipamiento = null, bool order = false)
        {
            try
            {
                if (!order)
                {
                    var orderBy = await _context.Talleres
                    .Where(x => equipamiento == null || equipamiento.Contains(x.IdTaller))
                    .OrderBy(x => x.IdTaller)
                    .GetPagedAsync(page, take);
                    return orderBy.MapTo<DataCollection<TalleresDTO>>();
                }
                var collection = await _context.Talleres
                .Where(x => equipamiento == null || equipamiento.Contains(x.IdTaller))
                .OrderByDescending(x => x.IdTaller)
                .GetPagedAsync(page, take);
                if (!collection.HasItems)
                {
                    throw new EmptyCollectionException("No se encontró ningun Item en la Base de Datos");
                }
                return collection.MapTo<DataCollection<TalleresDTO>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<TalleresDTO> GetAsync(long id)
        {
            try
            {
                var unidad = await _context.Talleres.FindAsync(id);

                if (await _context.Talleres.FindAsync(id) == null)
                {
                    throw new EmptyCollectionException("Error al obtener el Taller, el Taller con id" + " " + id + " " + "no existe");
                }
                return unidad.MapTo<TalleresDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<UpdateTalleresDTO> PutAsync(UpdateTalleresDTO taller, long id)
        {
            if (await _context.Talleres.FindAsync(id) == null)
            {
                throw new EmptyCollectionException("Error al obtener el Taller, el Taller con id" + " " + id + " " + "no existe");
            }
            if (taller.NombreTaller == "" || taller.NombreTaller is null)
            {
                throw new EmptyCollectionException("Debe colocar el Nombre del Taller");
            }
            var updateTaller = await _context.Talleres.FindAsync(id);

            updateTaller.NombreTaller = taller.NombreTaller;
            updateTaller.Obs = taller.Obs ?? updateTaller.Obs;
            updateTaller.Direccion = taller.Direccion ?? updateTaller.Direccion;
            updateTaller.Mail = taller.Mail ?? updateTaller.Mail;
            updateTaller.JefeAsignado = taller.JefeAsignado ?? updateTaller.JefeAsignado;
            updateTaller.Telefonos = taller.Telefonos ?? updateTaller.Telefonos;
            updateTaller.NombreRecibe = taller.NombreRecibe ?? updateTaller.NombreRecibe;
            updateTaller.RutaLogo = taller.RutaLogo ?? updateTaller.RutaLogo;
            updateTaller.RutaInstalador = taller.RutaInstalador ?? updateTaller.RutaInstalador;
            updateTaller.IdLugarAbastecimiento = taller.IdLugarAbastecimiento ?? updateTaller.IdLugarAbastecimiento;
            updateTaller.RutaIcono = taller.RutaIcono ?? updateTaller.RutaIcono;
            updateTaller.FondoPantalla = taller.FondoPantalla ?? updateTaller.FondoPantalla;
            updateTaller.NombreEmpresa = taller.NombreEmpresa ?? updateTaller.NombreEmpresa;
            updateTaller.NombreProvincia = taller.NombreProvincia ?? updateTaller.NombreProvincia;
            updateTaller.Slogan = taller.Slogan ?? updateTaller.Slogan;
            updateTaller.IdRecibidoPor = taller.IdRecibidoPor ?? updateTaller.IdRecibidoPor;
            updateTaller.IdSolicitadoPor = taller.IdSolicitadoPor ?? updateTaller.IdSolicitadoPor;
            updateTaller.RecibePersona = taller.RecibePersona ?? updateTaller.RecibePersona;
            updateTaller.SolicitaPersona = taller.SolicitaPersona ?? updateTaller.SolicitaPersona;
            updateTaller.CargaAutomaticaCombustible = taller.CargaAutomaticaCombustible ?? updateTaller.CargaAutomaticaCombustible;
            updateTaller.RutaCargaAutomatica = taller.RutaCargaAutomatica ?? updateTaller.RutaCargaAutomatica;
            updateTaller.UserIdCombustible = taller.UserIdCombustible ?? updateTaller.UserIdCombustible;
            updateTaller.PasswordCombustible = taller.PasswordCombustible ?? updateTaller.PasswordCombustible;
            updateTaller.IdLocalidad = taller.IdLocalidad ?? updateTaller.IdLocalidad;


            await _context.SaveChangesAsync();

            return taller.MapTo<UpdateTalleresDTO>();
        }
        public async Task<TalleresDTO> DeleteAsync(long id)
        {
            var taller = await _context.Talleres.FindAsync(id);
            if (taller is null)
            {
                throw new EmptyCollectionException("Error al eliminar el Taller, el Taller con id" + " " + id + " " + "no existe");
            }

            _context.Talleres.Remove(taller);

            await _context.SaveChangesAsync();
            return taller.MapTo<TalleresDTO>();
        }
        public async Task<GetResponse> CreateAsync(UpdateTalleresDTO taller)
        {
            try
            {
                if (taller.NombreTaller is null || taller.NombreTaller == "")
                {
                    var ex = new EmptyCollectionException("Debe ingresar el Nombre del Taller");

                    return new GetResponse()
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Message = ex.ToString(),
                        Result = null
                    };
                }
                var newTaller = new Talleres()
                {
                      NombreTaller = taller.NombreTaller,
                      Obs = taller.Obs,
                      Direccion = taller.Direccion,
                      Mail = taller.Mail,
                      JefeAsignado = taller.JefeAsignado,
                      Telefonos = taller.Telefonos,
                      NombreRecibe = taller.NombreRecibe,
                      RutaLogo = taller.RutaLogo,
                      RutaInstalador = taller.RutaInstalador,
                      IdLugarAbastecimiento = taller.IdLugarAbastecimiento,
                      RutaIcono = taller.RutaIcono,
                      FondoPantalla = taller.FondoPantalla,
                      NombreEmpresa = taller.NombreEmpresa,
                      NombreProvincia = taller.NombreProvincia,
                      Slogan = taller.Slogan,
                      IdRecibidoPor = taller.IdRecibidoPor,
                      IdSolicitadoPor = taller.IdSolicitadoPor,
                      RecibePersona = taller.RecibePersona,
                      SolicitaPersona = taller.SolicitaPersona,
                      CargaAutomaticaCombustible = taller.CargaAutomaticaCombustible,
                      RutaCargaAutomatica = taller.RutaCargaAutomatica,
                      UserIdCombustible = taller.UserIdCombustible,
                      PasswordCombustible = taller.PasswordCombustible,
                      IdLocalidad = taller.IdLocalidad,
                };
                await _context.Talleres.AddAsync(newTaller);

                await _context.SaveChangesAsync();
                var nuevoTaller =  newTaller.MapTo<UpdateTalleresDTO>();

                return new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success",
                    Result = nuevoTaller
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el Taller");
            }

        }
    }
}
