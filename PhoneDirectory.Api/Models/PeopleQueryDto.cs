using System.ComponentModel.DataAnnotations;

namespace PhoneDirectory.Api.Models;


public class PeopleQueryDto
{
    [Range(1, int.MaxValue)]
    public int Page {get;set;} = 1;

    [Range(1, 1000)]
    public int PageSize {get;set;} = 10;
    public string? Name {get;set;}
    public string? Department {get;set;}
    
}