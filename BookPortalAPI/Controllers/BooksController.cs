using BookPortalAPI.Models.Books.Request;
using BookPortalAPI.Models.Books.Response;
using BookPortalAPI.Repositories.Books;
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
        public async Task<IActionResult> GetBooks()
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

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddBook([FromBody] AddBookRequest request)
        {
            AddBookResponse response = new AddBookResponse();
            try
            {
                response = _bookRepository.AddBook(request);
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
        public async Task<IActionResult> DeleteBook([FromBody] DeleteBookRequest request)
        {
            DeleteBookResponse response = new DeleteBookResponse();
            try
            {
                response = _bookRepository.DeleteBook(request);
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
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookRequest request)
        {
            UpdateBookResponse response = new UpdateBookResponse();
            try
            {
                response = _bookRepository.UpdateBook(request);
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