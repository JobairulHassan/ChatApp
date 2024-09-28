namespace ChatApp.Business.DataTransferObject.MessageDTO
{
    public class PrivateMessageResponseDTO
    {
        public int Id { get; set; }
        public string TextBody { get; set; }
        public DateTime CreationDate { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
    }
}
