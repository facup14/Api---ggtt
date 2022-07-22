using MediatR;
using System;

namespace Service.EventHandlers.Command
{
    public class CreateRubroCommand : INotification
    {
        public string Descripcion { get; set; }
        public string Obs { get; set; }
        public long IdMecanico{ get; set; }
    }
}
