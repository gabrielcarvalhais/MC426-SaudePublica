using AutoMapper;
using MC426_Backend.Domain.Interfaces.Services;
using MC426_Backend.Infrastructure.Identity;
using MC426_Backend.Models;
using MC426_Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MC426_Backend.Controllers
{
    [ApiController]
    public class PacienteController : Controller
    {
        private readonly IPacienteService _pacienteService;
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;

        public PacienteController(IPacienteService pacienteService, IMapper mapper, UserManager<Usuario> userManager)
        {
            _pacienteService = pacienteService;
            _mapper = mapper;
            _userManager = userManager;        
        }

        [Route("[controller]/Cadastro")]
        [HttpPost]
        public async Task<JsonResult> CadastrarPaciente(PacienteModel model)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(BadRequest(ModelState.Values.SelectMany(e => e.Errors)));
            }

            try
            {
                var paciente = _mapper.Map<PacienteModel, Paciente>(model);

                var usuarioId = await InserirUsuario(model);
                paciente.Chave = Guid.Parse(usuarioId);
                paciente.Ativo = true;

                _pacienteService.Insert(paciente);
                
                return new JsonResult(Ok(paciente));                
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest(ex.Message));
            }
        }

        private async Task<string> InserirUsuario(PacienteModel model)
        {
            var usuario = new Usuario()
            {
                UserName = model.Email,
                NormalizedUserName = model.Email,
                Email = model.Email,
                NormalizedEmail = model.Email
            };

            var result = await _userManager.CreateAsync(usuario, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                throw new Exception($"{ModelState.Values.SelectMany(e => e.Errors).FirstOrDefault()}");
            }

            await _userManager.AddToRoleAsync(usuario, "Paciente");

            return usuario.Id;
        }

    }
}
