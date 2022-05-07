using BookPortalAPI.Models.Authors.Request;
using BookPortalAPI.Models.Authors.Response;

namespace BookPortalAPI.Repositories
{
    public interface IAuthorRepository
    {
       public GetAuthorsResponse GetAuthors();
       public GetAuthorsResponse GetAuthorsByUsername(GetAuthorsRequest req);
       public AddAuthorsResponse AddAuthor(AddAuthorsRequest request);
       public DeleteAuthorResponse DeleteAuthor(DeleteAuthorRequest request);
       public UpdateAuthorsResponse UpdateAuthor(UpdateAuthorRequest request);
    }
}