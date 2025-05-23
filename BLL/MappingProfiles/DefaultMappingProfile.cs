using AutoMapper;
using BLL.Models;
using DAL.Models;

namespace BLL.MappingProfiles
{
    public class DefaultMappingProfile : Profile
    {
        public DefaultMappingProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Lots, opt => opt.MapFrom(src => src.Lots))
                .ForMember(dest => dest.Bids, opt => opt.MapFrom(src => src.Bids));
            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.Lots, opt => opt.MapFrom(src => src.Lots))
                .ForMember(dest => dest.Bids, opt => opt.MapFrom(src => src.Bids));
            CreateMap<CreateUserDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
        }
    }
}
