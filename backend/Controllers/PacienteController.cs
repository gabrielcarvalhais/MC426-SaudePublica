using AutoMapper;
using MC426_Backend.Domain.Interfaces.Services;
using MC426_Backend.Infrastructure.Identity;
using MC426_Backend.Models;
using MC426_Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MC426_Backend.Controllers
{
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

                var usuarioId = InserirUsuario(model);
                paciente.Chave = Guid.Parse(usuarioId.Result);

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
                NormalizedUserName = model.Email.ToUpper(),
                Email = model.Email,
                NormalizedEmail = model.Email.ToUpper()
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

            await AdicionaRolesUsuario(usuario, model.Roles);

            return usuario.Id;
        }

        private async Task AdicionaRolesUsuario(Usuario usuario, List<string> roles)
        {
            foreach (var role in roles)
            {
                await _userManager.AddToRoleAsync(usuario, role);                
            }
        }
    }
}
