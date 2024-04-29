using Library.Api.Entities;
using Library.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepo;
        public BooksController(IBookRepository bookRepo)
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

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var book = _bookRepo.GetBookById(id);
            if (book is not null)
            {
                return Ok(book);
            }
            else
            {
                return NotFound($"Data not found with this id {id}");
            }
        }

        [HttpPut]
        public IActionResult Put(Books books)
        {
            if (books is null || books.BookID == 0)
            {
                return BadRequest("Data not found.");
            }
            try
            {
                _bookRepo.UpdateBook(books.BookID, books);
                return Ok("Data Update Successfully.");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _bookRepo.DeleteBookById(id);
                return Ok("Data Deleted!");

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

    }
}
