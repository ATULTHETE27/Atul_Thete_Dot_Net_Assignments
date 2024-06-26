using EmployeeManagementSystem.Entities;

namespace EmployeeManagementSystem.CosmoDB
{
    public interface ICosmoDBService
    {
        Task<T> Add<T>(T entity);
        Task<T> Update<T>(string id,T entity);

        Task DeleteAdditionalDetails(string id);
        Task<EmployeeAdditionalDetailsEntity> GetEmployeeAdditionalDetailsById(string id);
        Task<IEnumerable<EmployeeAdditionalDetailsEntity>> GetAllAdditionalDetails();


        Task<EmployeeBasicDetailsEntity> GetEmployeeBasicDetailsById(string id);
        Task DeleteBasicDetails(string id);
        Task<IEnumerable<EmployeeBasicDetailsEntity>> GetAllBasicDetails();

    }
}
