using DATA.Models;
using MediatR;
using PERSISTENCE;
using Service.EventHandlers.Command;
using System.Threading;
using System.Threading.Tasks;

namespace Service.EventHandlers
{/// <summary>
/// //////////////////////////////////////////Se utilizan Handlers para hacer peticiones que modifiquen la base de datos.
/// </summary>
    public class CreateValoresMediciones : INotificationHandler <CreateValoresMedicionesCommand>
    {
        private readonly Context _context;
        public CreateValoresMediciones(Context context)
        {
            _context = context;
        }
        public async Task Handle(CreateValoresMedicionesCommand notification, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new ValoresMediciones
            {
                ValorMedicion = notification.ValorMedicion,
                Obs = notification.Obs,

            });
            await _context.SaveChangesAsync();
        }
    }
}
