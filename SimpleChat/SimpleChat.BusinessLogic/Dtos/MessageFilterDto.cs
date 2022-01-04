namespace SimpleChat.BusinessLogic.Dtos
{
    public class MessageFilterDto
    {
        public int Page { get; set; }
        public int Take { get; set; }
        public int RoomId { get; set; }
    }
}
