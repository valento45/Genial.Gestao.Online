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
        public string Nome { get; set; }

        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Compare("Senha")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public bool AutenticaDoisFatores { get; set; }
        public string Celular { get; set; }
        public bool CelularConfirmed { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Complemento { get; set; }

        public TipoUsuario Tipo { get; set; }

        public Usuario()
        {

        }

        public bool CheckPassword(string passwordHash)
        {
            return this.Senha.Equals(passwordHash);
        }


    }

    public enum TipoUsuario : int
    {
        Vendedor = 0,
        Consultor = 1,
        Administrador = 2
    }
}
