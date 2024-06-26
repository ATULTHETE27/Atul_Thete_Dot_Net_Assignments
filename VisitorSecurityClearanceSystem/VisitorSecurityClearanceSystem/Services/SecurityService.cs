using AutoMapper;
using VisitorSecurityClearanceSystem.CosmoDB;
using VisitorSecurityClearanceSystem.DTO;
using VisitorSecurityClearanceSystem.Entites;
using VisitorSecurityClearanceSystem.Interface;

namespace VisitorSecurityClearanceSystem.Services
{
    public class SecurityService:ISecurityService
    {
        private readonly ICosmoDBService _cosmoDBService;
        private readonly IMapper _autoMapper;
        public SecurityService(ICosmoDBService cosmoDBService, IMapper mapper)
        {
            _cosmoDBService = cosmoDBService;
            _autoMapper = mapper;
        }

        public async Task<SecurityDTO> AddSecurity(SecurityDTO securityModel)
        {

            var securityEntity = _autoMapper.Map<SecurityEntity>(securityModel);

            securityEntity.Intialize(true, "security", "Atul", "Atul");

            var response = await _cosmoDBService.Add(securityEntity);

            return _autoMapper.Map<SecurityDTO>(response);
        }

        public async Task<SecurityDTO> GetSecurityById(string id)
        {
            var security = await _cosmoDBService.GetSecurityById(id); 
            return _autoMapper.Map<SecurityDTO>(security);
        }

        public async Task<SecurityDTO> UpdateSecurity(string id, SecurityDTO securityModel)
        {
            var securityEntity = await _cosmoDBService.GetSecurityById(id);
            if (securityEntity == null)
            {
                throw new Exception("Security not found");
            }
            securityEntity = _autoMapper.Map<SecurityEntity>(securityModel);
            securityEntity.Id = id;
            var response = await _cosmoDBService.Update(securityEntity);
            return _autoMapper.Map<SecurityDTO>(response);
        }

        public async Task DeleteSecurity(string id)
        {
            await _cosmoDBService.DeleteSecurity(id);
        }

        public async Task<SecurityDTO> LoginSecurityUser(string email, string password)
        {
            var securityUser = await _cosmoDBService.GetSecurityUserByEmail(email);

            if (securityUser == null || securityUser.Password != password)
            {
                return null; 
            }

            var securityDto = new SecurityDTO
            {
                Id = securityUser.Id,
                Name = securityUser.Name,
                Email = securityUser.Email,
            };

            return securityDto;
        }
    }
}
