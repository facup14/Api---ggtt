using Common.Collection;
using DATA.DTOS;
using DATA.DTOS.Updates;
using DATA.Extensions;
using Microsoft.EntityFrameworkCore;
using PERSISTENCE;
using Services.Common.Mapping;
using Services.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATA.Models;


namespace Service.Queries
{
    public interface IRubrosQueryService
    {
        Task<DataCollection<RubrosDTO>> GetAllAsync(int page, int take, IEnumerable<long> rubros = null, bool order = false);
        Task<RubrosDTO> GetAsync(long id);
        Task<UpdateRubroDTO> PutAsync(UpdateRubroDTO choferDto, long id);
        Task<RubrosDTO> DeleteAsync(long id);
        Task<UpdateRubroDTO> CreateAsync(UpdateRubroDTO rubro);
    }

    public class RubrosQueryService : IRubrosQueryService
    {
        private readonly Context _context;

        public RubrosQueryService(Context context)
        {
            _context = context;
        }

        public async Task<DataCollection<RubrosDTO>> GetAllAsync(int page, int take, IEnumerable<long> rubros = null, bool order = false)
        {
            try
            {
                if (!order)
                {
                    var orderBy = await _context.Rubros
                    .Where(x => rubros == null || rubros.Contains(x.IdRubro))
                    .OrderBy(x => x.IdRubro)
                    .GetPagedAsync(page, take);
                    return orderBy.MapTo<DataCollection<RubrosDTO>>();
                }
                var collection = await _context.Rubros
                .Where(x => rubros == null || rubros.Contains(x.IdRubro))
                .OrderByDescending(x => x.IdRubro)
                .GetPagedAsync(page, take);
                if (!collection.HasItems)
                {
                    throw new EmptyCollectionException("No se encontró ningun Item en la Base de Datos");
                }
                return collection.MapTo<DataCollection<RubrosDTO>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<RubrosDTO> GetAsync(long id)
        {
            try
            {
                var rubro = await _context.Rubros.FindAsync(id);

                if (await _context.Rubros.FindAsync(id) == null)
                {
                    throw new EmptyCollectionException("Error al obtener el Rubro, el Rubro con id" + " " + id + " " + "no existe");
                }
                return rubro.MapTo<RubrosDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<UpdateRubroDTO> PutAsync(UpdateRubroDTO RubroDTO, long id)
        {
            if (RubroDTO.Descripcion == "")
            {
                throw new EmptyCollectionException("La descripcion del Rubro es obligatoria");
            }
            if (await _context.Rubros.FindAsync(id) is null)
            {
                throw new EmptyCollectionException("Error al actualizar el Rubro, el Rubro con id" + " " + id + " " + "no existe");
            }

            var rubro = await _context.Rubros.FindAsync(id);

            rubro.Descripcion = RubroDTO.Descripcion;
            rubro.Obs = RubroDTO.Obs;
            rubro.IdMecanico = RubroDTO.IdMecanico;


            await _context.SaveChangesAsync();

            return RubroDTO.MapTo<UpdateRubroDTO>();
        }
        public async Task<RubrosDTO> DeleteAsync(long id)
        {
            var rubro = await _context.Rubros.FindAsync(id);
            if (rubro == null)
            {
                throw new EmptyCollectionException("Error al eliminar el Rubro, el Rubro con id" + " " + id + " " + "no existe");
            }

            _context.Rubros.Remove(rubro);

            await _context.SaveChangesAsync();
            return rubro.MapTo<RubrosDTO>();
        }

        public async Task<UpdateRubroDTO> CreateAsync(UpdateRubroDTO rubro)
        {
            try
            {
                var newRubro = new Rubros()
                {
        
                    Descripcion = rubro.Descripcion,
                    IdMecanico = rubro.IdMecanico,
                    Obs = rubro.Obs,
                };
                await _context.Rubros.AddAsync(newRubro);

                await _context.SaveChangesAsync();
                return newRubro.MapTo<UpdateRubroDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el Rubro");
            }

        }
    }
}
