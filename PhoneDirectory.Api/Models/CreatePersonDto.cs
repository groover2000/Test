using System.ComponentModel.DataAnnotations;

namespace PhoneDirectory.Api.Models;

public class CreatePersonDto
{
# region main

    [Required]
    public required string FullName { get; set;}
    [Required]
    public required string Department { get; set;}
    [Required]
    public required string Phone { get; set;}
    [Required]
    public required string Email { get; set;}
    [Required]
    public required string Position { get; set;}
    [Range(18, 150)]
    public int Age { get; set;}

#endregion
}