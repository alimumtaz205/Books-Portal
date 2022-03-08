using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPortalAPI.Models
{
    public class BaseResponse
    {
        //public TransactionCodes tranCode { get; set; }
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
