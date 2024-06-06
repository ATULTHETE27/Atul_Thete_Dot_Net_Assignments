﻿using Microsoft.AspNetCore.Mvc;
using VisitorSecurityClearanceSystem.DTO;
using VisitorSecurityClearanceSystem.Services;

namespace VisitorSecurityClearanceSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ManagerController : Controller
    {
        private readonly IManagerService _managerService;
        private readonly IOfficeService _officeService;
        private readonly ISecurityService _securityService;
        private readonly IVisitorService _visitorService;

        public ManagerController(IManagerService managerService, IOfficeService officeService, ISecurityService securityService, IVisitorService visitorService)
        {
            _managerService = managerService;
            _officeService = officeService;
            _securityService = securityService;
            _visitorService = visitorService;

        }

    [HttpPost]
        public async Task<Manager> AddManager(Manager managerModel)
        {
            return await _managerService.AddManager(managerModel);
        }


        [HttpGet("{id}")]
        public async Task<Manager> GetManagerById(string id)
        {
            return await _managerService.GetManagerById(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateManager(string id, Manager managerModel)
        {

            try
            {
                var updatedManager = await _managerService.UpdateManager(id, managerModel);
                return Ok(updatedManager);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateVisitor (Controller): {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManager(string id)
        {
            await _managerService.DeleteManager(id);
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> AddOfficeUser(Office officeModel)
        {
            var office = await _officeService.AddOffice(officeModel);
            return Ok(office);
        }

        [HttpPost]
        public async Task<IActionResult> AddSecurityUser(Security securityModel)
        {
            var security = await _securityService.AddSecurity(securityModel);
            return Ok(security);
        }

        [HttpPut("{visitorId}")]
        public async Task<IActionResult> UpdateVisitorStatus(string visitorId,  bool newStatus)
        {
            try
            {
                var updatedVisitor = await _visitorService.UpdateVisitorStatus(visitorId, newStatus);
                return Ok(updatedVisitor);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateVisitorStatus (Controller): {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{status}")]
        public async Task<IActionResult> GetVisitorsByStatus(bool status)
        {
            var visitors = await _visitorService.GetVisitorsByStatus(status);
            return Ok(visitors);
        }



        
    }
}
