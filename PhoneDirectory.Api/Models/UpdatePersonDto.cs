using System.ComponentModel.DataAnnotations;

namespace PhoneDirectory.Api.Models;


public class UpdatePersonDto
{
# region main
    [Required]
    public required string FullName { get; set; }

    [Required]
    public required string Department { get; set; }

    [Required]
    [StringLength(25, MinimumLength = 7)]
    public required string Phone { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    public required string Position { get; set; }

    [Range(18, 150)]
    public  int Age { get; set;}

# endregion
}