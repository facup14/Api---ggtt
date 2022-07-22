using DATA.Extensions;
using DATA.Models;
using MediatR;
using PERSISTENCE;
using Service.EventHandlers.Command;
using System.Threading;
using System.Threading.Tasks;


namespace Service.EventHandlers
{
    public class CreateRubro : INotificationHandler<CreateRubroCommand>
    {
        private readonly Context _context;
        public CreateRubro(Context context)
        {
            _context = context;
        }
        public async Task Handle(CreateRubroCommand notification, CancellationToken cancellationToken)
        {

            if (notification.Descripcion == "")
            {
                throw new EmptyCollectionException("Debe ingresar descripcion del rubro");
            }

            await _context.AddAsync(new Rubros
            {
                Descripcion = notification.Descripcion,
                Obs = notification.Obs,
                IdMecanico= notification.IdMecanico

            });
            await _context.SaveChangesAsync();
        }

    }
}
