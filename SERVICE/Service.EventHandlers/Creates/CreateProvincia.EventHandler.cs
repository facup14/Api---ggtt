using System;
using DATA.Models;
using MediatR;
using PERSISTENCE;
using System.Threading;
using System.Threading.Tasks;
using Service.EventHandlers.Command.CreateCommands;

namespace Service.EventHandlers.Creates
{/// <summary>
/// //////////////////////////////////////////Se utilizan Handlers para hacer peticiones que modifiquen la base de datos.
/// </summary>
    public class CreateProvincia : INotificationHandler<CreateProvinciaCommand>
    {
        private readonly Context _context;
        public CreateProvincia(Context context)
        {
            _context = context;
        }
        public async Task Handle(CreateProvinciaCommand notification, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new Provincias
            {
                Provincia = notification.Provincia,

            });
            await _context.SaveChangesAsync();
        }
    }
}
