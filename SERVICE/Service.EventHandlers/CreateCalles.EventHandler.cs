using DATA.Models;
using MediatR;
using PERSISTENCE;
using Service.EventHandlers.Command;
using System.Threading;
using System.Threading.Tasks;

namespace Service.EventHandlers
{
    public class CreateCalles : INotificationHandler<CreateCallesCommand>
    {
        private readonly Context _context;
        public CreateCalles(Context context)
        {
            _context = context;
        }
        public async Task Handle(CreateCallesCommand notification, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new Calles
            {
                Calle = notification.Calle,
                Obs = notification.Obs

            });
            await _context.SaveChangesAsync();
        }
    }
}
