using VisitorSecurityClearanceSystem.DTO;

namespace VisitorSecurityClearanceSystem.Services
{
    public interface ISecurityService
    {
        Task<Security> AddSecurity(Security securityModel);
        Task<Security> GetSecurityById(string id);
        Task<Security> UpdateSecurity(string id, Security securityModel);
        Task DeleteSecurity(string id);

        Task<Security> LoginSecurityUser(string email, string password);
    }
}
