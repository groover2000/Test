using Microsoft.AspNetCore.Mvc;
using PhoneDirectory.Api.Models;
using PhoneDirectory.Api.Services;

namespace PhoneDirectory.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleController : ControllerBase
{

    private readonly PersonDirectory directory;

    public PeopleController(PersonDirectory directory)
    {
        this.directory = directory;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PeopleQueryDto query)
    {

        return Ok(await directory.GetPeople(query));
    }

    [HttpGet("{id}")]

    public async Task<IActionResult> GetById(int id)
    {
        Person? person = await directory.GetById(id);

        if (person == null)
        {
            return NotFound();
        }

        return Ok(person);
    }

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
            new { id = person.Id },
            person
        );
    }


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
}