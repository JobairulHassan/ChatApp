using ChatApp.Persistence.Entities;

namespace ChatApp.Persistence.Retreve
{
    public class ChatWithLastMessage
    {
        public User User { get; set; }
        public PrivateMessage LastMessage { get; set; }
    }
}
