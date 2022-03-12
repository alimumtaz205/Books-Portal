using BookPortalAPI.Models.Finance.Request;
using BookPortalAPI.Models.Finance.Response;
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
    public class FinanceController : ControllerBase
    {

        private readonly IFinanceRepository _financeRepository;

        public FinanceController(IFinanceRepository financeRepository)
        {
            _financeRepository = financeRepository;
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetFinance()
        {
            GetFinanceResponse response = new GetFinanceResponse();
            try
            {
                response = _financeRepository.GetFinance();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message.ToString();
                // response.ResCode = Convert.ToString((int)TranCodes.Exception);
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddFinance([FromBody] AddFinanceRequest request)
        {
            AddFinanceResponse response = new AddFinanceResponse();
            try
            {
                response = _financeRepository.AddFinance(request);
                // response.ResCode = C;
                //response.IsSuccess = true;
                //response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message.ToString();
                // response.ResCode = Convert.ToString((int)TranCodes.Exception);
            }

            return Ok(response);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> DeleteFinance([FromBody] DeleteFinanceRequest request)
        {
            DeleteFinanceResponse response = new DeleteFinanceResponse();
            try
            {
                response = _financeRepository.DeleteFinance(request);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message.ToString();
                //response.ResCode = TransactionCodes.DELETE_BUNDLE_ERROR;
            }
            return Ok(response);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> UpdateFinance([FromBody] UpdateFinanceRequest request)
        {
            UpdateFinanceResponse response = new UpdateFinanceResponse();
            try
            {
                response = _financeRepository.UpdateFinance(request);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message.ToString();
                //response.ResCode = Common.TransactionCodes.UPDATE_ACTIVITY_ERROR;
            }
            return Ok(response);
        }
    }
}
