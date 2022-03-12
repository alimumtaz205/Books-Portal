using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPortalAPI.Models.Finance.Request
{
    public class AddFinanceRequest
    {
        public int financeId { get; set; }
        public int bookId { get; set; }
        public int authorId { get; set; }
        public string totalSale { get; set; }
        public string revenue { get; set; }
    }
}
