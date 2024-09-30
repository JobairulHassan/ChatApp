namespace ChatApp.Persistence.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string About { get; set; } = string.Empty;
        public bool IsDarkTheme { get; set; } = false;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public ICollection<PrivateMessage> SendedPrivateMessages { get; set; }
        public ICollection<PrivateMessage> ReceivedPrivateMessages { get; set; }
    }
}