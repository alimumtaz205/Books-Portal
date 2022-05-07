using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPortalAPI.Models.Authors
{
    public class AuthorsModel
    {
        public int authorId { get; set; }
        public string userName { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string authorName { get; set; }
        public string biography { get; set; }
        //public string picture { get; set; }
        public byte[] picture { get; set; }
    }
}
