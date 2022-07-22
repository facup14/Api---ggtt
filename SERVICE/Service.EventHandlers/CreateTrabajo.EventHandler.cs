using DATA.Extensions;
using DATA.Models;
using MediatR;
using PERSISTENCE;
using Service.EventHandlers.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Service.EventHandlers
{
    public class CreateTrabajo : INotificationHandler<CreateTrabajoCommand>
    {
        private readonly Context _context;
        public CreateTrabajo(Context context)
        {
            _context = context;
        }
        public async Task Handle(CreateTrabajoCommand notification, CancellationToken cancellationToken)
        {
       
            if (notification.Descripcion == "")
            {
                throw new EmptyCollectionException("Debe ingresar descripcion del trabajo");
            }
        
            await _context.AddAsync(new Trabajos
            {
                Descripcion = notification.Descripcion,
                Obs = notification.Obs,
                TipoTrabajo = notification.TipoTrabajo,
                IdRubro    = notification.IdRubro
              
            });
            await _context.SaveChangesAsync();
        }

    }
}
