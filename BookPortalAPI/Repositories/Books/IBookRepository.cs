

using BookPortalAPI.Models.Books.Response;

namespace BookPortalAPI.Repositories
{
    public interface IBookRepository
    {
        public GetBookResponse GetBooks();
    }
}