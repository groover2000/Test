using PhoneDirectory.Api.Models;
using PhoneDirectory.Api.Data;
using Microsoft.EntityFrameworkCore;
using PhoneDirectory.Api.Exceptions;
using Npgsql;

namespace PhoneDirectory.Api.Services;

public class PersonDirectory
{
# region Main
    private readonly AppDbContext db;

    public PersonDirectory(AppDbContext db)
    {

        this.db = db;

    }
#endregion
# region PersonAdd
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
        catch(DbUpdateException ex)
            when (ex.InnerException is PostgresException postgresException &&
                    postgresException.SqlState == PostgresErrorCodes.UniqueViolation)
        {
            throw new DuplicateEmailException(email);
        }
        
        return person;
    }

# endregion
# region Add
    public void Add(Person person)
    {
        db.People.Add(person);
        db.SaveChanges();
    }

# endregion
# region Count
    public int Count()
    {
        return db.People.Count();
    }
# endregion
# region Search
public async Task<List<Person>> Search(string? name, string? department)
{
    IQueryable<Person> query = db.People;

    if (!string.IsNullOrWhiteSpace(name))
    {
        query = query.Where(p =>
           EF.Functions.ILike(p.FullName, $"%{name}"));
    }

    if (!string.IsNullOrWhiteSpace(department))
    {
        query = query.Where(p =>
            p.Department == department);
    }

    return await query
        .OrderBy(p => p.FullName)
        .ToListAsync();
}

# endregion
# region FindByName
    public List<Person> FindByName(string name)
    {
        return db.People
            .Where(person => person.FullName.Contains(
                name,
                StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

# endregion
# region FindByEmail
    public Person? FindByEmail(string email)
    {
        return db.People
            .FirstOrDefault(person => person.Email == email);
    }
# endregion
# region HasEmail
    public bool HasEmail(string email)
    {
        return db.People
            .Any(person => person.Email == email);
    }
# endregion
# region GetSortedByName
    public async Task<List<Person>> GetSortedByName()
    {
        return await db.People
            .OrderBy(person => person.FullName)
            .ToListAsync();
    }
# endregion
# region FindById
    public async Task<Person?> FindById(int id)
    {
        return await db.People
            .FirstOrDefaultAsync(person => person.Id == id);
    }

# endregion
# region DeletById
    public async  Task<Person?> DeleteById(int id)
    {
        Person? person = await FindById(id);

        if (person is not null)
        {
            db.People.Remove(person);
            await db.SaveChangesAsync();
        }

        return person;
    }
# endregion
# region Update
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
# endregion
}