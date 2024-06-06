using AutoMapper;
using VisitorSecurityClearanceSystem.CosmoDB;
using VisitorSecurityClearanceSystem.DTO;
using VisitorSecurityClearanceSystem.Entites;

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

        public async Task<Security> AddSecurity(Security securityModel)
        {

            var securityEntity = _autoMapper.Map<SecurityEntity>(securityModel);

            securityEntity.Intialize(true, "security", "Atul", "Atul");

            var response = await _cosmoDBService.Add(securityEntity);

            return _autoMapper.Map<Security>(response);
        }

        public async Task<Security> GetSecurityById(string id)
        {
            var security = await _cosmoDBService.GetSecurityById(id); 
            return _autoMapper.Map<Security>(security);
        }

        public async Task<Security> UpdateSecurity(string id, Security securityModel)
        {
            var securityEntity = await _cosmoDBService.GetSecurityById(id);
            if (securityEntity == null)
            {
                throw new Exception("Security not found");
            }
            securityEntity = _autoMapper.Map<SecurityEntity>(securityModel);
            securityEntity.Id = id;
            var response = await _cosmoDBService.Update(securityEntity);
            return _autoMapper.Map<Security>(response);
        }

        public async Task DeleteSecurity(string id)
        {
            await _cosmoDBService.DeleteSecurity(id);
        }

        public async Task<Security> LoginSecurityUser(string email, string password)
        {
            var securityUser = await _cosmoDBService.GetSecurityUserByEmail(email);

            if (securityUser == null || securityUser.Password != password)
            {
                return null; 
            }

            var securityDto = new Security
            {
                Id = securityUser.Id,
                Name = securityUser.Name,
                Email = securityUser.Email,
            };

            return securityDto;
        }
    }
}
