using MediatR;

namespace Service.EventHandlers.Command
{
    public class CreateTareasCommand : INotification
    { 

        public string Descripcion { get; set; }
        public string Obs { get; set; }


    }
}