using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Genial.Gestao.Online.Domain.Authorization
{
    public class Usuario : IdentityUser<int>
    {

        public int IdUsuario { get; set; }
        public int IdPessoa { get; set; }
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Senha { get; set; }
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public bool AutenticaDoisFatores { get; set; }
        public string Celular { get; set; }
        public bool EmailVerificado { get; set; }
        


        public Usuario()
        {

        }

        public bool CheckPassword(string passwordHash)
        {
            return this.Senha.Equals(passwordHash);
        }
    }
}
