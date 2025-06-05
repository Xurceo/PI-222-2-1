using AutoMapper;
using BLL.CreateDTOs;
using BLL.DTOs;
using BLL.ShortDTOs;
using DAL.Models;

namespace BLL.MappingProfiles
{
    public class DefaultMappingProfile : Profile
    {
        public DefaultMappingProfile()
        {
            // User <-> UserDTO
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.BidIds, opt => opt.MapFrom(src => src.Bids.Select(b => b.Id)))
                .ForMember(dest => dest.LotIds, opt => opt.MapFrom(src => src.Lots.Select(l => l.Id)));
            CreateMap<UserDTO, User>();
            CreateMap<CreateUserDTO, User>();

            // Bid <-> BidDTO
            CreateMap<Bid, BidDTO>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.LotId, opt => opt.MapFrom(src => src.Lot.Id));
            CreateMap<BidDTO, Bid>();

            // Lot <-> LotDTO
            CreateMap<Lot, LotDTO>()
                .ForMember(dest => dest.BidIds, opt => opt.MapFrom(src => src.Bids.Select(b => b.Id)));
            CreateMap<LotDTO, Lot>();
            CreateMap<CreateLotDTO, Lot>();

            // Category <-> CategoryDTO
            CreateMap<Category, CategoryDTO>()
                .ForMember(dest => dest.LotIds, opt => opt.MapFrom(src => src.Lots.Select(l => l.Id)))
                .ForMember(dest => dest.SubcategoryIds, opt => opt.MapFrom(src => src.Subcategories.Select(sc => sc.Id)));
            CreateMap<CategoryDTO, Category>();
            CreateMap<CreateCategoryDTO, Category>();
        }
    }
}
