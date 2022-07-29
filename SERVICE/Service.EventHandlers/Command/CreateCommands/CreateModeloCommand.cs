using MediatR;

namespace Service.EventHandlers.Command.CreateCommands
{
    public class CreateModeloCommand : INotification
    {
        public string Modelo { get; set; }
        public string Obs { get; set; }
        public long IdMarca { get; set; }
        public long IdGrupo { get; set; }

    }
}
