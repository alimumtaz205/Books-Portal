using BookPortalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPortalAPI.Repositories
{
    public interface ILoginRepository
    {
        public LoginUserResponse LoginUser(LoginUserRequest request);
    }
}
