using DATA.Extensions;
using DATA.Models;
using MediatR;
using PERSISTENCE;
using Service.EventHandlers.Command.CreateCommands;
using System.Threading;
using System.Threading.Tasks;

namespace Service.EventHandlers.Creates
{
    public class CreateMecanico : INotificationHandler<CreateMecanicoCommand>
    {
        private readonly Context _context;
        public CreateMecanico(Context context)
        {
            _context = context;
        }
        public async Task Handle(CreateMecanicoCommand notification, CancellationToken cancellationToken)
        {
            if (notification.Legajo.Length > 20)
            {
                throw new EmptyCollectionException("El Legajo no puede tener mas de 20 caracteres");
            }
            if (notification.IdEmpresa == 0 && notification.IdTaller == 0 && notification.IdFuncion == 0 && notification.IdTitulo == 0 && notification.IdEspecialidad == 0 && notification.IdAgrupacionSindical == 0 && notification.IdConvenio == 0)
            {
                throw new EmptyCollectionException("Debe colocar al menos una opcion en el campo Empresa, Taller, Funcion, Titulo, Especialidad, Agrupacion Sindical o Convenio");
            }
            if (notification.IdEmpresa == 0)
            {
                throw new EmptyCollectionException("La Empresa es Obligatoria");
            }
            if (notification.IdTaller == 0)
            {
                throw new EmptyCollectionException("El Taller es Obligatorio");
            }
            if (notification.IdFuncion == 0)
            {
                throw new EmptyCollectionException("La Función es Obligatoria");
            }
            if (notification.IdTitulo == 0)
            {
                throw new EmptyCollectionException("El Título es Obligatorio");
            }
            if (notification.IdEspecialidad == 0)
            {
                throw new EmptyCollectionException("La Especialidad es Obligatoria");
            }
            if (notification.IdAgrupacionSindical == 0)
            {
                throw new EmptyCollectionException("La Agrupación es Obligatoria");
            }
            if (notification.IdConvenio == 0)
            {
                throw new EmptyCollectionException("El Convenio es Obligatorio");
            }
            var resultTaller = await _context.Talleres.FindAsync(notification.IdTaller);
            if (resultTaller == null)
            {
                throw new EmptyCollectionException("El Taller con id " + notification.IdTaller + ", no existe");
            }
            await _context.AddAsync(new Mecanicos
            {
                ApellidoyNombres = notification.ApellidoyNombres,
                Legajo = notification.Legajo,
                Especialidad = notification.Especialidad,
                Obs = notification.Obs,
                Foto = notification.Foto,
                Activo = notification.Activo,
                NroDocumento = notification.NroDocumento,
                FechaNacimiento = notification.FechaNacimiento,
                Empresa = notification.Empresa,
                Funcion = notification.Funcion,
                AgrupacionSindical = notification.AgrupacionSindical,
                Convenio = notification.Convenio,
                CostoHora = notification.CostoHora,
                ValorHoraInterno = notification.ValorHoraInterno,
                IdTaller = notification.IdTaller,
                IdEmpresa = notification.IdEmpresa,
                IdFuncion = notification.IdFuncion,
                IdTitulo = notification.IdTitulo,
                IdEspecialidad = notification.IdEspecialidad,
                IdAgrupacionSindical = notification.IdAgrupacionSindical,
                IdConvenio = notification.IdConvenio,
            });

            await _context.SaveChangesAsync();
        }
    }
}
