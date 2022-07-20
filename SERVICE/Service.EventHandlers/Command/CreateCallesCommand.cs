using MediatR;

namespace Service.EventHandlers.Command
{
    public class CreateCallesCommand : INotification
    {
        public string Calle { get; set; }
        public string Obs { get; set; }
    }
}
