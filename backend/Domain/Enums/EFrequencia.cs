using System.ComponentModel.DataAnnotations;

namespace MC426_Backend.Domain.Enums
{
    public enum EFrequencia
    {
        [Display(Name = "Não se repete")]
        NaoSeRepete = 1,
        [Display(Name = "Todos os dias")]
        TodosOsDias = 2,
        [Display(Name = "Semanal")]
        Semanal = 3,
        [Display(Name = "Mensal")]
        Mensal = 4,
        [Display(Name = "Anual")]
        Anual = 5,
        [Display(Name = "Todos os dias da semana")]
        TodosOsDiasSemana = 6,
    }
}
