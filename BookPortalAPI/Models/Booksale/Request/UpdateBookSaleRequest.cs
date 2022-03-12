using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPortalAPI.Models.Booksale.Request
{
    public class UpdateBookSaleRequest
    {
        public int saleId { get; set; }
        public int bookId { get; set; }
        public string price { get; set; }
        public string dateOfSale { get; set; }
    }
}
