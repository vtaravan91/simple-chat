using AutoMapper;
using SimpleChat.BusinessLogic.Dtos;
using SimpleChat.DataAccess.Entities;

namespace SimpleChat.BusinessLogic.Configuration
{
    internal class TypeMapper : Profile
    {
        public TypeMapper()
        {
            CreateMap<UserDto, UserEntity>().ReverseMap();
            CreateMap<MessageDto, MessageEntity>().ReverseMap();
            CreateMap<RoomDto, RoomEntity>().ReverseMap();
        }
    }
}
