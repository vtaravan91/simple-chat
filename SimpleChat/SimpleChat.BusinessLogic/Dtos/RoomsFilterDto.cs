namespace SimpleChat.BusinessLogic.Dtos
{
    public class RoomsFilterDto
    {
        public int? RoomId { get; set; }
        public bool? CheckUserInRoom { get; set; }
        public int? UserId { get; set; }
    }
}
