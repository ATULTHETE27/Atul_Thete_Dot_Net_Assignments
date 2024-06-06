﻿using VisitorSecurityClearanceSystem.DTO;

namespace VisitorSecurityClearanceSystem.Services
{
    public interface IVisitorService
    {
        Task<Visitor> AddVisitor(Visitor visitorModel);
        Task<IEnumerable<Visitor>> GetAllVisitors();
        Task<Visitor> GetVisitorById(string id);
        Task<Visitor> UpdateVisitor(string id, Visitor visitorModel);
        Task<Visitor> UpdateVisitorStatus(string visitorId, bool newStatus);
        Task<List<Visitor>> GetVisitorsByStatus(bool status);

        Task DeleteVisitor(string id);
    }
}
