using DATA.Models;
using MediatR;
using PERSISTENCE;
using Service.EventHandlers.Command;
using System.Threading;
using System.Threading.Tasks;

namespace Service.EventHandlers
{
    public class CreateTaller : INotificationHandler<CreateTallerCommand>
    {
        private readonly Context _context;
        public CreateTaller(Context context)
        {
            _context = context;
        }
        public async Task Handle(CreateTallerCommand notification, CancellationToken cancellationToken)
        {


            await _context.AddAsync(new Talleres
            {
                NombreTaller = notification.NombreTaller,
                Obs = notification.Obs,
                Mail = notification.Mail ,
                JefeAsignado = notification.JefeAsignado,
                Telefonos = notification.Telefonos,
                NombreRecibe = notification.NombreRecibe,
                RutaLogo = notification.RutaLogo,
                RutaInstalador = notification.RutaInstalador,
                IdLugarAbastecimiento = notification.IdLugarAbastecimiento,
                RutaIcono = notification.RutaIcono,
                FondoPantalla = notification.FondoPantalla,
                NombreEmpresa = notification.NombreEmpresa,
                NombreProvincia = notification.NombreProvincia,
                Slogan = notification.Slogan,
                IdRecibidoPor = notification.IdRecibidoPor,
                IdSolicitadoPor = notification.IdSolicitadoPor,
                RecibePersona = notification.RecibePersona,
                SolicitaPersona = notification.SolicitaPersona,
                CargaAutomaticaCombustible = notification.CargaAutomaticaCombustible,
                RutaCargaAutomatica = notification.RutaCargaAutomatica,
                UserIdCombustible = notification.UserIdCombustible,
                PasswordCombustible = notification.PasswordCombustible,
                IdLocalidad = notification.IdLocalidad,
        }); ;
            await _context.SaveChangesAsync();
        }
    }
}
