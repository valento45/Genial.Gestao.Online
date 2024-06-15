using Genial.Gestao.Online.Domain.Models;
using Genial.Gestao.Online.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Genial.Gestao.Online.WebApp.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public IActionResult Cadastro()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(CadastroUsuarioViewModel cadastroUsuarioViewModel)
        {
            CadastroUsuarioViewModel result;

            if (cadastroUsuarioViewModel.DadosUsuario?.IdUsuario > 0)
                result = await _usuarioService.Atualizar(cadastroUsuarioViewModel.DadosUsuario);
            else          
                result = await _usuarioService.Incluir(cadastroUsuarioViewModel?.DadosUsuario);
            

            if (result.OperationSucess)
                return View("SucessoMessage");
            else
                return View(nameof(Cadastro), result);
        }
    }
}
