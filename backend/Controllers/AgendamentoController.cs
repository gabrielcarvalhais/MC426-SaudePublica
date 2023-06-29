using AutoMapper;
using MC426_Backend.Domain.Entities;
using MC426_Backend.Domain.Interfaces.Services;
using MC426_Backend.ApplicationService.Services;
using MC426_Backend.Domain.Enums;
using MC426_Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace MC426_Backend.Controllers
{
    [ApiController]
    public class AgendamentoController : Controller
    {
        private readonly IAgendamentoService _agendamentoService;
        private readonly IPacienteService _pacienteService;
        private readonly IFuncionarioService _funcionarioService;
        private readonly EmailService _emailService;
        private readonly IMapper _mapper;

        public AgendamentoController(IAgendamentoService agendamentoService, IPacienteService pacienteService, IFuncionarioService funcionarioService, IMapper mapper, EmailService emailService)
        {
            _agendamentoService = agendamentoService;
            _pacienteService = pacienteService;
            _funcionarioService = funcionarioService;
            _mapper = mapper;
            _emailService = emailService;
        }        

        [Route("[controller]/GetAgendamentos")]
        [HttpPost]
        public JsonResult GetAgendamentos(FiltroAgendamentoModel filtro)
        {
            try
            {
                var agendamentos = _agendamentoService.GetAll().ToList();                

                if (filtro.UserId != null && filtro.UserId != string.Empty)
                {
                    if (string.IsNullOrEmpty(filtro.UserRole) || filtro.UserRole == "Paciente")
                    {
                        var paciente = _pacienteService.GetByChave(Guid.Parse(filtro.UserId));
                        agendamentos = agendamentos.Where(x => x.PacienteId == paciente.Id).ToList();
                    }
                    else if (!filtro.Todos)
                    {
                        var funcionario = _funcionarioService.GetByChave(Guid.Parse(filtro.UserId));
                        agendamentos = agendamentos.Where(x => x.MedicoId == funcionario.Id).ToList();
                    }
                }
                if (filtro.Especialidades != null && filtro.Especialidades.Length > 0)
                {
                    agendamentos = agendamentos.Where(a => filtro.Especialidades.Contains((int)a.Especialidade)).ToList();
                }

                var agendamentosModel = _mapper.Map<List<Agendamento>, List<AgendamentoModel>>(agendamentos);

                return new JsonResult(Ok(agendamentosModel));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest(ex.Message));
            }
        }

        [Route("[controller]/GetById/{id}")]
        [HttpGet]
        public JsonResult GetById(int id)
        {
            try
            {
                var agendamento = _agendamentoService.GetById(id);
                var agendamentoModel = _mapper.Map<Agendamento, AgendamentoModel>(agendamento);

                return new JsonResult(Ok(agendamentoModel));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest(ex.Message));
            }
        }

        [Route("[controller]/Salvar")]
        [HttpPost]
        public JsonResult SalvarAgendamento(AgendamentoModel model)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(BadRequest(ModelState.Values.SelectMany(e => e.Errors)));
            }

            if (model.HoraFinal <= model.HoraInicio)
            {
                return new JsonResult(BadRequest("O horário do término não pode ser inferior ao horário de início do agendamento."));
            }

            if (model.DataFinal != null && model.DataFinal <= model.DataInicio)
            {
                return new JsonResult(BadRequest("A data do término não pode ser inferior à data de início do agendamento."));
            }

            try
            {
                //update
                if (model.Id > 0)
                {                   
                    var agendamento = _agendamentoService.GetById(model.Id);
                    agendamento = _mapper.Map(model, agendamento);

                    _agendamentoService.Update(agendamento);

                    if (agendamento.MedicoId != null && agendamento.StatusAgendamento == EStatusAgendamento.Confirmado)
                    {
                        var paciente = _pacienteService.GetById(agendamento.PacienteId);
                        var medico = _funcionarioService.GetById((int)agendamento.MedicoId);

                        var pacienteEmail = paciente.Email;
                        var medicoEmail = medico.Email;

                        string inicioEmail = GetInicioEmail();
                        string fimEmail = GetFinalEmail(agendamento);

                        string emailPaciente = inicioEmail + $@"<h3 class=""appointment-title"">Consulta com o Dr. {medico.Nome}</h3>" + fimEmail;
                        string emailMedico = inicioEmail + $@"<h3 class=""appointment-title"">Consulta com o paciente {paciente.Nome}</h3>" + fimEmail;

                        _emailService.SendEmail(pacienteEmail, "Saúde+Barão: Consulta médica atualizada", emailPaciente);
                        _emailService.SendEmail(medicoEmail, "Saúde+Barão: Consulta médica atualizada", emailMedico);
                    }
                    

                    return new JsonResult(Ok());
                }
                //insert
                else
                {
                    var agendamento = _mapper.Map<AgendamentoModel, Agendamento>(model);
                    agendamento.Chave = Guid.NewGuid();

                    var data = agendamento.DataInicio;
                    var dataFinal = agendamento.DataFinal;

                    if (model.Frequencia != EFrequencia.NaoSeRepete)
                    {
                        agendamento.Vinculos = new List<Agendamento>();

                        while (data <= dataFinal)
                        {
                            switch (model.Frequencia)
                            {
                                case EFrequencia.Semanal:
                                    data = data.Value.AddDays(7);
                                    break;
                                case EFrequencia.TodosOsDias:
                                    data = data.Value.AddDays(1);
                                    break;
                                case EFrequencia.Mensal:
                                    data = data.Value.AddMonths(1);
                                    break;
                                case EFrequencia.Anual:
                                    data = data.Value.AddYears(1);
                                    break;
                                default:
                                    break;
                            }

                            if (data <= dataFinal)
                            {
                                var agendamentoVinculo = _mapper.Map<AgendamentoModel, Agendamento>(model);
                                agendamentoVinculo.DataInicio = data;
                                agendamentoVinculo.Chave = Guid.NewGuid();
                                agendamento.Vinculos.Add(agendamentoVinculo);
                            }
                        }
                    }

                    _agendamentoService.Insert(agendamento);
                    return new JsonResult(Ok());
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest(ex.Message));
            }
        }

        [Route("[controller]/Excluir/{id}")]
        [HttpDelete]
        public JsonResult ExcluirAgendamento(int id)
        {
            try
            {
                var agendamentoDb = _agendamentoService.GetById(Convert.ToInt32(id));
                _agendamentoService.Delete(agendamentoDb);
                return new JsonResult(Ok());                
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest(ex.Message));
            }
        }

        private string GetInicioEmail()
        {
            return $@"
                        <head>
                            <style>
                                body {{
                                font-family: Arial, sans-serif;
                                background-color: #f5f5f5;
                                }}
                                
                                .container {{
                                max-width: 600px;
                                margin: 0 auto;
                                padding: 20px;
                                background-color: #ffffff;
                                border: 1px solid #cccccc;
                                }}
                                
                                .header {{
                                text-align: center;
                                margin-bottom: 30px;
                                }}
                                
                                .title {{
                                color: #333333;
                                font-size: 24px;
                                margin-bottom: 10px;
                                }}
                                
                                .subtitle {{
                                color: #777777;
                                font-size: 18px;
                                margin-bottom: 20px;
                                }}
                                
                                .appointment {{
                                background-color: #f9f9f9;
                                border-radius: 5px;
                                padding: 10px;
                                margin-bottom: 20px;
                                }}
                                
                                .appointment-title {{
                                color: #333333;
                                font-size: 20px;
                                margin-bottom: 5px;
                                }}
                                
                                .appointment-details {{
                                color: #777777;
                                font-size: 16px;
                                }}
                                
                                .footer {{
                                text-align: center;
                                margin-top: 30px;
                                color: #777777;
                                font-size: 14px;
                                }}
                            </style>
                        </head>

                        <body>
                            <div class=""container"">
                                <div class=""header"">
                                <h1 class=""title"">Atualização de Consulta</h1>
                                <h2 class=""subtitle"">Informações atualizadas sobre a sua consulta médica</h2>
                                </div>
                                
                                <div class=""appointment"">";
        }

        private string GetFinalEmail(Agendamento agendamento)
        {
            return $@"
                    <p class=""appointment-details"">
                        Especialidade: {agendamento.Especialidade.ToString()}<br>
                        Data: {agendamento.DataInicio?.ToString("dd/MM/yyyy") ?? "N/A"}<br>
                        Hora: {agendamento.HoraInicio?.ToString(@"hh\:mm") ?? "N/A"} até {agendamento.HoraFinal?.ToString(@"hh\:mm") ?? "N/A"}<br>
                    </p>
                    </div>
                                
                        <div class=""footer"">
                        <p>Se você tiver alguma dúvida ou precisar de mais informações, entre em contato conosco.</p>
                        <p>Atenciosamente,</p>
                        <p>A equipe de agendamentos</p>
                        </div>
                    </div>
                </body>
            ";
        }

    }
}
