using MRA.Bookings.Models;
using MRA.Bookings.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace MRA.Bookings.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookRepository _bookRepository;


        public BookController(ILogger<BookController> logger, IBookRepository bookRepository)
        {
            _logger = logger;
            _bookRepository = bookRepository;
        }

        [HttpGet]
        [Route("GetAllBooks")]
        public IActionResult GetBooks()
        {
            var books = _bookRepository.GetBooks();
            var response = JsonConvert.SerializeObject(books);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetBooksByUserId")]
        public IActionResult GetBooksByUserId(string data)
        {
            Guid userId = JsonConvert.DeserializeObject<Guid>(data);
            return Ok(JsonConvert.SerializeObject(_bookRepository.GetBooksByUser(userId)));
        }

        [HttpPost]
        [Route("AddBook")]
        public IActionResult AddBook(string data)
        {
            Book book = JsonConvert.DeserializeObject<Book>(data);
            _bookRepository.AddBook(book);

            return Ok();
        }

        [HttpDelete]
        [Route("deleteBook")]
        public IActionResult DeleteBook(string data)
        {
            Guid bookId = JsonConvert.DeserializeObject<Guid>(data);
            _bookRepository.DeleteBook(bookId);

            return Ok();
        }

        [HttpPut]
        [Route("updateBook")]
        public IActionResult UpdateBook(string data)
        {
            Book book = JsonConvert.DeserializeObject<Book>(data);
            _bookRepository.UpdateBook(book);

            return Ok();
        }
    }
}
