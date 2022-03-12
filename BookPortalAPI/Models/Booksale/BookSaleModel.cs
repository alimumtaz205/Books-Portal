using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPortalAPI.Models.Booksale
{
    public class BookSaleModel
    {
        public int saleId { get; set; }
        public int bookId { get; set; }
        public string price { get; set; }
        public string dateOfSale { get; set; }
    }
}
