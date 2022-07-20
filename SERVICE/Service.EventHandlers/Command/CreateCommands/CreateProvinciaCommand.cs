using MediatR;

namespace Service.EventHandlers.Command.CreateCommands
{
    public class CreateProvinciaCommand : INotification
    {

        public string Provincia { get; set; }

    }
}
