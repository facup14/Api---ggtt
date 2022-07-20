using MediatR;

namespace Service.EventHandlers.Command.CreateCommands
{
    public class CreateEmpresaCommand : INotification
    {
        public string Descripcion { get; set; }
        public string Obs { get; set; }
    }
}
