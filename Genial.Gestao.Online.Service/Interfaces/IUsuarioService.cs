using Genial.Gestao.Online.Domain.Authorization;
using Genial.Gestao.Online.Domain.Bases;
using Genial.Gestao.Online.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genial.Gestao.Online.Service.Interfaces
{
    public interface IUsuarioService
    {
        Task<CadastroUsuarioViewModel> Incluir(Usuario model);
        Task<CadastroUsuarioViewModel> Atualizar(Usuario model);
        Task<OperationResult> Excluir(int idUsuario);
    }
}
