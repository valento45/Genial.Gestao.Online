using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genial.Gestao.Online.Domain.Bases
{
    public class Pessoa
    {
        public string Nome { get; set; }
        public string NomeSocial { get; set; }
        public string Celular { get; set; }
        public Sexo Sexo { get; set; }
        public DateTime DataNascimento { get; set; }    
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Complemento { get; set; }
    }

    public enum Sexo
    {
        Masculino = 0,
        Feminino = 1
    }
}
