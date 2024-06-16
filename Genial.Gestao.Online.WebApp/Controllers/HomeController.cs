using Genial.Gestao.Online.Domain.Authorization;
using Genial.Gestao.Online.Domain.Models;
using Genial.Gestao.Online.WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Genial.Gestao.Online.WebApp.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, UserManager<Usuario> userManager) : base(userManager) 
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logar(LoginViewModel loginViewModel)
        {
            if (loginViewModel != null)
            {

                var result = await base.Autenticar(loginViewModel);
                if (result != null)
                {
                    return View("Administrativo/PainelAdministrador", result);
                }
                else
                {
                    loginViewModel.Message = "Usuário ou senha inválidos, por favor verifique.";

                    return View("Login", loginViewModel);
                }
            }
            else
            {
                loginViewModel = new LoginViewModel();
                loginViewModel.Message = "Ocorreu um erro ao recuperar as informações, por favor tente mais tarde.";

                return View("Login", loginViewModel);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}