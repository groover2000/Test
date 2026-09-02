using PhoneDirectory.Api.Exceptions;
namespace PhoneDirectory.Api.Models;

public class Person
{
#region Main
    public int Id { get; private set; }
    public string FullName { get; private set; }
    public string Department { get; private set; }
    public string Phone { get; private set; }
    public string Email { get; private set; }
    public string Position { get; private set; }
    public int Age { get; private set; }

    private Person()
    {
       FullName = null!;
       Department = null!;
       Phone = null!;
       Email = null!;
       Position = null!;
    }

    public Person(
        string fullName,
        string department,
        string phone,
        string email,
        string position,
        int age)
    {
        Validate(fullName, phone, email,age);

        FullName = fullName;
        Department = department;
        Phone = phone;
        Email = email;
        Position = position;
        Age = age;
    }
#endregion
#region Update
    public void Update(
       string fullName,
       string department,
       string phone,
       string email,
       string position,
       int age
   )
    {
       Validate(fullName, phone, email,age);

        FullName = fullName;
        Department = department;
        Phone = phone;
        Email = email;
        Position = position;
        Age = age;
    }
#endregion
#region Validate
    private static void Validate(
        string fullName,
        string phone,
        string email,
        int age
    )
    {
         if (age < 18)
        {
            throw new PersonValidationException("Age","Age field must be more then 18");
        }

        if (string.IsNullOrWhiteSpace(fullName))
        {
            throw new PersonValidationException("Fullname", "Name can not be empty");
        }
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new PersonValidationException("Email", "Email can not be empty");
        }
        if (string.IsNullOrWhiteSpace(phone))
        {
            throw new PersonValidationException("Phone", "Phone can not be empty");
        }
    }
#endregion
}