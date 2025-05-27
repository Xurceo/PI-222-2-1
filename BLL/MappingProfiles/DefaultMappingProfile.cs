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
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<CreateUserDTO, User>();

            // Bid <-> BidDTO
            CreateMap<Bid, BidDTO>()
                .ForMember(dest => dest.Lot, opt => opt.Ignore())   // уникаємо циклу
                .ForMember(dest => dest.User, opt => opt.Ignore()); // уникаємо циклу
            CreateMap<BidDTO, Bid>();


            // Lot <-> LotDTO
            CreateMap<Lot, LotDTO>()
                .ForMember(dest => dest.Category, opt => opt.Ignore());
            CreateMap<LotDTO, Lot>();
            CreateMap<CreateLotDTO, Lot>();

            // Category <-> CategoryDTO
            CreateMap<Category, CategoryDTO>()
                .ForMember(dest => dest.Subcategories, opt => opt.MapFrom(src => src.Subcategories))
                .ForMember(dest => dest.Lots, opt => opt.MapFrom(src => src.Lots));
            CreateMap<CategoryDTO, Category>();
            CreateMap<CreateCategoryDTO, Category>();
        }
    }
}
