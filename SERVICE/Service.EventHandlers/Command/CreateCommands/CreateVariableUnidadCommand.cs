using MediatR;


namespace Service.EventHandlers.Command.CreateCommands
{
    public class CreateVariableUnidadCommand : INotification
    {
        public string Nombre { get; set; }
    }
}
