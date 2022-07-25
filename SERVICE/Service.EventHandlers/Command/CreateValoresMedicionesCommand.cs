using MediatR;

namespace Service.EventHandlers.Command
{
    public class CreateValoresMedicionesCommand : INotification
    { 

        public string ValorMedicion { get; set; }
        public string Obs { get; set; }

    }
}