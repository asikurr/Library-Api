using Library.Api.Entities;
using Library.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookBorrowedsController : ControllerBase
    {
        private readonly IBorrowedBooksRepository _bookBorRepo;
        public BookBorrowedsController(IBorrowedBooksRepository bookBorRepo)
        {
            _bookBorRepo = bookBorRepo;
        }
        [HttpPost]
        public IActionResult Post(BorrowedBooks bookBor)
        {
            try
            {
                _bookBorRepo.CreateBorrowedBook(bookBor);
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
            var bookBors = _bookBorRepo.GetAllBorrowedBooks();
            if (bookBors.Count == 0)
            {
                return NotFound("Data Not Found");
            }
            return Ok(bookBors);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var bookBor = _bookBorRepo.GetBorrowedBookById(id);
            if (bookBor is not null)
            {
                return Ok(bookBor);
            }
            else
            {
                return NotFound($"Data not found with this id {id}");
            }
        }

        [HttpPut]
        public IActionResult Put(BorrowedBooks bookBors)
        {
            if (bookBors is null || bookBors.BorrowID == 0)
            {
                return BadRequest("Data not found.");
            }
            try
            {
                _bookBorRepo.UpdateBorrowedBook(bookBors.BorrowID, bookBors);
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
                _bookBorRepo.DeleteBorrowedBookById(id);
                return Ok("Data Deleted!");

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}
