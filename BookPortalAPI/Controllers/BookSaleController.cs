﻿using BookPortalAPI.Models.Booksale.Request;
using BookPortalAPI.Models.Booksale.Response;
using BookPortalAPI.Repositories.Booksale;
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
    public class BookSaleController : ControllerBase
    {
        private readonly IBookSaleRepository _bookSaleRepository;

        public BookSaleController(IBookSaleRepository bookSaleRepository)
        {
            _bookSaleRepository = bookSaleRepository;
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetBookSale()
        {
            GetBookSaleResponse response = new GetBookSaleResponse();
            try
            {
                response = _bookSaleRepository.GetBookSale();
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
        public async Task<IActionResult> AddBookSale([FromBody] AddBookSaleRequest request)
        {
            AddBookSaleResponse response = new AddBookSaleResponse();
            try
            {
                response = _bookSaleRepository.AddBookSale(request);
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
        public async Task<IActionResult> DeleteBookSale([FromBody] DeleteBookSaleRequest request)
        {
            DeleteBookSaleResponse response = new DeleteBookSaleResponse();
            try
            {
                response = _bookSaleRepository.DeleteBookSale(request);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message.ToString();
                //response.ResCode = TransactionCodes.DELETE_BUNDLE_ERROR;
            }
            return Ok(response);
        }

        //[Route("[action]")]
        //[HttpPost]
        //public async Task<IActionResult> UpdateBook([FromBody] UpdateBookRequest request)
        //{
        //    UpdateBookResponse response = new UpdateBookResponse();
        //    try
        //    {
        //        response = _bookRepository.UpdateBook(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Message = ex.Message.ToString();
        //        //response.ResCode = Common.TransactionCodes.UPDATE_ACTIVITY_ERROR;
        //    }
        //    return Ok(response);
        //}
    }
}
