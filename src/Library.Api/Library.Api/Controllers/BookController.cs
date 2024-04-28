using Library.Api.Entities;
using Library.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepo;
        public BookController(IBookRepository bookRepo)
        {
            _bookRepo = bookRepo;
        }
        [HttpPost]
        public IActionResult Post(Books book)
        {
            try
            {
                _bookRepo.CreateBook(book);
                return Ok("Data Saved.");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult Get()
        {
            var books = _bookRepo.GetAllBooks();
            if (books.Count == 0)
            {
                return NotFound("Data Not Found");
            }
            return Ok(books);
        }
    }
}
