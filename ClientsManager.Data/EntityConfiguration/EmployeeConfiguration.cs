using ClientsManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientsManager.Data.EntityConfiguration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            //Map Employee entity to table Employees
            builder.ToTable("Employees");

            //Map Primary Key
            builder.HasKey(e => e.Id); //PK

            //Map Relationships
            builder
                .HasMany<BillableActivity>(e => e.BillableActivities)
                .WithOne(ba => ba.Employee); //Relationship with BillableActivity
            
            builder
                .HasOne(e => e.EmployeeType)
                .WithMany(et => et.Employees) //Relationship with EmployeeType
                .HasForeignKey(e => e.EmployeeType_Id); //FK from EmployeeType

            //Map Properties
            builder.Property(e => e.Id)
                .IsRequired()
                .HasColumnType<int>("int");
            
            builder.Property(e => e.Name)
                .IsRequired()
                .HasColumnType<string>("nvarchar(max)");

            builder.Property(e => e.EmployeeType_Id)
                .IsRequired(false)
                .HasColumnType<int>("int");
            
            builder.Property(e => e.Position)
                .IsRequired(false)
                .HasColumnType<string>("nvarchar(max)");


            //Seed data
            builder.HasData(
                new Employee
                {
                    Id = 1,
                    Name = "John Smith",
                    EmployeeType_Id = 1,
                    Position = "Junior Lawyer"
                },
                new Employee
                {
                    Id = 2,
                    Name = "Jane Doe",
                    EmployeeType_Id = 1,
                    Position = "Junior Lawyer"
                },
                new Employee
                {
                    Id = 3,
                    Name = "Michael Jones",
                    EmployeeType_Id = 1,
                    Position = "Junior Lawyer"
                },
                new Employee
                {
                    Id = 4,
                    Name = "Eliza Deer",
                    EmployeeType_Id = 1,
                    Position = "Junior Lawyer"
                },
                new Employee
                {
                    Id = 5,
                    Name = "Peter Granger",
                    EmployeeType_Id = 2,
                    Position = "Partner"
                },
                new Employee
                {
                    Id = 6,
                    Name = "Mary Osterfitz",
                    EmployeeType_Id = 2,
                    Position = "Partner"
                }
            );
        }
    }
}
