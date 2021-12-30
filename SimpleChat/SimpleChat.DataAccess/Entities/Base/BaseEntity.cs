using System;

namespace SimpleChat.DataAccess.Entities.Base
{
    public abstract class BaseEntity<T> where T: struct
    {
        public T Id { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
