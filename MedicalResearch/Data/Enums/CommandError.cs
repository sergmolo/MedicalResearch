namespace MedicalResearch.Data.Enums
{
    public enum CommandError
    {
        DbError = 1,
        UserNotFound = 2,
        UserRemoved = 3,
        WrongEmailOrPassword = 4,
        WrongPassword = 5,
        NotFound = 10,
        PasswordExpired = 101,
        YouAreAdmin = 102
    }
}
