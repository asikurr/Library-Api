using Library.Api.Entities;
using Library.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IAuthorRepository _authorRepo;
        public AuthorsController(IAuthorRepository authorRepo)
        {
            _authorRepo = authorRepo;
        }
        [HttpPost]
        public IActionResult Post(Authors authors)
        {
            try
            {
                _authorRepo.CreateAuthor(authors);
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
            var authors = _authorRepo.GetAllAuthors();
            if (authors.Count == 0)
            {
                return NotFound("Data Not Found");
            }
            return Ok(authors);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var auth = _authorRepo.GetAuthorById(id);
            if (auth is not null)
            {
                return Ok(auth);
            }
            else
            {
                return NotFound($"Data not found with this id {id}");
            }

        }
        [HttpPut]
        public IActionResult Put(Authors authors)
        {
            if (authors is null || authors.AuthorID == 0)
            {
                return BadRequest("Data not found.");
            }
            try
            {
                _authorRepo.UpdateAuthor(authors.AuthorID, authors);
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
                _authorRepo.DeleteAuthorById(id);
                return Ok("Data Deleted!");

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

    }
}
