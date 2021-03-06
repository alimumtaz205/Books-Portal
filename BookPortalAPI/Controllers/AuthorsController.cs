using BookPortalAPI.Models.Authors.Request;
using BookPortalAPI.Models.Authors.Response;
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
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            GetAuthorsResponse response = new GetAuthorsResponse();
            try
            {
                response = _authorRepository.GetAuthors();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message.ToString();
                // response.ResCode = Convert.ToString((int)TranCodes.Exception);
            }

            return Ok(response);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> GetAuthorsByUsername(GetAuthorsRequest req)
        {
            GetAuthorsResponse response = new GetAuthorsResponse();
            try
            {
                 response = _authorRepository.GetAuthorsByUsername(req);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message.ToString();
                // response.ResCode = Convert.ToString((int)TranCodes.Exception);
            }

            return Ok(response);
        }


        [Route("[action]")]
        [HttpPost]
        public IActionResult AddAuthor([FromBody] AddAuthorsRequest request)
        {
            AddAuthorsResponse response = new AddAuthorsResponse();
            try
            {
                response = _authorRepository.AddAuthor(request);
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
        public async Task<IActionResult> DeleteAuthor([FromBody] DeleteAuthorRequest request)
        {
            DeleteAuthorResponse response = new DeleteAuthorResponse();
            try
            {
                response = _authorRepository.DeleteAuthor(request);
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
        public async Task<IActionResult> UpdateAuthor([FromBody] UpdateAuthorRequest request)
        {
            UpdateAuthorsResponse response = new UpdateAuthorsResponse();
            try
            {
                response = _authorRepository.UpdateAuthor(request);
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
