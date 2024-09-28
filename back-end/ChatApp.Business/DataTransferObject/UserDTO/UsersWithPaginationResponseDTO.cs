namespace ChatApp.Business.DataTransferObject.UserDTO
{
    public class UsersWithPaginationResponseDTO
    {
        public IEnumerable<UserResponseDTO> users { get; set; } = new List<UserResponseDTO>();
        public int numOfPages { get; set; }
        public int currentPage { get; set; }
    }
}
