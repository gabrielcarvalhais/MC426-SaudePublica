using MC426_Backend.Infrastructure.Identity;
using MC426_Backend.Models;
using Microsoft.AspNetCore.Authentication;
using MC426_Backend.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MC426_Backend.Controllers
{
    [ApiController]    
    public class AutenticacaoController : Controller
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly IPacienteService _pacienteService;
        private readonly IFuncionarioService _funcionarioService;


        public AutenticacaoController(SignInManager<Usuario> signInManager, UserManager<Usuario> userManager, IPacienteService pacienteService, IFuncionarioService funcionarioService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _pacienteService = pacienteService;
            _funcionarioService = funcionarioService;
        }

        [Route("[controller]/Login")]
        [HttpPost]
        public async Task<JsonResult> Login(LoginModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(BadRequest(ModelState.Values.SelectMany(e => e.Errors)));
            }

            try
            {
                var result = await _signInManager.PasswordSignInAsync(userModel.Email, userModel.Password, true, false);

                if (result.Succeeded)
                {
                    var usuario = await _userManager.FindByEmailAsync(userModel.Email);

                    var roles = _userManager.GetRolesAsync(usuario).Result;

                    string userRoleId = null!;
                    if (roles[0] == "Paciente")
                        userRoleId = _pacienteService.GetByChave(Guid.Parse(usuario.Id)).Id.ToString();
                    else
                        userRoleId = _funcionarioService.GetByChave(Guid.Parse(usuario.Id)).Id.ToString();

                    var nome = usuario.Name ?? "";
                    var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id));
                    identity.AddClaim(new Claim(ClaimTypes.Name, nome));
                    identity.AddClaim(new Claim(ClaimTypes.UserData, userRoleId));
                    identity.AddClaim(new Claim(ClaimTypes.Email, usuario.Email));

                    foreach (var role in roles)
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, role));
                    }

                    await Request.HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(identity));
                    return new JsonResult(Ok());
                }
                else
                {
                    ModelState.AddModelError("", "E-mail ou senha inválidos.");
                    return new JsonResult(BadRequest(ModelState.Values.SelectMany(e => e.Errors)));
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest(ex.Message));
            }
        }

        [Route("[controller]/Logout")]
        [HttpGet]
        public async Task<JsonResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return new JsonResult(Ok());
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest(ex.Message));
            }
        }

        [Route("[controller]/UsuarioLogado")]
        [HttpGet]
        public JsonResult UsuarioLogado()
        {
            try
            {
                bool usuarioLogado = User.Identity.IsAuthenticated;
                var claims = User.Claims.ToList();
                var retorno = new
                {
                    UsuarioLogado = usuarioLogado,
                    Claims = claims
                };
                return new JsonResult(Ok(retorno));
            }
            catch(Exception ex)
            {
                return new JsonResult(BadRequest(ex.Message));
            }            
        }
    }
}
