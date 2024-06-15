using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genial.Gestao.Online.Domain.Bases
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public DateTime DataHoraOperacao { get; private set; }


        public OperationResult()
        {
            DataHoraOperacao = DateTime.Now;
        }


        public OperationResult(string message, bool sucess)
        {
            Success = sucess;
            Message = message;
            DataHoraOperacao = DateTime.Now;
        }
    }
}
