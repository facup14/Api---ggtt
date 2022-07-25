using DATA.Models;
using MediatR;
using PERSISTENCE;
using Service.EventHandlers.Command;
using System.Threading;
using System.Threading.Tasks;

namespace Service.EventHandlers
{
    public class CreateAlicuotasIVA : INotificationHandler<CreateAlicuotasIVACommand>
    {
        private readonly Context _context;
        public CreateAlicuotasIVA(Context context)
        {
            _context = context;
        }
        public async Task Handle(CreateAlicuotasIVACommand notification, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new AlicuotasIVA
            {
                Detalle = notification.Detalle,
                Alicuota = notification.Alicuota,
                NumeroCUIT = notification.NumeroCUIT,
                AlicuotaRecargo = notification.AlicuotaRecargo
            });
            await _context.SaveChangesAsync();
        }
    }
}
