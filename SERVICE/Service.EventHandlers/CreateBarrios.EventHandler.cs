using DATA.Models;
using MediatR;
using PERSISTENCE;
using Service.EventHandlers.Command;
using System.Threading;
using System.Threading.Tasks;

namespace Service.EventHandlers
{
    public class CreateBarrios : INotificationHandler<CreateBarriosCommand>
    {
        private readonly Context _context;
        public CreateBarrios(Context context)
        {
            _context = context;
        }
        public async Task Handle(CreateBarriosCommand notification, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new Barrios
            {
                Barrio = notification.Barrio,
                Obs = notification.Obs,
                IdLocalidad = notification.IdLocalidad,

            });
            await _context.SaveChangesAsync();
        }
    }
}
