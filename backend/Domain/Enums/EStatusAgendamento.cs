using System.ComponentModel.DataAnnotations;

namespace MC426_Backend.Domain.Enums
{
    public enum EStatusAgendamento
    {
        [Display(Name = "Realizado")]
        Realizado = 1,
        [Display(Name = "Confirmado")]
        Confirmado = 2,
        [Display(Name = "Cancelado")]
        Cancelado = 3,
    }
}
