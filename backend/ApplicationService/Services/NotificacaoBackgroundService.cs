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

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var agendamentoService = scope.ServiceProvider.GetService<IAgendamentoService>();
                        var emailService = scope.ServiceProvider.GetService<EmailService>();

                        DateTime inicioDia = DateTime.Today; // Hoje 00:00:00
                        DateTime fimDia = DateTime.Today.AddDays(1).AddTicks(-1); // Hoje 23:59:59

                        var agendamentosDia = agendamentoService.GetAll().Where(a => a.DataInicio >= inicioDia && a.DataInicio <= fimDia);
                        var agendamentosPacientes = agendamentosDia.GroupBy(a => a.PacienteId);
                        var agendamentosMedicos = agendamentosDia.GroupBy(a => a.MedicoId);

                        foreach (var group in agendamentosPacientes)
                        {
                            System.Console.WriteLine($"Agendamentos do paciente: {group.Key}");
                            foreach (var agendamentoPaciente in group)
                            {
                                System.Console.WriteLine($"Agendamento hoje ({agendamentoPaciente.DataInicio}) de {agendamentoPaciente.HoraInicio} ate {agendamentoPaciente.HoraFinal}, especialidade {agendamentoPaciente.Especialidade}, id: {agendamentoPaciente.Id}");
                            }
                        }

                        foreach (var group in agendamentosMedicos)
                        {
                            System.Console.WriteLine($"Agendamentos do médico: {group.Key}");
                            foreach (var agendamentoMedico in group)
                            {
                                System.Console.WriteLine($"Agendamento hoje ({agendamentoMedico.DataInicio}) de {agendamentoMedico.HoraInicio} ate {agendamentoMedico.HoraFinal}, especialidade {agendamentoMedico.Especialidade}, id: {agendamentoMedico.Id}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("Could not send notifications.");
                    System.Console.Error.WriteLine(ex.Message);
                }

                await Task.Delay(60 * 1000, stoppingToken);
            }
        }
    }
}