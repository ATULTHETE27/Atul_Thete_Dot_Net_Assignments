using AutoMapper;
using VisitorSecurityClearanceSystem.DTO;
using VisitorSecurityClearanceSystem.Entites;

namespace VisitorSecurityClearanceSystem.Common
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<VisitorEntity,Visitor>().ReverseMap();
            CreateMap<SecurityEntity,Security>().ReverseMap();
            CreateMap<OfficeEntity,Office>().ReverseMap();
            CreateMap<ManagerEntity,Manager>().ReverseMap();
        }
    }
}
