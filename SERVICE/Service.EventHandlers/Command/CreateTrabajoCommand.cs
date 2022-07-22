using MediatR;
using System;

namespace Service.EventHandlers.Command
{
    public class CreateTrabajoCommand:INotification
    {
       
        public string Descripcion { get; set; }
        public string Obs { get; set; }
        public int TipoTrabajo { get; set; }
        public long IdRubro { get; set; }


    }
}
