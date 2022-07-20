﻿using Common.Collection;
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

namespace Service.Queries
{
    public interface IMecanicoQueryService
    {
        Task<DataCollection<MecanicosDTO>> GetAllAsync(int page, int take, IEnumerable<long> mecanicos = null);
        Task<MecanicosDTO> GetAsync(long id);
        Task<UpdateMecanicoDTO> PutAsync(UpdateMecanicoDTO mecanico, long it);
        Task<MecanicosDTO> DeleteAsync(long id);
    }
    public class MecanicoQueryService : IMecanicoQueryService
    {
        private readonly Context _context;

        public MecanicoQueryService(Context context)
        {
            _context = context;
        }

        public async Task<DataCollection<MecanicosDTO>> GetAllAsync(int page, int take, IEnumerable<long> mecanicos = null)
        {
            try
            {
                var collection = await _context.Mecanicos
                .Where(x => mecanicos == null || mecanicos.Contains(x.IdMecanico))
                .OrderByDescending(x => x.IdMecanico)
                .GetPagedAsync(page, take);
                if (!collection.HasItems)
                {
                    throw new EmptyCollectionException("No se encontró ningun Item en la Base de Datos");
                }
                return collection.MapTo<DataCollection<MecanicosDTO>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<MecanicosDTO> GetAsync(long id)
        {
            try
            {
                var unidad = await _context.Mecanicos.SingleAsync(x => x.IdMecanico == id);

                if (await _context.Mecanicos.SingleAsync(x => x.IdMecanico == id) == null)
                {
                    throw new EmptyCollectionException("Error al obtener el Mecanico, el Mecanico con id" + " " + id + " " + "no existe");
                }
                return unidad.MapTo<MecanicosDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<UpdateMecanicoDTO> PutAsync(UpdateMecanicoDTO mecanicoDto, long id)
        {
            if (mecanicoDto.Legajo.Length > 20)
            {
                throw new EmptyCollectionException("El Legajo no puede tener mas de 20 caracteres");
            }
            if(mecanicoDto.IdEmpresa == 0 && mecanicoDto.IdTaller == 0 && mecanicoDto.IdFuncion == 0 && mecanicoDto.IdTitulo == 0 && mecanicoDto.IdEspecialidad == 0 && mecanicoDto.IdAgrupacionSindical == 0 && mecanicoDto.IdConvenio == 0)
            {
                throw new EmptyCollectionException("Debe colocar al menos una opcion en el campo Empresa, Taller, Funcion, Titulo, Especialidad, Agrupacion Sindical o Convenio");
            }
            if(mecanicoDto.IdEmpresa == 0)
            {
                throw new EmptyCollectionException("La Empresa es Obligatoria");
            }
            if (mecanicoDto.IdTaller == 0)
            {
                throw new EmptyCollectionException("El Taller es Obligatorio");
            }
            if (mecanicoDto.IdFuncion == 0)
            {
                throw new EmptyCollectionException("La Función es Obligatoria");
            }
            if (mecanicoDto.IdTitulo == 0)
            {
                throw new EmptyCollectionException("El Título es Obligatorio");
            }
            if (mecanicoDto.IdEspecialidad == 0)
            {
                throw new EmptyCollectionException("La Especialidad es Obligatoria");
            }
            if (mecanicoDto.IdAgrupacionSindical == 0)
            {
                throw new EmptyCollectionException("La Agrupación es Obligatoria");
            }
            if (mecanicoDto.IdConvenio == 0)
            {
                throw new EmptyCollectionException("El Convenio es Obligatorio");
            }
            var resultTaller = await _context.Talleres.FindAsync(mecanicoDto.IdTaller);
            if (resultTaller == null)
            {
                throw new EmptyCollectionException("El Taller con id " + mecanicoDto.IdTaller + ", no existe");
            }
            var mecanico = await _context.Mecanicos.FindAsync(id);

            mecanico.ApellidoyNombres = mecanicoDto.ApellidoyNombres;
            mecanico.Legajo = mecanicoDto.Legajo;
            mecanico.Especialidad = mecanicoDto.Especialidad;
            mecanico.Obs = mecanicoDto.Obs;
            mecanico.Foto = mecanicoDto.Foto;
            mecanico.Activo = mecanicoDto.Activo;
            mecanico.NroDocumento = mecanicoDto.NroDocumento;
            mecanico.FechaNacimiento = mecanicoDto.FechaNacimiento;
            mecanico.Empresa = mecanicoDto.Empresa;
            mecanico.Funcion = mecanicoDto.Funcion;
            mecanico.AgrupacionSindical = mecanicoDto.AgrupacionSindical;
            mecanico.Convenio = mecanicoDto.Convenio;
            mecanico.CostoHora = mecanicoDto.CostoHora;
            mecanico.ValorHoraInterno = mecanicoDto.ValorHoraInterno;
            mecanico.IdTaller = mecanicoDto.IdTaller;
            mecanico.IdEmpresa = mecanicoDto.IdEmpresa;
            mecanico.IdFuncion = mecanicoDto.IdFuncion;
            mecanico.IdTitulo = mecanicoDto.IdTitulo;
            mecanico.IdEspecialidad = mecanicoDto.IdEspecialidad;
            mecanico.IdAgrupacionSindical = mecanicoDto.IdAgrupacionSindical;
            mecanico.IdConvenio = mecanicoDto.IdConvenio;


            await _context.SaveChangesAsync();

            return mecanicoDto.MapTo<UpdateMecanicoDTO>();
        }
        public async Task<MecanicosDTO> DeleteAsync(long id)
        {
            var mecaninco = await _context.Mecanicos.FindAsync(id);
            if (mecaninco == null)
            {
                throw new EmptyCollectionException("Error al eliminar el Mecanico, el Mecanico con id" + " " + id + " " + "no existe");
            }

            _context.Mecanicos.Remove(mecaninco);

            await _context.SaveChangesAsync();
            return mecaninco.MapTo<MecanicosDTO>();
        }
    }
}
