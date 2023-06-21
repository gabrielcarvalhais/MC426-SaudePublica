using MC426_Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MC426_Backend.Domain.Enums;

namespace MC426_Backend.Domain.Entities
{
    public class Agendamento
    {
        [Required, Key]
        [Column("AgendamentoId")]
        public int Id { get; set; }

        [Required]
        public Guid Chave { get; set; }

        [Required]
        public EStatusAgendamento StatusAgendamento { get; set; }

        public Paciente Paciente { get; set; }
        public int PacienteId { get; set; }

        public Funcionario Medico { get; set; }
        public int? MedicoId { get; set; }

        [Required]
        public EEspecialidade Especialidade { get; set; }

        [Required]
        public DateTime? DataInicio { get; set; }

        [Required]
        public TimeSpan? HoraInicio { get; set; }

        [Required]
        public TimeSpan? HoraFinal { get; set; }

        [Required]
        public EFrequencia Frequencia { get; set; }

        public DateTime? DataFinal { get; set; }

        public decimal? ValorHora { get; set; }

        public bool Excluido { get; set; }

        public Agendamento Vinculo { get; set; }
        public int? VinculoId { get; set; }

        public List<Agendamento> Vinculos { get; set; }
    }
}
