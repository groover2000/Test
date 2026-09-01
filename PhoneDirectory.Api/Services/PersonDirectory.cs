using PhoneDirectory.Api.Models;
using PhoneDirectory.Api.Data;
using Microsoft.EntityFrameworkCore;
using PhoneDirectory.Api.Exceptions;
using Npgsql;


namespace PhoneDirectory.Api.Services;

public class PersonDirectory
{

    private readonly AppDbContext db;

    public PersonDirectory(AppDbContext db)
    {

        this.db = db;

    }

    public async Task<PagedResultDto<PersonListDto>> GetPeople(
        PeopleQueryDto query
    )
    {
        IQueryable<Person> people = db.People
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.Name))
        {
            people = people.Where(p =>
                EF.Functions.ILike(
                    p.FullName,
                    $"{query.Name}%"
                ));
        }

        if (!string.IsNullOrWhiteSpace(query.Department))
        {
            people = people.Where(p =>
            p.Department == query.Department
            );
        }

        int totalCount = await people.CountAsync();

        var items = await people
            .OrderBy(p => p.FullName)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(p => new PersonListDto
            {
                Id = p.Id,
                FullName = p.FullName,
                Department = p.Department,
                Phone = p.Phone,
                Email = p.Email
            })
            .ToListAsync();

        int totalPages = (int)Math.Ceiling(
            (double)totalCount / query.PageSize
        );


        return new PagedResultDto<PersonListDto>
        {
            Items = items,
            Page = query.Page,
            PageSize = query.PageSize,
            TotalCount = totalCount,
            TotalPages = totalPages
        };
    }

    public async Task<Person> PersonAdd(
        string fullname,
        string department,
        string phone,
        string email,
        string position,
        int age
    )
    {
        Person person = new(
            fullname,
            department,
            phone,
            email,
            position,
            age
        );

        db.People.Add(person);

        try
        {
            await db.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
            when (ex.InnerException is PostgresException postgresException &&
                    postgresException.SqlState == PostgresErrorCodes.UniqueViolation)
        {
            throw new DuplicateEmailException(email);
        }

        return person;
    }





    public bool HasEmail(string email)
    {
        return db.People
            .Any(person => person.Email == email);
    }




    public async Task<PersonDetailsDto?> GetById(int id)
    {
        return await db.People
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new PersonDetailsDto
            {
                Id = p.Id,
                FullName = p.FullName,
                Phone = p.Phone,
                Email = p.Email,
                Position = p.Position,
                Department = p.Department
            })
            .FirstOrDefaultAsync();
    }
    public async Task<Person?> FindById(int id)
    {
        return await db.People.FindAsync(id);
    }



    public async Task<Person?> DeleteById(int id)
    {
        Person? person = await FindById(id);

        if (person is not null)
        {
            db.People.Remove(person);
            await db.SaveChangesAsync();
        }

        return person;
    }


    public async Task<Person?> Update(
        int id,
        string fullName,
        string department,
        string phone,
        string email,
        string position,
        int age
    )
    {

        Person? person = await FindById(id);


        if (person is null)
        {
            return null;
        }


        person.Update(
            fullName,
            department,
            phone,
            email,
            position,
            age
        );

       
        await db.SaveChangesAsync();

        return person;
    }

}