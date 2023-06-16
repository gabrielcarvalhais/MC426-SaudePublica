using System.ComponentModel.DataAnnotations;

namespace MC426_Backend.Domain.Enums
{
    public enum EEspecialidade
    {
        [Display(Name = "Clínico geral")]
        ClinicoGeral = 1,
        [Display(Name = "Cardiologia")]
        Cardiologia = 2,
        [Display(Name = "Dermatologia")]
        Dermatologia = 3,
        [Display(Name = "Ginecologia")]
        Ginecologia = 4,
        [Display(Name = "Ortopedia")]
        Ortopedia = 5,
        [Display(Name = "Pediatria")]
        Pediatria = 6,
        [Display(Name = "Oftalmologia")]
        Oftalmologia = 7,
        [Display(Name = "Psiquiatria")]
        Psiquiatria = 8,
        [Display(Name = "Endocrinologia")]
        Endocrinologia = 9,
        [Display(Name = "Neurologia")]
        Neurologia = 10,
        [Display(Name = "Radiologia")]
        Radiologia = 11,
        [Display(Name = "Fisioterapia")]
        Fisioterapia = 12,
    }
}
