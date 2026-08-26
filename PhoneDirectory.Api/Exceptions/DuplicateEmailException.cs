namespace PhoneDirectory.Api.Exceptions;

public class DuplicateEmailException : Exception
{
    public DuplicateEmailException(string email)
        :base($"Сотрудник с email '{email}' уже существует")
    {
    
    }
}