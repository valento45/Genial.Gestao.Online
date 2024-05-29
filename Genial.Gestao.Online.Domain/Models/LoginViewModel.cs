using Genial.Gestao.Online.Securitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genial.Gestao.Online.Domain.Models
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public string PasswordHash
        {
            get
            {
                return Security.Encrypt(Senha);
            }
        }
        public string Message { get; set; }

        public bool LembrarMe { get; set; }

        public LoginViewModel()
        {

        }
    }
}
