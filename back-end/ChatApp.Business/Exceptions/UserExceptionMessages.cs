namespace ChatApp.Business.Exceptions
{
    public static class UserExceptionMessages
    {
        public const string NotFoundUserById = "no user with this id";

        public const string NotFoundUserByEmail = "no user with this email";

        public const string EmailAlreadyExsist = "this email is already exist";

        public const string IncorrectPassword = "password is not correct";

        public const string AlreadyHaveProfilePicture = "user already have profile picture";

        public const string DoNotHaveProfilePicture = "user don't have profile picture";
    }
}
