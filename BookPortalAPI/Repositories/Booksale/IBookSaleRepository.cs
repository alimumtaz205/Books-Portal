using BookPortalAPI.Models.Booksale.Request;
using BookPortalAPI.Models.Booksale.Response;

namespace BookPortalAPI.Repositories.Booksale
{
    public interface IBookSaleRepository
    {
        public GetBookSaleResponse GetBookSale();
        public AddBookSaleResponse AddBookSale(AddBookSaleRequest request);
        public DeleteBookSaleResponse DeleteBookSale(DeleteBookSaleRequest request);
    }
}