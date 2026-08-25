using Microsoft.AspNetCore.Mvc;
using PhoneDirectory.Api.Models;
using PhoneDirectory.Api.Services;

namespace PhoneDirectory.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleController: ControllerBase
{
#region Main
    private readonly PersonDirectory directory;

    public PeopleController(PersonDirectory directory)
    {
        this.directory = directory;
    }
#endregion
#region Get
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        
        return Ok(await directory.GetSortedByName());
    }
#endregion
#region  GetById
    [HttpGet("{id}")]

    public async Task<IActionResult> GetById(int id)
    {
        Person? person = await directory.FindById(id);

        if (person == null)
        {
            return NotFound();
        }

        return Ok(person);
    }
#endregion    
#region Create
    [HttpPost]
    public async Task<IActionResult> Create(CreatePersonDto dto)
    {
        Person person = await directory.PersonAdd(
            dto.FullName,
            dto.Department,
            dto.Phone,
            dto.Email,
            dto.Position,
            dto.Age
        );

        return CreatedAtAction(
            nameof(GetById),
            new {id = person.Id},
            person
        );
    }
#endregion
#region Delete
    [HttpDelete("{id}")]

    public async Task<IActionResult> Delete(int id)
    {
        Person? person = await directory.DeleteById(id);

        if (person == null)
        {
            return NotFound();
        }

        return NoContent();
    }
#endregion
#region Search
    [HttpGet("search")]
    public async Task<IActionResult> Search(string? name, string? department)
    {
       return Ok(await directory.Search(name, department));
    }
#endregion
#region Update
    [HttpPut("{id}")]

    public async Task<IActionResult> Update(int id, UpdatePersonDto dto)
    {
        Person? person = await directory.Update(
            id,
            dto.FullName,
            dto.Department,
            dto.Phone,
            dto.Email,
            dto.Position,
            dto.Age
        );

        if (person is null)
        {
            return NotFound();
        }

        return Ok(person);
    }
#endregion
}