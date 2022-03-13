using BookPortalAPI.Models;
using BookPortalAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public readonly ILoginRepository _IloginRepository;

        public AccountController(ILoginRepository loginRepository)
        {
            _IloginRepository = loginRepository;
            // _accessor = accessor;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserRequest request)
        {
            LoginUserResponse response = new LoginUserResponse();
            try
            {
                response = _IloginRepository.LoginUser(request);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message.ToString();
                //response.tranCode = Common.TransactionCodes.LOGIN_USER_ERROR;
            }
            return Ok(response);
        }
    }
}
