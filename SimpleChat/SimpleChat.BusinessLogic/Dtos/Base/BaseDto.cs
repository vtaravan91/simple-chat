namespace SimpleChat.BusinessLogic.Dtos.Base
{
    public class BaseDto<TKey> where TKey : struct
    {
        public TKey Id { get; set; }
    }
}
