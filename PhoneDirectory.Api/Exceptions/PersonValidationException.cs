namespace PhoneDirectory.Api.Exceptions;


public class PersonValidationException(string field,
    string message) : Exception(message)
{
    public string Field {get;} = field;
}