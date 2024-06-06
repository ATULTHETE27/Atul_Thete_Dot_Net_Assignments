using VisitorSecurityClearanceSystem.DTO;

namespace VisitorSecurityClearanceSystem.Services
{
    public interface IManagerService
    {
        Task<Manager> AddManager(Manager managerModel);
        Task<Manager> GetManagerById(string id);
        Task<Manager> UpdateManager(string id, Manager managerModel);
        Task DeleteManager(string id);



    }
}
