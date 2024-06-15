using Genial.Gestao.Online.Domain.Authorization;
using Genial.Gestao.Online.Domain.Bases;
using Genial.Gestao.Online.Domain.Models;
using Genial.Gestao.Online.Repository.Interfaces;
using Genial.Gestao.Online.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genial.Gestao.Online.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<CadastroUsuarioViewModel> Incluir(Usuario model)
        {
            var result = new CadastroUsuarioViewModel { DadosUsuario = model };

            var operationResult = await _usuarioRepository.Incluir(model);

            result.OperationSucess = operationResult.Success;
            result.Message = operationResult.Message;
            result.IsInsert = operationResult.Success;

            return result;

        }
        public async Task<CadastroUsuarioViewModel> Atualizar(Usuario model)
        {
            var result = new CadastroUsuarioViewModel { DadosUsuario = model };

            var operationResult = await _usuarioRepository.Atualizar(model);

            result.OperationSucess = operationResult.Success;
            result.Message = operationResult.Message;
            result.IsInsert = operationResult.Success;

            return result;
        }

        public Task<OperationResult> Excluir(int idUsuario)
        {
            throw new NotImplementedException();
        }


    }
}
