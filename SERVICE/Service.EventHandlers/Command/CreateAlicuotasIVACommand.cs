using MediatR;

namespace Service.EventHandlers.Command
{
    public class CreateAlicuotasIVACommand : INotification
    { 

        public string Detalle { get; set; }
        public decimal? Alicuota { get; set; }
        public bool NumeroCUIT { get; set; }
        public decimal AlicuotaRecargo { get; set; }

    }
}