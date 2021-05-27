namespace MedicalResearch.Business.Enums
{
    public enum CommandErrorCode
    {
        UserNotFound = 2,
        UserRemoved = 3,
        UserIsLockedOut = 4,
        WrongPassword = 5,
        NotFound = 10,
        PasswordExpired = 101
    }
}
