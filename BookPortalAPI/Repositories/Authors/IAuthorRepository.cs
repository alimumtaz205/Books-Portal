using BookPortalAPI.Models.Authors.Request;
using BookPortalAPI.Models.Authors.Response;

namespace BookPortalAPI.Repositories
{
    public interface IAuthorRepository
    {
       public GetAuthorsResponse GetAuthors();
       public AddAuthorsResponse AddAuthor(AddAuthorsRequest request);
    }
}