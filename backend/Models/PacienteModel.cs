using System.ComponentModel.DataAnnotations;

namespace MC426_Backend.Models
{
    public class PacienteModel
    {
        public int Id { get; set; }

        public Guid Chave { get; set; }

        [Required(ErrorMessage = "Nome obrigatório")]
        [MaxLength(70)]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Data de nascimento obrigatória")]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "CPF")]
        public string? Cpf { get; set; }

        //CADASTRO
        [Required(ErrorMessage = "E-mail obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [Display(Name = "E-mail")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Senha obrigatória")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "Confirmação de senha obrigatória")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmação de senha")]
        [Compare("Password", ErrorMessage = "As senhas não coincidem")]
        public string? ConfirmPassword { get; set; } = "";

    }
}
