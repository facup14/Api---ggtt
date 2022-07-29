using MediatR;

namespace Service.EventHandlers.Command.CreateCommands
{
    public class CreateDomiciliosCommand : INotification
    {
        public bool? Predeterminado { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdCalle { get; set; }
        public string Numero { get; set; }
        public int? IdBarrio { get; set; }
        public string Dpto { get; set; }
        public int? IdCliente { get; set; }
    }
}
