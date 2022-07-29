

using MediatR;

namespace Service.EventHandlers.Command.CreateCommands
{
    public class CreateUnidadesDeMedidaCommand : INotification
    {
        public string UnidadDeMedida { get; set; }
    }
}
