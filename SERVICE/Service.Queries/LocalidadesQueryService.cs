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
    public interface ILocalidadQueryService
    {
        Task<DataCollection<LocalidadesDTO>> GetAllAsync(int page, int take, IEnumerable<long> titulos = null, bool order = false);
        Task<LocalidadesDTO> GetAsync(long id);
        Task<UpdateLocalidadesDTO> PutAsync(UpdateLocalidadesDTO localidad, long id);
        Task<LocalidadesDTO> DeleteAsync(long id);
        Task<GetResponse> CreateAsync(UpdateLocalidadesDTO localidades);
    }
    public class LocalidadesQueryService : ILocalidadQueryService
    {
        private readonly Context _context;
        public LocalidadesQueryService(Context context)
        {
            _context = context;
        }
        public async Task<DataCollection<LocalidadesDTO>> GetAllAsync(int page, int take, IEnumerable<long> titulos = null, bool order = false)
        {
            try
            {
                if (!order)
                {
                    var orderBy = await _context.Localidades
                    .Where(x => titulos == null || titulos.Contains(x.IdLocalidad))
                    .OrderBy(x => x.IdLocalidad)
                    .GetPagedAsync(page, take);
                    return orderBy.MapTo<DataCollection<LocalidadesDTO>>();
                }
                var collection = await _context.Localidades
                .Where(x => titulos == null || titulos.Contains(x.IdLocalidad))
                .OrderByDescending(x => x.IdLocalidad)
                .GetPagedAsync(page, take);
                if (!collection.HasItems)
                {
                    throw new EmptyCollectionException("No se encontró ningun Item en la Base de Datos");
                }
                return collection.MapTo<DataCollection<LocalidadesDTO>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<LocalidadesDTO> GetAsync(long id)
        {
            try
            {
                var localidad = await _context.Localidades.FindAsync(id);

                if (await _context.Localidades.FindAsync(id) == null)
                {
                    throw new EmptyCollectionException("Error al obtener la Localidad, la Localidad con id" + " " + id + " " + "no existe");
                }
                return localidad.MapTo<LocalidadesDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<UpdateLocalidadesDTO> PutAsync(UpdateLocalidadesDTO localidad, long id)
        {
            if (await _context.Localidades.FindAsync(id) == null)
            {
                throw new EmptyCollectionException("Error al actualizar la Localidad, la Localidad con id" + " " + id + " " + "no existe");
            }
            if (localidad.idProvincia == 0)
            {
                throw new EmptyCollectionException("Debe colocar la Provincia");
            }
            var updateLocalidad = await _context.Localidades.FindAsync(id);

            updateLocalidad.Localidad = localidad.Localidad;
            updateLocalidad.CodigoPostal = localidad.CodigoPostal;
            updateLocalidad.idProvincia = localidad.idProvincia;


            await _context.SaveChangesAsync();

            return localidad.MapTo<UpdateLocalidadesDTO>();
        }
        public async Task<LocalidadesDTO> DeleteAsync(long id)
        {
            var localidad = await _context.Localidades.FindAsync(id);
            if (localidad == null)
            {
                throw new EmptyCollectionException("Error al eliminar la Localidad, la Localidad con id" + " " + id + " " + "no existe");
            }

            _context.Localidades.Remove(localidad);

            await _context.SaveChangesAsync();

            return localidad.MapTo<LocalidadesDTO>();
        }
        public async Task<GetResponse> CreateAsync(UpdateLocalidadesDTO localidades)
        {
            try
            {
                if (localidades.Localidad is null || localidades.Localidad == "")
                {
                    var ex = new EmptyCollectionException("Debe ingresar una Localidad");

                    return new GetResponse()
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Message = ex.ToString(),
                        Result = null
                    };
                }
                var newLocalidad = new Localidades()
                {
                    Localidad = localidades.Localidad,
                    CodigoPostal = localidades.CodigoPostal,
                    idProvincia = localidades.idProvincia,
                };
                await _context.Localidades.AddAsync(newLocalidad);

                await _context.SaveChangesAsync();
                var nuevaLocalidad =  newLocalidad.MapTo<UpdateLocalidadesDTO>();

                return new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success",
                    Result = nuevaLocalidad
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el Grupo");
            }

        }
    }
}
