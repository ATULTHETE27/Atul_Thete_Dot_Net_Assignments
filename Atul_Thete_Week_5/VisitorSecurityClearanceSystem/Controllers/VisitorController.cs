using Microsoft.AspNetCore.Mvc;
using VisitorSecurityClearanceSystem.DTO;
using VisitorSecurityClearanceSystem.Services;

namespace VisitorSecurityClearanceSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VisitorController : Controller
    {
        private readonly IVisitorService _visitorService;

        public VisitorController(IVisitorService visitorService)
        {
            _visitorService = visitorService;
        }

        [HttpPost]
        public async Task<Visitor> AddVisitor(Visitor visitorModel)
        {
            return await _visitorService.AddVisitor(visitorModel);
        }

        [HttpGet("{id}")]
        public async Task<Visitor> GetVisitorById(string id)
        {
            return await _visitorService.GetVisitorById(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVisitor(string id, Visitor visitorModel)
        {
            try
            {
                var updatedVisitor = await _visitorService.UpdateVisitor(id, visitorModel);
                return Ok(updatedVisitor);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateVisitor (Controller): {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisitor(string id)
        {
            await _visitorService.DeleteVisitor(id);
            return NoContent();
        }

    }
}
