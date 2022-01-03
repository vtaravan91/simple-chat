using AutoMapper;
using SimpleChat.API.Models;
using SimpleChat.BusinessLogic.Dtos;

namespace SimpleChat.API.Mapper
{
    internal class ModelMapper : Profile
    {
        public ModelMapper()
        {
            CreateMap<UserModel, UserDto>().ReverseMap();
        }
    }
}
