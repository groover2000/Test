using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneDirectory.Api.Models;

namespace PhoneDirectory.Api.Data;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.FullName)
            .IsRequired();

        builder.Property(p => p.Department)
            .IsRequired();

        builder.Property(p => p.Phone)
            .IsRequired();

        builder.Property(p => p.Email)
            .IsRequired();
        
        builder.HasIndex(p => p.Email)
            .IsUnique();

        builder.Property(p => p.Position)
            .IsRequired();

        builder.Property(p => p.Age)
            .IsRequired();

        
    }

}