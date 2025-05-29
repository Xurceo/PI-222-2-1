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
            CreateMap<Bid, BidDTO>();
            CreateMap<BidDTO, Bid>();

            // Lot <-> LotDTO
            CreateMap<Lot, LotDTO>();
            CreateMap<LotDTO, Lot>();
            CreateMap<CreateLotDTO, Lot>();

            // Category <-> CategoryDTO
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();
            CreateMap<CreateCategoryDTO, Category>();
        }
    }
}
