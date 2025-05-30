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
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<CreateUserDTO, User>();
            CreateMap<User, UserShortDTO>();
            CreateMap<User, UserGetDTO>();

            // Bid <-> BidDTO
            CreateMap<Bid, BidDTO>();
            CreateMap<BidDTO, Bid>();
            CreateMap<Bid, BidShortDTO>();

            // Lot <-> LotDTO
            CreateMap<Lot, LotDTO>();
            CreateMap<LotDTO, Lot>();
            CreateMap<CreateLotDTO, Lot>();
            CreateMap<Lot, LotShortDTO>();

            // Category <-> CategoryDTO
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();
            CreateMap<CreateCategoryDTO, Category>();
            CreateMap<Category, CategoryShortDTO>();
        }
    }
}
