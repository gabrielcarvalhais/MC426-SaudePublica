using MC426_Backend.Domain.Enums;
using MC426_Backend.Utils;
using System.ComponentModel.DataAnnotations;

namespace MC426_Backend.Models
{
    public class AgendamentoModel
    {
        public int Id { get; set; }

        public Guid Chave { get; set; }

        public EStatusAgendamento StatusAgendamento { get; set; }

        public PacienteModel? Paciente { get; set; }
        [Required(ErrorMessage = "Paciente obrigatório")]
        public int PacienteId { get; set; }

        public FuncionarioModel? Medico { get; set; }
        public int? MedicoId { get; set; }

        [Required(ErrorMessage = "Especialidade obrigatória")]
        public EEspecialidade Especialidade { get; set; }
        public string NomeEspecialidade => RetornaNameEspecialidade(Especialidade);

        [Required(ErrorMessage = "Data obrigatória")]
        public DateTime? DataInicio { get; set; }

        [Required(ErrorMessage = "Horário inicial obrigatório")]
        public TimeSpan HoraInicio { get; set; }

        [Required(ErrorMessage = "Horário final obrigatório")]
        public TimeSpan HoraFinal { get; set; }

        [Required(ErrorMessage = "Frequência obrigatória")]
        public EFrequencia Frequencia { get; set; }

        public DateTime? DataFinal { get; set; }

        public int? VinculoId { get; set; }

        public bool Excluido { get; set; }

        public string Color => DefineCor(StatusAgendamento);

        public DateTime DataHoraInicio => RetornaDataHora(DataInicio, HoraInicio);
        public DateTime DataHoraFim => RetornaDataHora(DataInicio, HoraFinal);
        public string DataInicioFormatada => RetornaDataFormatada(DataInicio);
        public string DataFinalFormatada => RetornaDataFormatada(DataFinal);

        private DateTime RetornaDataHora(DateTime? dateTime, TimeSpan? timeSpan)
        {
            if (dateTime != null && timeSpan != null)
            {
                var data = Convert.ToDateTime(dateTime);
                var hora = (TimeSpan)timeSpan;
                return new DateTime(data.Year, data.Month, data.Day, hora.Hours, hora.Minutes, hora.Seconds);
            }
            return new DateTime();
        }

        private string RetornaDataFormatada(DateTime? data)
        {
            if (!data.HasValue)
                return "";

            return Convert.ToDateTime(data).ToString("yyyy-MM-dd");
        }

        private string RetornaNameEspecialidade(EEspecialidade especialidade)
        {
            if (especialidade != 0)
                return especialidade.GetDisplayName();
            else
                return "";
        }

        private string DefineCor(EStatusAgendamento? statusAgendamento)
        {
            switch (statusAgendamento)
            {
                case EStatusAgendamento.EmAberto:
                    return "#0dcaf0";
                case EStatusAgendamento.Confirmado:
                    return "#2bf032";
                case EStatusAgendamento.Aguardando:
                    return "#f0df2b";
                case EStatusAgendamento.Cancelado:
                    return "#f02b32";
                case EStatusAgendamento.Realizado:
                    return "#00ff99";
                default:
                    return "#0dcaf0";
            }
        }
    }
}
