using Library.Api.Entities;
using Library.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberRepository _membRepo;
        public MembersController(IMemberRepository membRepo)
        {
            _membRepo = membRepo;
        }
        [HttpPost]
        public IActionResult Post(Members members)
        {
            try
            {
                _membRepo.CreateMember(members);
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
            var members = _membRepo.GetAllMembers();
            if (members.Count == 0)
            {
                return NotFound("Data Not Found");
            }
            return Ok(members);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var memb = _membRepo.GetMemberById(id);
            if (memb is not null)
            {
                return Ok(memb);
            }
            else
            {
                return NotFound($"Data not found with this id {id}");
            }

        }

        [HttpPut]
        public IActionResult Put(Members members)
        {
            if (members is null || members.MemberID == 0)
            {
                return BadRequest("Data not found.");
            }
            try
            {
                _membRepo.UpdateMember(members.MemberID, members);
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
                _membRepo.DeleteMemberById(id);
                return Ok("Data Deleted!");

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

    }
}
