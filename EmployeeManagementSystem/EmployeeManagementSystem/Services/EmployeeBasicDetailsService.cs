﻿using AutoMapper;
using EmployeeManagementSystem.CosmoDB;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entities;
using EmployeeManagementSystem.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeBasicDetailsService : IEmployeeBasicDetailsService
    {
        private readonly ICosmoDBService _cosmoDBService;
        private readonly IMapper _autoMapper;

        public EmployeeBasicDetailsService(ICosmoDBService cosmoDBService, IMapper mapper)
        {
            _cosmoDBService = cosmoDBService;
            _autoMapper = mapper;
        }

        public async Task<EmployeeBasicDetailsDTO> AddEmployeeBasicDetails(EmployeeBasicDetailsDTO employeeBasicDetails)
        {
            var entity = _autoMapper.Map<EmployeeBasicDetailsEntity>(employeeBasicDetails);
            entity.Intialize(true, "employeeBasicDetails", "Atul", "Atul");
            var response = await _cosmoDBService.Add(entity);
            return _autoMapper.Map<EmployeeBasicDetailsDTO>(response);
        }

        public async Task<IEnumerable<EmployeeBasicDetailsDTO>> GetAllEmployeeBasicDetails()
        {
            var entities = await _cosmoDBService.GetAllBasicDetails();
            return _autoMapper.Map<IEnumerable<EmployeeBasicDetailsDTO>>(entities);
        }

        public async Task<EmployeeBasicDetailsDTO> GetEmployeeBasicDetailsById(string id)
        {
            var entity = await _cosmoDBService.GetEmployeeBasicDetailsById(id);
            return _autoMapper.Map<EmployeeBasicDetailsDTO>(entity);
        }

        public async Task<EmployeeBasicDetailsDTO> UpdateEmployeeBasicDetails(string id, EmployeeBasicDetailsDTO employeeBasicDetails)
        {
            var entity = await _cosmoDBService.GetEmployeeBasicDetailsById(id);
            if (entity == null) throw new Exception("Employee not found");

            _autoMapper.Map(employeeBasicDetails, entity);
            entity.Intialize(false, "employeeBasicDetails", "System", "System");

            var response = await _cosmoDBService.Update(id, entity);
            return _autoMapper.Map<EmployeeBasicDetailsDTO>(response);
        }

        public async Task DeleteEmployeeBasicDetails(string id)
        {
            var entity = await _cosmoDBService.GetEmployeeBasicDetailsById(id);
            if (entity == null) throw new Exception("Employee not found");

            await _cosmoDBService.DeleteBasicDetails(id);
        }

        public async Task<EmployeeFilterCriteria> GetAllEmployeesByPagination(EmployeeFilterCriteria employeeFilterCriteria)
        {
            EmployeeFilterCriteria response = new EmployeeFilterCriteria();

            var checkFilter = employeeFilterCriteria.Filters.Any(x => x.fieldName == "role");
            var role = "";
            if (checkFilter)
            {
                role = employeeFilterCriteria.Filters.Find(a => a.fieldName == "role").fieldValue.FirstOrDefault();
            }

            var employees = await GetAllEmployeeBasicDetails();

            var filterRecords = string.IsNullOrEmpty(role) ? employees : employees.Where(a => a.Role == role);

            var employeeList = filterRecords.ToList();

            response.totalCount = employeeList.Count;

            response.page = employeeFilterCriteria.page;
            response.pageSize = employeeFilterCriteria.pageSize;

            if (response.page < 1) response.page = 1;
            if (response.pageSize < 1) response.pageSize = 10; 

            var skip = response.pageSize * (response.page - 1);

            var pagedRecords = employeeList.Skip(skip).Take(response.pageSize).ToList();

            response.Employees = pagedRecords;

            return response;
        }
    }

}
