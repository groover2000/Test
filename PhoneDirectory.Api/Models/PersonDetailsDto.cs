namespace PhoneDirectory.Api.Models;

public class PersonDetailsDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Department { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Position { get; set; }
}