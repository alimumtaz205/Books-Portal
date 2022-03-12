using BookPortalAPI.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPortalAPI.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private string Code;
        private bool IsSuccess;
        private string Message;
        public readonly IConfiguration _configuration;

        public Task<BaseResponse> LoginUser(LoginRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
