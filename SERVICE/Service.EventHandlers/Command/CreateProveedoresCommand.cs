using MediatR;

namespace Service.EventHandlers.Command
{
    public class CreateProveedoresCommand : INotification
    {
        public string RazonSocial { get; set; }
        public string IdAlicuota { get; set; }
        public string NCuit { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Contacto { get; set; }
        public string Email { get; set; }
        public string ChequesA { get; set; }
        public string Web { get; set; }
        public string Obs { get; set; }

    }
}