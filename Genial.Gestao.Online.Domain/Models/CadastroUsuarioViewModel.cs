using Genial.Gestao.Online.Domain.Authorization;
using Genial.Gestao.Online.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genial.Gestao.Online.Domain.Models
{
    public class CadastroUsuarioViewModel : ResponseBase
    {

        public Usuario DadosUsuario { get; set; }
        public bool IsInsert { get; set; }        



        public CadastroUsuarioViewModel() : base()
        {
            DadosUsuario = new Usuario();
        }

        public bool IsValido()
        {
            return DadosUsuario != null && !string.IsNullOrEmpty(DadosUsuario.UserName) && !string.IsNullOrEmpty(DadosUsuario.Senha)
                && !string.IsNullOrEmpty(DadosUsuario.Celular);
        }
    }
}
