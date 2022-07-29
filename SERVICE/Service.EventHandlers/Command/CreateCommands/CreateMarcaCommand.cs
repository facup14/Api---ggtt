using MediatR;


namespace Service.EventHandlers.Command.CreateCommands
{
    public class CreateMarcaCommand : INotification
    {

        public string Marca { get; set; }
        public string Obs { get; set; }
    }
}
