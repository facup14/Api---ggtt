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
    public interface IVariablesUnidadesQueryService
    {
        Task<DataCollection<VariablesUnidadesDTO>> GetAllAsync(int page, int take, IEnumerable<int> unidadesMedida = null, bool order = false);
        Task<VariablesUnidadesDTO> GetAsync(int id);
        Task<UpdateVariableUnidadDTO> PutAsync(UpdateVariableUnidadDTO titulo, int id);
        Task<VariablesUnidadesDTO> DeleteAsync(int id);
        Task<GetResponse> CreateAsync(UpdateVariableUnidadDTO variables);
    }
    public class VariablesUnidadesQueryService : IVariablesUnidadesQueryService
    {
        private readonly Context _context;
        public VariablesUnidadesQueryService(Context context)
        {
            _context = context;
        }
        public async Task<DataCollection<VariablesUnidadesDTO>> GetAllAsync(int page, int take, IEnumerable<int> unidadesMedida = null, bool order = false)
        {
            try
            {
                if (!order)
                {
                    var orderBy = await _context.VariablesUnidades
                    .Where(x => unidadesMedida == null || unidadesMedida.Contains(x.IdVariableUnidad))
                    .OrderBy(x => x.IdVariableUnidad)
                    .GetPagedAsync(page, take);
                    return orderBy.MapTo<DataCollection<VariablesUnidadesDTO>>();
                }
                var collection = await _context.VariablesUnidades
                .Where(x => unidadesMedida == null || unidadesMedida.Contains(x.IdVariableUnidad))
                .OrderByDescending(x => x.IdVariableUnidad)
                .GetPagedAsync(page, take);
                if (!collection.HasItems)
                {
                    throw new EmptyCollectionException("No se encontró ningun Item en la Base de Datos");
                }
                return collection.MapTo<DataCollection<VariablesUnidadesDTO>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<VariablesUnidadesDTO> GetAsync(int id)
        {
            try
            {
                var variableUnidad = await _context.VariablesUnidades.FindAsync(id);

                if (await _context.VariablesUnidades.FindAsync(id) == null)
                {
                    throw new EmptyCollectionException("Error al obtener la Variable de Unidad, la Variable con id" + " " + id + " " + "no existe");
                }
                return variableUnidad.MapTo<VariablesUnidadesDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<UpdateVariableUnidadDTO> PutAsync(UpdateVariableUnidadDTO titulo, int id)
        {
            if (await _context.VariablesUnidades.FindAsync(id) == null)
            {
                throw new EmptyCollectionException("Error al obtener La Unidad de Medida, la Unidad con id" + " " + id + " " + "no existe");
            }
            var updateVariable = await _context.VariablesUnidades.FindAsync(id);

            updateVariable.Nombre = titulo.Nombre;


            await _context.SaveChangesAsync();

            return titulo.MapTo<UpdateVariableUnidadDTO>();
        }
        public async Task<VariablesUnidadesDTO> DeleteAsync(int id)
        {
            var variableUnidad = await _context.VariablesUnidades.FindAsync(id);
            if (variableUnidad == null)
            {
                throw new EmptyCollectionException("Error al eliminar el Titulo, el Titulo con id" + " " + id + " " + "no existe");
            }

            _context.VariablesUnidades.Remove(variableUnidad);

            await _context.SaveChangesAsync();

            return variableUnidad.MapTo<VariablesUnidadesDTO>();
        }
        public async Task<GetResponse> CreateAsync(UpdateVariableUnidadDTO variables)
        {
            try
            {
                if (variables.Nombre is null || variables.Nombre == "")
                {
                    var ex = new EmptyCollectionException("Debe ingresar el Nombre");

                    return new GetResponse()
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Message = ex.ToString(),
                        Result = null
                    };
                }
                var newVariables = new VariablesUnidades()
                {
                    Nombre = variables.Nombre,
                };
                await _context.VariablesUnidades.AddAsync(newVariables);

                await _context.SaveChangesAsync();
                var nuevaVariable =  newVariables.MapTo<UpdateVariableUnidadDTO>();

                return new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success",
                    Result = nuevaVariable
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la Variable");
            }

        }
    }
}
