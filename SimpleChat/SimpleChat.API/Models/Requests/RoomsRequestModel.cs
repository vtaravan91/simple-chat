namespace SimpleChat.API.Models.Requests
{
    public class RoomsRequestModel
    {
        public int? RoomId { get; set; }
        public bool? CheckUserInRoom { get; set; }
        public int? UserId { get; set; }
    }
}
