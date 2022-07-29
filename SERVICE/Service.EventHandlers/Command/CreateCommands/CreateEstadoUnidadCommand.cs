
using MediatR;

namespace Service.EventHandlers.Command.CreateCommands
{
    public class CreateEstadoUnidadCommand : INotification
    {
        public string Estado { get; set; }
        public string Obs { get; set; }
    }
}
