using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MC426_Backend.Domain.Entities
{
    public class Funcionario
    {
        [Required, Key]
        [Column("FuncionarioId")]
        public int Id { get; set; }

        [Required]
        public Guid Chave { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(70)")]
        [MaxLength(70)]
        public string? Nome { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(70)")]
        [MaxLength(70)]
        public string? Email { get; set; }

        public DateTime? DataNascimento { get; set; }

        [Column(TypeName = "VARCHAR(11)")]
        [MaxLength(11)]
        public string? Cpf { get; set; }

        [Column(TypeName = "VARCHAR(30)")]
        [MaxLength(30)]
        public string? Telefone { get; set; }

        [Required]
        public bool Ativo { get; set; }

        [Required]
        public bool Excluido { get; set; }
    }
}
