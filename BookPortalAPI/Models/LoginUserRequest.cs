using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookPortalAPI.Models
{
    public class LoginUserRequest
    {
        /// <example>mumtazali</example>
        [Required(ErrorMessage = "UserName Required")]
        public string userName { get; set; }
        /// <example>123456789</example>
        [Required(ErrorMessage = "Password Required")]
        public string uPassword { get; set; }
    }
}
