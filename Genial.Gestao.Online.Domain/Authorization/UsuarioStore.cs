using Dapper;
using Genial.Gestao.Online.Securitys;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Genial.Gestao.Online.Domain.Authorization
{
    public class UsuarioStore : IUserStore<Usuario>, IUserPasswordStore<Usuario>, IUserClaimStore<Usuario>
    {
        protected readonly IDbConnection _connection;


        public UsuarioStore(IDbConnection connection)
        {
            _connection = connection;

        }


        public async Task AddClaimsAsync(Usuario user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {

        }

        public async Task<IdentityResult> CreateAsync(Usuario user, CancellationToken cancellationToken)
        {
            var inserted = await _connection.ExecuteAsync("insert into sys.usuario_tb (Nome, UserName, Senha, Email, EmailConfirmed, Tipo, Celular, CelularConfirmed, TwoFactorEnabled, CEP, Logradouro, Numero, Cidade, UF, Complemento) VALUES (" +
                "@Nome, @UserName, @Senha, @Email, @EmailConfirmed, @Tipo, @Celular, @CelularConfirmed, @TwoFactorEnabled, @CEP, @Logradouro, @Numero, @Cidade, @UF, @Complemento)",
                new
                {
                    Nome = user.Nome,
                    UserName = user.UserName,
                    Senha = Security.Encrypt(user.Senha),
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    Tipo = (int)user.Tipo,
                    Celular = user.Celular,
                    CelularConfirmed = user.CelularConfirmed,
                    TwoFactorEnabled = user.TwoFactorEnabled,
                    CEP = user.CEP,
                    Logradouro = user.Logradouro,
                    Numero = user.Numero,
                    Cidade = user.Cidade,
                    UF = user.UF,
                    Complemento = user.Complemento
                });

            if (inserted > 0)
                return IdentityResult.Success;
            else
                return IdentityResult.Failed(new IdentityError[] { new IdentityError() { Description = "Erro ao inserir o usuário." } });

        }

        public async Task<IdentityResult> DeleteAsync(Usuario user, CancellationToken cancellationToken)
        {
            var identityResult = new IdentityResult();

            string query = "delete from sys.usuario_tb where IdUsuario = " + user.IdUsuario;
            var success = await _connection.ExecuteAsync(query);

            if (!(success > 0))
                identityResult.Errors.ToList().Add(new IdentityError { Code = "500", Description = "Erro ao excluir usuário" });


            return identityResult;

        }

        public void Dispose()
        {

        }

        public async Task<Usuario?> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            string query = "select * from sys.usuario_tb WHERE IdUsuario = " + userId;
            return await _connection.QueryFirstAsync<Usuario>(query);
        }

        public async Task<Usuario?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            string query = $"select * from sys.usuario_tb WHERE UPPER(UserName) = '{normalizedUserName}'";
            return await _connection.QueryFirstAsync<Usuario>(query);
        }

        public async Task<IList<Claim>> GetClaimsAsync(Usuario user, CancellationToken cancellationToken)
        {
            return new List<Claim>();
        }

        public async Task<string?> GetNormalizedUserNameAsync(Usuario user, CancellationToken cancellationToken)
        {
            return user.Email.ToUpper();
        }

        public async Task<string?> GetPasswordHashAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Security.Encrypt(user.Senha);
        }

        public async Task<string> GetUserIdAsync(Usuario user, CancellationToken cancellationToken)
        {
            return user.IdUsuario.ToString();
        }

        public async Task<string?> GetUserNameAsync(Usuario user, CancellationToken cancellationToken)
        {
            return user.Email;
        }

        public async Task<IList<Usuario>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            return new List<Usuario>();
        }

        public async Task<bool> HasPasswordAsync(Usuario user, CancellationToken cancellationToken)
        {
            return !string.IsNullOrEmpty(user.Senha);
        }

        public async Task RemoveClaimsAsync(Usuario user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {

        }

        public async Task ReplaceClaimAsync(Usuario user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {

        }

        public async Task SetNormalizedUserNameAsync(Usuario user, string? normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
        }

        public async Task SetPasswordHashAsync(Usuario user, string? passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
        }

        public async Task SetUserNameAsync(Usuario user, string? userName, CancellationToken cancellationToken)
        {
            user.UserName = user.UserName ?? string.Empty;
        }

        public async Task<IdentityResult> UpdateAsync(Usuario user, CancellationToken cancellationToken)
        {
            var identityResult = new IdentityResult();

            string query = "UPDATE sys.usuario_tb Nome = @Nome, Senha = @Senha, Email = @Email, EmailConfirmed = @EmailConfirmed, " +
                "Tipo = @Tipo, Celular = @Celular, CelularConfirmed = @CelularConfirmed, TwoFactorEnabled = @TwoFactorEnabled, " +
                "CEP = @CEP, Logradouro = @Logradouro, Numero = @Numero, Cidade = @Cidade, UF = @UF, Complemento = @Complemento" +
                " WHERE IdUsuario = @IdUsuario";

            var result = await _connection.ExecuteAsync(query, new
            {
                IdUsuario = user.IdUsuario,
                Nome = user.Nome,
                UserName = user.UserName,
                Senha = Security.Encrypt(user.Senha),
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                Tipo = (int)user.Tipo,
                Celular = user.Celular,
                CelularConfirmed = user.CelularConfirmed,
                TwoFactorEnabled = user.TwoFactorEnabled,
                CEP = user.CEP,
                Logradouro = user.Logradouro,
                Numero = user.Numero,
                Cidade = user.Cidade,
                UF = user.UF,
                Complemento = user.Complemento
            });

            if (result > 0)
                return identityResult;
            else
            {
                identityResult.Errors.ToList().Add(new IdentityError { Code = "500" });
                return identityResult;
            }
        }
    }
}
