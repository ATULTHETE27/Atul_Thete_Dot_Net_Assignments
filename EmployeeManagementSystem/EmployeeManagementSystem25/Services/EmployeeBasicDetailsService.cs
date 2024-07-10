using AutoMapper;
using EmployeeManagementSystem25.Common;
using EmployeeManagementSystem25.CosmosDB;
using EmployeeManagementSystem25.DTO;
using EmployeeManagementSystem25.Entities;
using EmployeeManagementSystem25.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace EmployeeManagementSystem25.Services
{
    public class EmployeeBasicDetailsService : IEmployeeBasicDetailsService
    {
        private readonly ICosmosDBService _cosmosDBService;
        private readonly IMapper _mapper;

        public EmployeeBasicDetailsService(ICosmosDBService cosmosDBService, IMapper mapper)
        {
            _cosmosDBService = cosmosDBService;
            _mapper = mapper;
        }

        public async Task<EmployeeBasicDetailsEntity> AddEmployeeBasicDetails(EmployeeBasicDetailsEntity employeeEntity)
        {
            employeeEntity.Initialize(true, "Employee", "Atul", "Atul");
            employeeEntity = await _cosmosDBService.AddEmployeeBasicDetails(employeeEntity);
            return employeeEntity;
        }



        public async Task<List<EmployeeBasicDetailsEntity>> GetAllEmployeeBasicDetails()
        {
            return await _cosmosDBService.GetAllEmployeeBasicDetails();
        }



        public async Task<EmployeeBasicDetailsDTO> GetEmployeeBasicDetailsByUId(string UId)
        {
            var response = await _cosmosDBService.GetEmployeeBasicDetailsByUId(UId);
            return _mapper.Map<EmployeeBasicDetailsDTO>(response);

        }

        public async Task<EmployeeBasicDetailsDTO> UpdateUserDetailsByUId(string UId, EmployeeBasicDetailsDTO employeeBasicDetailsDTO)
        {
            var existingUser = await _cosmosDBService.GetEmployeeBasicDetailsByUId(UId);
            if (existingUser == null)
            {
                return null;
            }

            existingUser.Active = false;
            existingUser.Archieved = true;

            await _cosmosDBService.ReplaceAsync(UId, existingUser);

            existingUser.Initialize(false, Credentials.EmployeeDataType, "Atul", "Atul");
            _mapper.Map(employeeBasicDetailsDTO, existingUser);

            existingUser = await _cosmosDBService.AddEmployeeBasicDetails(existingUser);
            return _mapper.Map<EmployeeBasicDetailsDTO>(existingUser);
        }

        public async Task<EmployeeBasicDetailsDTO> DeleteUserByUId(string UId)
        {
            var existingUser = await _cosmosDBService.GetEmployeeBasicDetailsByUId(UId);
            existingUser.Archieved = true;
            await _cosmosDBService.ReplaceAsync(UId, existingUser);

            existingUser.Initialize(false, Credentials.EmployeeDataType, "Atul", "Atul");
            existingUser.Active = false;
            existingUser.Archieved = true;
            var response = await _cosmosDBService.AddEmployeeBasicDetails(existingUser);
            return _mapper.Map<EmployeeBasicDetailsDTO>(response);

        }

        public async Task<EmployeeBasicDetailsDTO> AddEmployeeBasicDetails(EmployeeBasicDetailsDTO employee)
        {
            var entity = _mapper.Map<EmployeeBasicDetailsEntity>(employee);
            entity.Initialize(true, "Employee", "Atul", "Atul");

            var response = await _cosmosDBService.AddEmployeeBasicDetails(entity); // Await the async operation

            return _mapper.Map<EmployeeBasicDetailsDTO>(response);
        }

        public async Task<List<EmployeeBasicDetailsEntity>> GetAllEmployeeBasicDetailsByRole(string role)
        {
            try
            {
                var allEmployees = await GetAllEmployeeBasicDetails();
                return allEmployees.FindAll(a => a.Role == role);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error in GetAllEmployeeBasicDetailsByRole method", ex);
            }
        }

        public async Task<EmployeeFilterCriteria> GetAllEmployeeBasicDetailsByPagination(EmployeeFilterCriteria employeeFilterCriteria)
        {
            EmployeeFilterCriteria criteria = new EmployeeFilterCriteria();

            var employee = await GetAllEmployeeBasicDetails();
            criteria.totalCount = employee.Count;

            var skip = employeeFilterCriteria.PageSize * (employeeFilterCriteria.Page - 1);

            employee = employee.Skip(skip).Take(employeeFilterCriteria.PageSize).ToList();

            employeeFilterCriteria.Employee = employee;

            return criteria;
        }

        public async Task<VisitorDTO> AddVisitorByMakePostRequest(VisitorDTO visitor)
        {
            var serialObj = JsonConvert.SerializeObject(visitor);
            var requestObj = await HttpClientHelper.MakePostRequest(Credentials.VisitorUrl, Credentials.AddVisitorEndPoint, serialObj);
            var responseObj = JsonConvert.DeserializeObject<VisitorDTO>(requestObj);
            return responseObj;

        }

        public async Task<IEnumerable<VisitorDTO>> GetVisitorByMakePostRequest()
        {
            var responseString = await HttpClientHelper.MakeGetRequest(Credentials.VisitorUrl, Credentials.GetAllVisitorEndPoint);
            var employees = JsonConvert.DeserializeObject<IEnumerable<VisitorDTO>>(responseString);
            return employees;
        }
    }
}