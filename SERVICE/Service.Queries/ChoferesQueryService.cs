﻿using Common.Collection;
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
using System.Text;
using System.Threading.Tasks;

namespace Service.Queries
{
    public interface IChoferesQueryService
    {
        Task<DataCollection<ChoferesDTO>> GetAllAsync(int page, int take, IEnumerable<long> choferes = null, bool order = false);
        Task<ChoferesDTO> GetAsync(long id);
        Task<UpdateChoferesDTO> PutAsync(UpdateChoferesDTO choferDto, long id);
        Task<UnidadesDTO> DeleteAsync(long id);
        Task<UpdateChoferesDTO> CreateAsync(UpdateChoferesDTO chofer);
    }
    public class ChoferesQueryService : IChoferesQueryService
    {
        private readonly Context _context;

        public ChoferesQueryService(Context context)
        {
            _context = context;
        }

        public async Task<DataCollection<ChoferesDTO>> GetAllAsync(int page, int take, IEnumerable<long> choferes = null, bool order = false)
        {
            try
            {
                if (!order)
                {
                    var orderBy = await _context.Choferes
                    .Where(x => choferes == null || choferes.Contains(x.IdChofer))
                    .OrderBy(x => x.IdChofer)
                    .GetPagedAsync(page, take);
                    return orderBy.MapTo<DataCollection<ChoferesDTO>>();
                }
                var collection = await _context.Choferes
                .Where(x => choferes == null || choferes.Contains(x.IdChofer))
                .OrderByDescending(x => x.IdChofer)
                .GetPagedAsync(page, take);
                if (!collection.HasItems)
                {
                    throw new EmptyCollectionException("No se encontró ningun Item en la Base de Datos");
                }
                return collection.MapTo<DataCollection<ChoferesDTO>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<ChoferesDTO> GetAsync(long id)
        {
            try
            {
                var chofer = await _context.Choferes.FindAsync(id);

                if (await _context.Choferes.FindAsync(id) == null)
                {
                    throw new EmptyCollectionException("Error al obtener el Chofer, el Chofer con id" + " " + id + " " + "no existe");
                }
                return chofer.MapTo<ChoferesDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<UpdateChoferesDTO> PutAsync(UpdateChoferesDTO choferDto, long id)
        {
            if (choferDto.ApellidoyNombres == "" )
            {
                throw new EmptyCollectionException("El Nombre y Apellido del Chofer son Obligatorios");
            }
            if (await _context.Choferes.FindAsync(id) is null)
            {
                throw new EmptyCollectionException("Error al actualizar el Chofer, el Chofer con id" + " " + id + " " + "no existe");
            }

            var chofer = await _context.Choferes.FindAsync(id);

            chofer.ApellidoyNombres = choferDto.ApellidoyNombres;
            chofer.Legajo = choferDto.Legajo;
            chofer.CarnetVence = choferDto.CarnetVence;
            chofer.Obs = choferDto.Obs;
            chofer.Foto = choferDto.Foto;
            chofer.Activo = choferDto.Activo;
            chofer.NroDocumento = choferDto.NroDocumento;
            chofer.FechaNacimiento = choferDto.FechaNacimiento;
            chofer.IdEmpresa = choferDto.IdEmpresa;
            chofer.IdAgrupacionSindical = choferDto.IdAgrupacionSindical;
            chofer.IdConvenio = choferDto.IdConvenio;
            chofer.IdFuncion = choferDto.IdFuncion;
            chofer.IdEspecialidad = choferDto.IdEspecialidad;
            chofer.IdTitulo = choferDto.IdTitulo;

            

            await _context.SaveChangesAsync();

            return choferDto.MapTo<UpdateChoferesDTO>();
        }
        public async Task<UnidadesDTO> DeleteAsync(long id)
        {
            var choferes = await _context.Choferes.FindAsync(id);
            if (choferes == null)
            {
                throw new EmptyCollectionException("Error al eliminar el Chofer, el Chofer con id" + " " + id + " " + "no existe");
            }

            _context.Choferes.Remove(choferes);

            await _context.SaveChangesAsync();
            return choferes.MapTo<UnidadesDTO>();
        }
        public async Task<UpdateChoferesDTO> CreateAsync(UpdateChoferesDTO chofer)
        {
            try
            {
                var newChofer = new Choferes()
                {
                    ApellidoyNombres = chofer.ApellidoyNombres,
                    Legajo = chofer.Legajo,
                    CarnetVence = chofer.CarnetVence,
                    Obs = chofer.Obs,
                    Foto = chofer.Foto,
                    Activo = chofer.Activo,
                    NroDocumento = chofer.NroDocumento,
                    FechaNacimiento = chofer.FechaNacimiento,
                    IdEmpresa = chofer.IdEmpresa,
                    IdAgrupacionSindical = chofer.IdAgrupacionSindical,
                    IdConvenio = chofer.IdConvenio,
                    IdFuncion = chofer.IdFuncion,
                    IdEspecialidad = chofer.IdEspecialidad,
                    IdTitulo = chofer.IdTitulo,
            };
                await _context.Choferes.AddAsync(newChofer);

                await _context.SaveChangesAsync();
                return newChofer.MapTo<UpdateChoferesDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la Agrupación");
            }

        }
    }
}
