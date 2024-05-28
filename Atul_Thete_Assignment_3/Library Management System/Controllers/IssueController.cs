using Library_Management_System.Model;
using Library_Management_System.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly ICosmosDbService _cosmosDbService;
        private const string ContainerName = "Issue";

        public IssueController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Issue>>> GetIssues()
        {
            return Ok(await _cosmosDbService.GetItemsAsync<Issue>(ContainerName, "SELECT * FROM c"));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Issue>> GetIssue(string id)
        {
            var issue = await _cosmosDbService.GetItemAsync<Issue>(ContainerName, id);
            if (issue == null)
            {
                return NotFound();
            }

            return issue;
        }

        [HttpPost]
        public async Task<ActionResult> PostIssue([FromBody] Issue issue)
        {
            issue.Id = Guid.NewGuid().ToString();
            await _cosmosDbService.AddItemAsync(ContainerName, issue);

            // Update the book status to issued
            var book = await _cosmosDbService.GetItemAsync<Book>("Book", issue.BookId);
            if (book != null)
            {
                book.IsIssued = true;
                await _cosmosDbService.UpdateItemAsync("Book", book.Id, book);
            }

            return CreatedAtAction(nameof(GetIssue), new { id = issue.Id }, issue);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutIssue(string id, [FromBody] Issue issue)
        {
            if (id != issue.Id)
            {
                return BadRequest();
            }

            await _cosmosDbService.UpdateItemAsync(ContainerName, id, issue);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIssue(string id)
        {
            await _cosmosDbService.DeleteItemAsync<Issue>(ContainerName, id);
            return NoContent();
        }
    }
}
