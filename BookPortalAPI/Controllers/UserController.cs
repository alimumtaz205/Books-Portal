using BookPortalAPI.Models.Requests;
using BookPortalAPI.Models.Responses;
using BookPortalAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookPortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiBaseController
    {

        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            GetUserResponse response = new GetUserResponse();
            try
            {

                response = _userRepository.GetUser();
            }
            catch (Exception ex)
            {
               // _GlobalInfo.LogError(ex, "GetDevices/DeviceMgtController", "1001", "", JsonConvert.SerializeObject(request));
                // res.IsSuccess = false; set by default false
                response.Message = ex.Message.ToString();
                //response.tranCode = Common.TransactionCodes.GET_DEVICE_ERROR;
            }

            return Ok(response);
        }

        [Route("[action/id]")]
        [HttpGet]
        public async Task<IActionResult> GetUserById(GetUserRequest req)
        {
            GetUserResponse response = new GetUserResponse();
            try
            {

                response = _userRepository.GetUserById(req);
            }
            catch (Exception ex)
            {
                // _GlobalInfo.LogError(ex, "GetDevices/DeviceMgtController", "1001", "", JsonConvert.SerializeObject(request));
                // res.IsSuccess = false; set by default false
                response.Message = ex.Message.ToString();
                //response.tranCode = Common.TransactionCodes.GET_DEVICE_ERROR;
            }

            return Ok(response);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> AddUser(GetUserRequest req)
        {
            GetUserResponse response = new GetUserResponse();
            try
            {

                response = _userRepository.AddUser(req);
            }
            catch (Exception ex)
            {
                // _GlobalInfo.LogError(ex, "GetDevices/DeviceMgtController", "1001", "", JsonConvert.SerializeObject(request));
                // res.IsSuccess = false; set by default false
                response.Message = ex.Message.ToString();
                //response.tranCode = Common.TransactionCodes.GET_DEVICE_ERROR;
            }

            return Ok(response);
        }

        [Route("[action]")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(GetUserRequest req)
        {
            GetUserResponse response = new GetUserResponse();
            try
            {

                response = _userRepository.DeleteUser(req);
            }
            catch (Exception ex)
            {
                // _GlobalInfo.LogError(ex, "GetDevices/DeviceMgtController", "1001", "", JsonConvert.SerializeObject(request));
                // res.IsSuccess = false; set by default false
                response.Message = ex.Message.ToString();
                //response.tranCode = Common.TransactionCodes.GET_DEVICE_ERROR;
            }

            return Ok(response);
        }

        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> UpdateUser(GetUserRequest req)
        {
            GetUserResponse response = new GetUserResponse();
            try
            {

                response = _userRepository.UpdateUser(req);
            }
            catch (Exception ex)
            {
                // _GlobalInfo.LogError(ex, "GetDevices/DeviceMgtController", "1001", "", JsonConvert.SerializeObject(request));
                // res.IsSuccess = false; set by default false
                response.Message = ex.Message.ToString();
                //response.tranCode = Common.TransactionCodes.GET_DEVICE_ERROR;
            }

            return Ok(response);
        }

    }
}
