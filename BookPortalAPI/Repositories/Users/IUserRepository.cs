
using BookPortalAPI.Models.Users.Request;
using BookPortalAPI.Models.Users.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookPortalAPI.Repositories
{
    public interface IUserRepository
    {
        public AddUserResponse AddUser(AddUserRequest request);
        public DeleteUserResponse DeleteUser(DeleteUserRequest request);
        public UpdateUserResponse UpdateUser(UpdateUserRequest request);
        public GetUserResponse GetUser();
    }
}