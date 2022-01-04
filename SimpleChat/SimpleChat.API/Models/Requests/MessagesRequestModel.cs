using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleChat.API.Models.Requests
{
    public class MessagesRequestModel
    {
        public int Page { get; set; }
        public int Take { get; set; }
        public int RoomId { get; set; }
    }
}
