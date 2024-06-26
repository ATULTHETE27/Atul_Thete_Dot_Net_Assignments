﻿using AutoMapper;
using VisitorSecurityClearanceSystem.CosmoDB;
using VisitorSecurityClearanceSystem.DTO;
using VisitorSecurityClearanceSystem.Entites;
using VisitorSecurityClearanceSystem.Interface;

namespace VisitorSecurityClearanceSystem.Services
{
    public class ManagerService : IManagerService
    {
        private readonly ICosmoDBService _cosmoDBService;
        private readonly IMapper _autoMapper;

        public ManagerService(ICosmoDBService cosmoDBService, IMapper mapper)
        {
            _cosmoDBService = cosmoDBService;
            _autoMapper = mapper;
        }

        public async Task<ManagerDTO> AddManager(ManagerDTO managerModel)
        {

            var managerEntity = _autoMapper.Map<ManagerEntity>(managerModel);

            managerEntity.Intialize(true, "manager", "Atul", "Atul");

            var response = await _cosmoDBService.Add(managerEntity);

            return _autoMapper.Map<ManagerDTO>(response);
        }

        public async Task<ManagerDTO> GetManagerById(string id)
        {
            var security = await _cosmoDBService.GetManagerById(id); 
            return _autoMapper.Map<ManagerDTO>(security);
        }

        public async Task<ManagerDTO> UpdateManager(string id, ManagerDTO managerModel)
        {
            var managerEntity = await _cosmoDBService.GetManagerById(id);
            if (managerEntity == null)
            {
                throw new Exception("Manager not found");
            }
            managerEntity = _autoMapper.Map<ManagerEntity>(managerModel);
            managerEntity.Id = id;
            var response = await _cosmoDBService.Update(managerEntity);
            return _autoMapper.Map<ManagerDTO>(response);
        }

        public async Task DeleteManager(string id)
        {
            await _cosmoDBService.DeleteManager(id);
        }

       
    }
}