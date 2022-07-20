using MediatR;

namespace Service.EventHandlers.Command.CreateCommands
{
    public class CreateEspecialidadCommand : INotification
    {
        public string Descripcion { get; set; }
        public string Obs { get; set; }

    }
}
