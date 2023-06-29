using MC426_Backend.Domain.Entities;
using MC426_Backend.Domain.Interfaces.Services;

namespace MC426_Backend.ApplicationService.Services
{
    public class NotificacaoBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public NotificacaoBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private void EnviarNotificacoesPacientes(IServiceScope scope, List<Agendamento> agendamentosDia)
        {
            var pacienteService = scope.ServiceProvider.GetService<IPacienteService>();
            var funcionarioService = scope.ServiceProvider.GetService<IFuncionarioService>();
            var emailService = scope.ServiceProvider.GetService<EmailService>();

            var agendamentosPacientes = agendamentosDia.GroupBy(a => a.PacienteId);

            foreach (var group in agendamentosPacientes)
            {
                var paciente = pacienteService!.GetById(group.Key);
                string email = EmailService.EMAIL_HEADER + $@"
                    <body>
                        <div class=""container"">
                            <div class=""header"">
                                <h1 class=""title"">Notificação de Consultas</h1>
                                <h2 class=""subtitle"">Olá {paciente.Nome}! Esses são seus agendamentos de consultas médicas para hoje</h2>
                            </div>";

                foreach (var agendamentoPaciente in group)
                {
                    var medico = funcionarioService!.GetById((int)agendamentoPaciente.MedicoId!);
                    var data = agendamentoPaciente.DataInicio?.ToString("dd/MM/yyyy") ?? "N/A";
                    var hora = agendamentoPaciente.HoraInicio?.ToString(@"hh\:mm") ?? "N/A";

                    email += $@"
                        <div class=""appointment"">
                            <h3 class=""appointment-title"">Consulta com o Dr. {medico.Nome}</h3>
                            <p class=""appointment-details"">
                                Especialidade: {agendamentoPaciente.Especialidade.ToString()}<br>
                                Data: {data}<br>
                                Hora: {hora}<br>
                            </p>
                        </div>
                    ";
                }

                email += EmailService.EMAIL_FOOTER + "</body>";

                emailService.SendEmail(paciente.Email, "Saúde+Barão: Agendamentos do dia", email);
            }
        }

        private void EnviarNotificacoesFuncionarios(IServiceScope scope, List<Agendamento> agendamentosDia)
        {
            var funcionarioService = scope.ServiceProvider.GetService<IFuncionarioService>();
            var pacienteService = scope.ServiceProvider.GetService<IPacienteService>();
            var emailService = scope.ServiceProvider.GetService<EmailService>();

            var agendamentosMedicos = agendamentosDia.GroupBy(a => a.MedicoId);

            foreach (var group in agendamentosMedicos)
            {
                var funcionario = funcionarioService!.GetById((int)group.Key!);
                string email = EmailService.EMAIL_HEADER + $@"
                    <body>
                        <div class=""container"">
                            <div class=""header"">
                                <h1 class=""title"">Notificação de Consultas</h1>
                                <h2 class=""subtitle"">Olá Dr. {funcionario.Nome}! Esses são seus agendamentos de consultas médicas para hoje</h2>
                            </div>";

                foreach (var agendamentoMedico in group)
                {
                    var paciente = pacienteService!.GetById(agendamentoMedico.PacienteId);
                    var data = agendamentoMedico.DataInicio?.ToString("dd/MM/yyyy") ?? "N/A";
                    var hora = agendamentoMedico.HoraInicio?.ToString(@"hh\:mm") ?? "N/A";

                    email += $@"
                        <div class=""appointment"">
                            <h3 class=""appointment-title"">Consulta com o paciente {paciente.Nome}</h3>
                            <p class=""appointment-details"">
                                Data: {data}<br>
                                Hora: {hora}<br>
                            </p>
                        </div>
                    ";
                }

                email += EmailService.EMAIL_FOOTER + "</body>";

                emailService!.SendEmail(funcionario.Email, "Saúde+Barão: Agendamentos do dia", email);
            }
        }

        private List<Agendamento> ObterAgendamentosDoDia(IServiceScope scope)
        {
            var agendamentoService = scope.ServiceProvider.GetService<IAgendamentoService>();

            DateTime inicioDia = DateTime.Today; // Hoje 00:00:00
            DateTime fimDia = DateTime.Today.AddDays(1).AddTicks(-1); // Hoje 23:59:59

            var agendamentosDia = agendamentoService!.GetAll().Where(a => a.DataInicio >= inicioDia && a.DataInicio <= fimDia
                && a.MedicoId != null && a.StatusAgendamento == Domain.Enums.EStatusAgendamento.Confirmado);

            return agendamentosDia.ToList();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var agendamentosDia = ObterAgendamentosDoDia(scope);
                        EnviarNotificacoesPacientes(scope, agendamentosDia);
                        EnviarNotificacoesFuncionarios(scope, agendamentosDia);
                    }
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("Could not send notifications.");
                    System.Console.Error.WriteLine(ex.Message);
                }

                await Task.Delay(24 * 60 * 60 * 1000, stoppingToken); // 1 dia
            }
        }
    }
}