using Genial.Gestao.Online.Domain.Authorization;
using Genial.Gestao.Online.Domain.Bases;
using Genial.Gestao.Online.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genial.Gestao.Online.Repository
{
    public class UsuarioRepository : RepositoryBase, IUsuarioRepository
    {
      
        public UsuarioRepository(IDbConnection dbConnection) : base(dbConnection)
        {

        }

        public async Task<OperationResult> Atualizar(Usuario usuario)
        {
            OperationResult result;

            string query = "UPDATE usuario_tb Nome = @Nome, Senha = @Senha, Email = @Email, EmailConfirmed = @EmailConfirmed, " +
                "Tipo = @Tipo, Celular = @Celular, CelularConfirmed = @CelularConfirmed, TwoFactorEnabled = @TwoFactorEnabled, " +
                "CEP = @CEP, Logradouro = @Logradouro, Numero = @Numero, Cidade = @Cidade, UF = @UF, Complemento = @Complemento" +
                " WHERE IdUsuario = @IdUsuario";

            var hasUpdated = await base.ExecuteAsync(query, new
            {
                IdUsuario = usuario.IdUsuario,
                Nome = usuario.Nome,
                UserName = usuario.UserName,
                Senha = usuario.Senha,
                Email = usuario.Email,
                EmailConfirmed = usuario.EmailConfirmed,
                Tipo = (int)usuario.Tipo,
                Celular = usuario.Celular,
                CelularConfirmed = usuario.CelularConfirmed,
                TwoFactorEnabled = usuario.TwoFactorEnabled,
                CEP = usuario.CEP,
                Logradouro = usuario.Logradouro,
                Numero = usuario.Numero,
                Cidade = usuario.Cidade,
                UF = usuario.UF,
                Complemento = usuario.Complemento
            });


            if (hasUpdated)
                result = new OperationResult("Dados atualizados com sucesso", hasUpdated);
            else
                result = new OperationResult(base.GetMessage(), base.GetOperationSucess());

            return result;
        }

        public Task<OperationResult> Excluir(int idUsuario)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> Incluir(Usuario usuario)
        {
            OperationResult result;

            var inserted = await base.ExecuteAsync("insert into usuario_tb (Nome, UserName, Senha, Email, EmailConfirmed, Tipo, Celular, CelularConfirmed, TwoFactorEnabled, CEP, Logradouro, Numero, Cidade, UF, Complemento) VALUES (" +
              "@Nome, @UserName, @Senha, @Email, @EmailConfirmed, @Tipo, @Celular, @CelularConfirmed, @TwoFactorEnabled, @CEP, @Logradouro, @Numero, @Cidade, @UF, @Complemento)",
              new
              {
                  Nome = usuario.Nome,
                  UserName = usuario.UserName,
                  Senha = usuario.Senha,
                  Email = usuario.Email,
                  EmailConfirmed = usuario.EmailConfirmed,
                  Tipo = (int)usuario.Tipo,
                  Celular = usuario.Celular,
                  CelularConfirmed = usuario.CelularConfirmed,
                  TwoFactorEnabled = usuario.TwoFactorEnabled,
                  CEP = usuario.CEP,
                  Logradouro = usuario.Logradouro,
                  Numero = usuario.Numero,
                  Cidade = usuario.Cidade,
                  UF = usuario.UF,
                  Complemento = usuario.Complemento
              });

            if (inserted)
                result = new OperationResult("Usuário cadastrado com sucesso.", inserted);
            else
                result = new OperationResult(base.GetMessage(), base.GetOperationSucess());

            return result;
        }

        public Task<Usuario> ObterByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> ObterById(int idUsuario)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Usuario>> ObterByNome(string nome)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Usuario>> ObterByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Usuario>> ObterTodos()
        {
            throw new NotImplementedException();
        }
    }
}
