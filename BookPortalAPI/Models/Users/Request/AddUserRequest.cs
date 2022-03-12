using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPortalAPI.Models.Users.Request
{
    public class AddUserRequest
    {
        public string userName { get; set; }
        public string fullName { get; set; }
        public string uPassword { get; set; }
    }
}
