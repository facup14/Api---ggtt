using MediatR;

namespace Service.EventHandlers.Command.CreateCommands
{
    public class CreateAgrupacionSindicalCommand : INotification
    {

        public string Descripcion { get; set; }
        public string Obs { get; set; }

    }
}