using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MC426_Domain.Entities
{
    public class Paciente
    {
        [Required, Key]
        [Column("PacienteId")]
        public int Id { get; set; }

        [Required]
        public Guid Chave { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(70)")]
        [MaxLength(70)]
        public string? Nome { get; set; }

        public DateTime? DataNascimento { get; set; }

        [Column(TypeName = "VARCHAR(11)")]
        [MaxLength(11)]
        public string? Cpf { get; set; }

        [Required]
        public bool Ativo { get; set; }

        [Required]
        public bool Excluido { get; set; }
  
    }
}
