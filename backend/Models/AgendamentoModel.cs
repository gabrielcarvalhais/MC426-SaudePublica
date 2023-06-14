using MC426_Backend.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace MC426_Backend.Models
{
    public class AgendamentoModel
    {
        public int Id { get; set; }

        public Guid Chave { get; set; }

        [Required(ErrorMessage = "Status obrigatório")]
        public EStatusAgendamento StatusAgendamento { get; set; }

        public PacienteModel? Paciente { get; set; }
        [Required(ErrorMessage = "Paciente obrigatório")]
        public int PacienteId { get; set; }

        public FuncionarioModel? Medico { get; set; }
        [Required(ErrorMessage = "Médico obrigatório")]
        public int MedicoId { get; set; }

        [Required(ErrorMessage = "Data obrigatória")]
        public DateTime? DataInicio { get; set; }

        [Required(ErrorMessage = "Horário inicial obrigatório")]
        public TimeSpan? HoraInicio { get; set; }

        [Required(ErrorMessage = "Horário final obrigatório")]
        public TimeSpan? HoraFinal { get; set; }

        [Required(ErrorMessage = "Frequência obrigatória")]
        public EFrequencia Frequencia { get; set; }

        public DateTime? DataFinal { get; set; }

        public int? VinculoId { get; set; }

        public bool Excluido { get; set; }
    }
}
