using BookPortalAPI.Models.Requests;
using BookPortalAPI.Models.Responses;

namespace BookPortalAPI.Repositories
{
    public interface IUserRepository
    {
        public GetUserResponse GetUser();
        GetUserResponse AddUser(GetUserRequest req);
        GetUserResponse DeleteUser(GetUserRequest req);
        GetUserResponse UpdateUser(GetUserRequest req);
        GetUserResponse GetUserById(GetUserRequest req);
    }
}