using DATA.Models;
using MediatR;
using PERSISTENCE;
using Service.EventHandlers.Command;
using System.Threading;
using System.Threading.Tasks;

namespace Service.EventHandlers
{
    public class CreateTareas : INotificationHandler<CreateTareasCommand>
    {
        private readonly Context _context;
        public CreateTareas(Context context)
        {
            _context = context;
        }
        public async Task Handle(CreateTareasCommand notification, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new Tareas
            {
                Descripcion = notification.Descripcion,
                Obs = notification.Obs,

            });
            await _context.SaveChangesAsync();
        }
    }
}
