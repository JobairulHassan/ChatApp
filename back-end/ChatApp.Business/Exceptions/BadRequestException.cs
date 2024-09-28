namespace ChatApp.Business.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string massege) : base(massege) { }
    }
}
