using AutoMapper;
using BLL.CreateDTOs;
using BLL.DTOs;
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
            CreateMap<Bid, BidDTO>();
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
