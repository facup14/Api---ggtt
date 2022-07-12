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
    public class CreateUnidad : INotificationHandler<CreateUnidadCommand>
    {
        private readonly Context _context;
        public CreateUnidad(Context context)
        {
            _context = context;
        }
        public async Task Handle(CreateUnidadCommand notification, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new Unidades
            {
                NroUnidad = notification.NroUnidad,
                Dominio = notification.Dominio,
                Motor = notification.Motor,
                Chasis = notification.Chasis,
                Titular = notification.Titular,
                idEstadoUnidad = notification.idEstadoUnidad,
                idModelo= notification.idModelo,
                idSituacionUnidad = notification.idSituacionUnidad
            });
            await _context.SaveChangesAsync();
        }
    }
}
