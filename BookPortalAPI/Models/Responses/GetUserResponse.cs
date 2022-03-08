using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPortalAPI.Models.Responses
{
    public class GetUserResponse : BaseResponse
    {
        public User user { get; set; }

        public string userName { get; set; }
    }
}
