using Library_Management_System.Model;
using Library_Management_System.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ICosmosDbService _cosmosDbService;
        private const string ContainerName = "Book";

        public BookController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return Ok(await _cosmosDbService.GetItemsAsync<Book>(ContainerName, "SELECT * FROM c"));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(string id)
        {
            var book = await _cosmosDbService.GetItemAsync<Book>(ContainerName, id);
            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpGet("ByName/{name}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookByName(string name)
        {
            var books = await _cosmosDbService.GetItemsAsync<Book>(ContainerName, $"SELECT * FROM c WHERE c.title = '{name}'");
            if (books == null)
            {
                return NotFound();
            }

            return Ok(books);
        }

        [HttpGet("Available")]
        public async Task<ActionResult<IEnumerable<Book>>> GetAvailableBooks()
        {
            var books = await _cosmosDbService.GetItemsAsync<Book>(ContainerName, "SELECT * FROM c WHERE c.isIssued = false");
            return Ok(books);
        }

        [HttpGet("Issued")]
        public async Task<ActionResult<IEnumerable<Book>>> GetIssuedBooks()
        {
            var books = await _cosmosDbService.GetItemsAsync<Book>(ContainerName, "SELECT * FROM c WHERE c.isIssued = true");
            return Ok(books);
        }

        [HttpPost]
        public async Task<ActionResult> PostBook([FromBody] Book book)
        {
            book.Id = Guid.NewGuid().ToString();
            await _cosmosDbService.AddItemAsync(ContainerName, book);
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(string id, [FromBody] Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            await _cosmosDbService.UpdateItemAsync(ContainerName, id, book);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(string id)
        {
            await _cosmosDbService.DeleteItemAsync<Book>(ContainerName, id);
            return NoContent();
        }
    }
}
