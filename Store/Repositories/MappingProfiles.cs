using AutoMapper;
using Store.Models;
using Store.Models.DTO;

namespace Store.Repositories
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, DTOProduct>(MemberList.Destination).ReverseMap();
            CreateMap<Group, DTOGroup>(MemberList.Destination).ReverseMap();
            CreateMap<Warehouse, DTOWarehouse>(MemberList.Destination).ReverseMap();
        }
    }
}
