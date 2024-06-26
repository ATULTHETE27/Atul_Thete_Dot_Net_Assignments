using VisitorSecurityClearanceSystem.Entites;

namespace VisitorSecurityClearanceSystem.CosmoDB
{
    public interface ICosmoDBService
    {

        Task<IEnumerable<T>> GetAll<T>();
        Task<T> Add<T>(T entity);
        Task<T> Update<T>(T entity);


        Task<VisitorEntity> GetVisitorByEmail(string email);
        Task<List<VisitorEntity>> GetVisitorByStatus(bool status);
        Task<VisitorEntity> GetVisitorById(string id);
        Task<IEnumerable<VisitorEntity>> GetAllVisitor();
        Task DeleteVisitor(string id);


        Task<SecurityEntity> GetSecurityById(string id);
        Task DeleteSecurity(string id);
        Task<SecurityEntity> GetSecurityUserByEmail(string email);


        Task<ManagerEntity> GetManagerById(string id);
        Task DeleteManager(string id);


        Task<OfficeEntity> GetOfficeById(string id);
        Task<OfficeEntity> GetOfficeUserByEmail(string email);
        Task DeleteOffice(string id);

    }
}
