using DATA.Models;
using MediatR;
using PERSISTENCE;
using Service.EventHandlers.Command;
using System.Threading;
using System.Threading.Tasks;

namespace Service.EventHandlers
{
    public class CreateProveedores : INotificationHandler<CreateProveedoresCommand>
    {
        private readonly Context _context;
        public CreateProveedores(Context context)
        {
            _context = context;
        }
        public async Task Handle(CreateProveedoresCommand notification, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new Proveedores
            {
                RazonSocial = notification.RazonSocial,
                IdAlicuota = notification.IdAlicuota,
                NCuit = notification.NCuit,
                Telefono = notification.Telefono,
                Celular = notification.Celular,
                Contacto = notification.Contacto,
                Email = notification.Email,
                ChequesA = notification.ChequesA,
                Web = notification.Web,
                Obs = notification.Obs

            });
            await _context.SaveChangesAsync();
        }
    }
}
