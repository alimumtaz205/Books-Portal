using BookPortalAPI.Models.Books.Request;
using BookPortalAPI.Models.Books.Response;

namespace BookPortalAPI.Repositories.Books
{
    public interface IBookRepository
    {
       public GetBookResponse GetBooks();
       public AddBookResponse AddBook(AddBookRequest request);
       public DeleteBookResponse DeleteBook(DeleteBookRequest request);
       public UpdateBookResponse UpdateBook(UpdateBookRequest request);
    }
}