using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genial.Gestao.Online.Domain.Responses
{
    public class ResponseBase
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool OperationSucess { get; set; }

        public ResponseBase()
        {

        }
    }
}
