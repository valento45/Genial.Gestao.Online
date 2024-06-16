using Genial.Gestao.Online.Domain.Authorization;
using Genial.Gestao.Online.Domain.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genial.Gestao.Online.Repository.Interfaces
{
    public interface IUsuarioRepository
    {

        Task<OperationResult> Incluir(Usuario usuario);
        Task<OperationResult> Atualizar(Usuario usuario);
        Task<OperationResult> Excluir(int idUsuario);
        Task<IEnumerable<Usuario>> ObterTodos();
        Task<Usuario> ObterById(int idUsuario);
        Task<IEnumerable<Usuario>> ObterByEmail(string email);
        Task<Usuario> ObterByUserName(string userName);
        Task<IEnumerable<Usuario>> ObterByNome(string nome);
    }
}
