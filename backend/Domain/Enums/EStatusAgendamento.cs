using System.ComponentModel.DataAnnotations;

namespace MC426_Backend.Domain.Enums
{
    public enum EStatusAgendamento
    {        
        [Display(Name = "Em aberto")]
        EmAberto = 1,
        [Display(Name = "Aguardando")]
        Aguardando = 2,
        [Display(Name = "Confirmado")]
        Confirmado = 3,
        [Display(Name = "Realizado")]
        Realizado = 4,        
        [Display(Name = "Cancelado")]
        Cancelado = 5,
    }
}
