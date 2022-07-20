using MediatR;

namespace Service.EventHandlers.Command
{
    public class CreateBarriosCommand : INotification
    {
        public string Barrio { get; set; }
        public string Obs { get; set; }
        public long IdLocalidad { get; set; }
    }
}
