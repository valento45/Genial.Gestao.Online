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

            if (cadastroUsuarioViewModel.IsValido())
            {
                CadastroUsuarioViewModel result;
                var user = await _usuarioService.ObterByUserName(cadastroUsuarioViewModel.DadosUsuario?.UserName);

                if (user == null)
                {
                    result = await _usuarioService.Incluir(cadastroUsuarioViewModel?.DadosUsuario);


                    if (result.OperationSucess)
                        return View("SucessoMessage");
                    else
                        return View(nameof(Cadastro), result);
                }
                else
                {
                    cadastroUsuarioViewModel.Message = $"Usuário '{cadastroUsuarioViewModel.DadosUsuario.UserName}'" +
                        $" Já existe um usuário, por favor escolha outro.";
                    return View(nameof(Cadastro), cadastroUsuarioViewModel);
                }

            }
            else
            {
                cadastroUsuarioViewModel.Message = "Preencha todos os campos obrigatórios para prosseguir";
                return View(nameof(Cadastro), cadastroUsuarioViewModel);
            }
        }
    }
}
