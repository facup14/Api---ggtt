using DATA.Models;
using MediatR;
using PERSISTENCE;
using Service.EventHandlers.Command.CreateCommands;
using System.Threading;
using System.Threading.Tasks;

namespace Service.EventHandlers.Creates
{
    public class CreateDomicilio : INotificationHandler<CreateDomiciliosCommand>
    {
        private readonly Context _context;
        public CreateDomicilio(Context context)
        {
            _context = context;
        }
        public async Task Handle(CreateDomiciliosCommand notification, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new Domicilios
            {
                Predeterminado = notification.Predeterminado,
                IdProveedor = notification.IdProveedor,
                IdCalle = notification.IdCalle,
                Numero = notification.Numero,
                IdBarrio = notification.IdBarrio,
                Dpto = notification.Dpto,
                IdCliente = notification.IdCliente,
            });
            await _context.SaveChangesAsync();
        }
    }
}
