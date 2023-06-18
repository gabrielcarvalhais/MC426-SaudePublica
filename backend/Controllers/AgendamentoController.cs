using AutoMapper;
using MC426_Backend.ApplicationService.Services;
using MC426_Backend.Domain.Entities;
using MC426_Backend.Domain.Enums;
using MC426_Backend.Domain.Interfaces.Services;
using MC426_Backend.Infrastructure.Identity;
using MC426_Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MC426_Backend.Controllers
{
    [ApiController]
    public class AgendamentoController : Controller
    {
        private readonly IAgendamentoService _agendamentoService;
        private readonly IPacienteService _pacienteService;
        private readonly IMapper _mapper;

        public AgendamentoController(IAgendamentoService agendamentoService, IPacienteService pacienteService, IMapper mapper)
        {
            _agendamentoService = agendamentoService;
            _pacienteService = pacienteService;
            _mapper = mapper;
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
                    var paciente = _pacienteService.GetByChave(Guid.Parse(filtro.UserId));
                    agendamentos = agendamentos.Where(x => x.PacienteId == paciente.Id).ToList();
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
                    return new JsonResult(Ok());
                }
                //insert
                else
                {
                    var agendamento = _mapper.Map<AgendamentoModel, Agendamento>(model);
                    agendamento.Chave = Guid.NewGuid();                                        

                    _agendamentoService.Insert(agendamento);
                    return new JsonResult(Ok());
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest(ex.Message));
            }
        }
    }
}
