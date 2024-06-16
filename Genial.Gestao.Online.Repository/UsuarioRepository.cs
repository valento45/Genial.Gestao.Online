using Genial.Gestao.Online.Domain.Authorization;
using Genial.Gestao.Online.Domain.Bases;
using Genial.Gestao.Online.Repository.Interfaces;
using Genial.Gestao.Online.Securitys;
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

            string query = "UPDATE sys.usuario_tb Nome = @Nome, Senha = @Senha, Email = @Email, EmailConfirmed = @EmailConfirmed, " +
                "Tipo = @Tipo, Celular = @Celular, CelularConfirmed = @CelularConfirmed, TwoFactorEnabled = @TwoFactorEnabled, " +
                "CEP = @CEP, Logradouro = @Logradouro, Numero = @Numero, Cidade = @Cidade, UF = @UF, Complemento = @Complemento" +
                " WHERE IdUsuario = @IdUsuario";

            var hasUpdated = await base.ExecuteAsync(query, new
            {
                IdUsuario = usuario.IdUsuario,
                Nome = usuario.Nome,
                UserName = usuario.UserName,
                Senha = Security.Encrypt(usuario.Senha),
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

            var inserted = await base.ExecuteAsync("insert into sys.usuario_tb (Nome, UserName, Senha, Email, EmailConfirmed, Tipo, Celular, CelularConfirmed, TwoFactorEnabled, CEP, Logradouro, Numero, Cidade, UF, Complemento) VALUES (" +
              "@Nome, @UserName, @Senha, @Email, @EmailConfirmed, @Tipo, @Celular, @CelularConfirmed, @TwoFactorEnabled, @CEP, @Logradouro, @Numero, @Cidade, @UF, @Complemento)",
              new
              {
                  Nome = usuario.Nome,
                  UserName = usuario.UserName,
                  Senha = Security.Encrypt(usuario.Senha),
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

        public async Task<IEnumerable<Usuario>> ObterByEmail(string email)
        {
            string query = $"select * from sys.usuario_tb where UPPER(Email) like '%{email.ToUpper()}%'";

            return await base.QueryAsync<Usuario>(query);
        }

        public async Task<Usuario> ObterById(int idUsuario)
        {
            string query = $"select * from sys.usuario_tb where IdUsuario = {idUsuario}";

            return await base.QueryFirsAsync<Usuario>(query);
        }

        public async Task<IEnumerable<Usuario>> ObterByNome(string nome)
        {
            string query = $"select * from sys.usuario_tb where UPPER(Nome) like '%{nome.ToUpper()}%'";

            return await base.QueryAsync<Usuario>(query);
        }

        public async Task<Usuario> ObterByUserName(string userName)
        {
            string query = $"select * from sys.usuario_tb where UPPER(UserName) like '%{userName.ToUpper()}%'";

            return await base.QueryFirsAsync<Usuario>(query);
        }

        public async Task<IEnumerable<Usuario>> ObterTodos()
        {
            string query = $"select * from sys.usuario_tb ";

            return await base.QueryAsync<Usuario>(query);
        }
    }
}
