using MediatR;

namespace Service.EventHandlers.Command.CreateCommands
{
    public class CreateConvenioCommand : INotification
    {

        public string Descripcion { get; set; }
        public string Obs { get; set; }

    }
}
