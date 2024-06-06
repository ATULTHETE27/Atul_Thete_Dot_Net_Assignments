using VisitorSecurityClearanceSystem.DTO;

namespace VisitorSecurityClearanceSystem.Services
{
    public interface IOfficeService
    {
        Task<Office> AddOffice(Office officeModel);
        Task<Office> GetOfficeById(string id);
        Task<Office> UpdateOffice(string id, Office officeModel);
        Task DeleteOffice(string id);

        Task<Office> LoginOfficeUser(string email, string password);
    }
}
