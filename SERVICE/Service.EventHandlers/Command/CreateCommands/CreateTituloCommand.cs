using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Service.EventHandlers.Command.CreateCommands
{
    public class CreateTituloCommand : INotification
    {
        [Required]
        public string Descripcion { get; set; }
        public string Obs { get; set; }
    }
}
