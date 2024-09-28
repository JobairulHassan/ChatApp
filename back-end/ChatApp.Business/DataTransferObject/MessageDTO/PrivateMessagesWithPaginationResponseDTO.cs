namespace ChatApp.Business.DataTransferObject.MessageDTO
{
    public class PrivateMessagesWithPaginationResponseDTO
    {
        public IEnumerable<PrivateMessageResponseDTO> Messages { get; set; }
        public bool IsThereMore { get; set; }
    }
}
