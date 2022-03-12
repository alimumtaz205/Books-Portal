using BookPortalAPI.Models.Books.Response;
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
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            GetBookResponse response = new GetBookResponse();
            try
            {
                response = _bookRepository.GetBooks();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message.ToString();
                // response.ResCode = Convert.ToString((int)TranCodes.Exception);
            }

            return Ok(response);
        }
    }
}
