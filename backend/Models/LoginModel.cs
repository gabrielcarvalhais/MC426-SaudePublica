using System.ComponentModel.DataAnnotations;

namespace MC426_Backend.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "E-mail obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Senha obrigatória")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
