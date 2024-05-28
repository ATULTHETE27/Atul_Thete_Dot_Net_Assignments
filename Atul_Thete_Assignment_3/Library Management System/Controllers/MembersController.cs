using Library_Management_System.Model;
using Library_Management_System.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly ICosmosDbService _cosmosDbService;
        private const string ContainerName = "Member";

        public MemberController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
            return Ok(await _cosmosDbService.GetItemsAsync<Member>(ContainerName, "SELECT * FROM c"));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(string id)
        {
            var member = await _cosmosDbService.GetItemAsync<Member>(ContainerName, id);
            if (member == null)
            {
                return NotFound();
            }

            return member;
        }

        [HttpPost]
        public async Task<ActionResult> PostMember([FromBody] Member member)
        {
            member.Id = Guid.NewGuid().ToString();
            await _cosmosDbService.AddItemAsync(ContainerName, member);
            return CreatedAtAction(nameof(GetMember), new { id = member.Id }, member);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMember(string id, [FromBody] Member member)
        {
            if (id != member.Id)
            {
                return BadRequest();
            }

            await _cosmosDbService.UpdateItemAsync(ContainerName, id, member);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(string id)
        {
            await _cosmosDbService.DeleteItemAsync<Member>(ContainerName, id);
            return NoContent();
        }
    }
}
