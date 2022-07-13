using Common.Collection;
using DATA.DTOS.Updates;
using Microsoft.EntityFrameworkCore;
using PERSISTENCE;
using Service.Queries.DTOS;
using Services.Common.Mapping;
using Services.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Queries
{
    public interface IEspecialidadesQueryService
    {
        Task<DataCollection<EspecialidadesDTO>> GetAllAsync(int page, int take, IEnumerable<int> Especialidades = null);
        Task<EspecialidadesDTO> GetAsync(int id);
        Task<UpdateEspecialidadesDTO> PutAsync(UpdateEspecialidadesDTO Especialidad, int it);
        Task<EspecialidadesDTO> DeleteAsync(int id);
    }

    public class EspecialidadesQueryService : IEspecialidadesQueryService
    {
        private readonly Context _context;

        public EspecialidadesQueryService(Context context)
        {
            _context = context;
        }

        public async Task<DataCollection<EspecialidadesDTO>> GetAllAsync(int page, int take, IEnumerable<int> especialidades = null)
        {
            try
            {
                var collection = await _context.Especialidades
                .Where(x => especialidades == null || especialidades.Contains(x.IdEspecialidad))
                .OrderByDescending(x => x.IdEspecialidad)
                .GetPagedAsync(page, take);

                return collection.MapTo<DataCollection<EspecialidadesDTO>>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las especialidades");
            }

        }

        public async Task<EspecialidadesDTO> GetAsync(int id)
        {
            try
            {
                return (await _context.Especialidades.SingleAsync(x => x.IdEspecialidad == id)).MapTo<EspecialidadesDTO>();
            }
            catch (Exception ex)
            {
                if (_context.Especialidades.SingleAsync(x => x.IdEspecialidad == id) == null)
                {
                    throw new Exception("Error al obtener el especialidad, la especialidad con id" + " " + id + " " + "no existe");
                }
                throw new Exception("Error al obtener el especialidad");
            }

        }
        public async Task<UpdateEspecialidadesDTO> PutAsync(UpdateEspecialidadesDTO Especialidad, int id)
        {
            if (_context.Especialidades.SingleAsync(x => x.IdEspecialidad == id).Result == null)
            {
                throw new Exception("El especialidad con id" + " " + id + " " + ",no existe");
            }
            var especialidad = await _context.Especialidades.SingleAsync(x => x.IdEspecialidad == id);
            especialidad.Descripcion = Especialidad.Descripcion;
            especialidad.Obs = Especialidad.Obs;

            await _context.SaveChangesAsync();

            return Especialidad.MapTo<UpdateEspecialidadesDTO>();
        }
        public async Task<EspecialidadesDTO> DeleteAsync(int id)
        {
            try
            {
                var especialidad = await _context.Especialidades.SingleAsync(x => x.IdEspecialidad == id);
                _context.Especialidades.Remove(especialidad);
                await _context.SaveChangesAsync();
                return especialidad.MapTo<EspecialidadesDTO>();
            }
            catch (Exception ex)
            {
                if (_context.Especialidades.SingleAsync(x => x.IdEspecialidad == id) == null)
                {
                    throw new Exception("Error al eliminar especialidad, el especialidad con id" + " " + id + " " + "no existe");
                }
                throw new Exception("Error al eliminar el especialidad");
            }

        }

    }




}
