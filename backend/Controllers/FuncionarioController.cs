using AutoMapper;
using MC426_Backend.Domain.Entities;
using MC426_Backend.Domain.Interfaces.Services;
using MC426_Backend.Infrastructure.Identity;
using MC426_Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MC426_Backend.Controllers
{
    [ApiController]
    public class FuncionarioController : Controller
    {
        private readonly IFuncionarioService _funcionarioService;
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;

        public FuncionarioController(IFuncionarioService funcionarioService, IMapper mapper, UserManager<Usuario> userManager)
        {
            _funcionarioService = funcionarioService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [Route("[controller]/Cadastro")]
        [HttpPost]
        public async Task<JsonResult> CadastrarFuncionario(FuncionarioModel model)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(BadRequest(ModelState.Values.SelectMany(e => e.Errors)));
            }

            try
            {
                var funcionario = _mapper.Map<FuncionarioModel, Funcionario>(model);

                var usuarioId = await InserirUsuario(model);
                funcionario.Chave = Guid.Parse(usuarioId);
                funcionario.Ativo = true;

                _funcionarioService.Insert(funcionario);

                return new JsonResult(Ok(funcionario));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest(ex.Message));
            }
        }

        private async Task<string> InserirUsuario(FuncionarioModel model)
        {
            var usuario = new Usuario()
            {
                Name = model.Nome,
                UserName = model.Email,
                NormalizedUserName = model.Email,
                Email = model.Email,
                NormalizedEmail = model.Email,
                PhoneNumber = model.Telefone
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

            await _userManager.AddToRoleAsync(usuario, "Funcionário");

            return usuario.Id;
        }

        [Route("[controller]/GetAll")]
        [HttpGet]
        public JsonResult GetAllFuncionarios()
        {
            try
            {
                var funcionarios = _funcionarioService.GetAll().ToList();
                return new JsonResult(Ok(funcionarios));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest(ex.Message));
            }
        }

    }
}
