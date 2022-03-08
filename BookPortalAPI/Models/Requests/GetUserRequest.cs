using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPortalAPI.Models.Requests
{
    public class GetUserRequest 
    {
        public User user { get; set; }
        ///<example> MediaPad 7 Youth 2</example>
        public string userName { get; set; }
        ///<example></example>
        public string fullName { get; set; }

        ///<example> </example>
        public string uPassword { get; set; }
    }
}
