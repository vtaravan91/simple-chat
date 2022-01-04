using AutoMapper;
using SimpleChat.API.Models;
using SimpleChat.API.Models.Requests;
using SimpleChat.BusinessLogic.Dtos;

namespace SimpleChat.API.Mapper
{
    internal class ModelMapper : Profile
    {
        public ModelMapper()
        {
            CreateMap<UserModel, UserDto>().ReverseMap();
            CreateMap<RoomModel, RoomDto>().ReverseMap();
            CreateMap<MessageModel, MessageDto>().ReverseMap();
            CreateMap<UserRoomModel, UserRoomDto>().ReverseMap();
            CreateMap<MessagesRequestModel, MessageFilterDto>();
            CreateMap<RoomsRequestModel, RoomsFilterDto>();
        }
    }
}
