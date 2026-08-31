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
            throw new ArgumentException("Age incorrect");
        }

        if (string.IsNullOrWhiteSpace(fullName))
        {
            throw new ArgumentException("Name incorrect");
        }
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("email incorrect");
        }
        if (string.IsNullOrWhiteSpace(phone))
        {
            throw new ArgumentException("Phone incorrect");
        }
    }
#endregion
}