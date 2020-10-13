using ClientsManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientsManager.Data.EntityConfiguration
{
    public class EmployeeTypeConfiguration : IEntityTypeConfiguration<EmployeeType>
    {
        public void Configure(EntityTypeBuilder<EmployeeType> builder)
        {
            //Map EmployeeType entity to table EmployeeTypes
            builder.ToTable("EmployeeTypes");

            //Map Primary Key
            builder.HasKey(et => et.Id); //PK

            //Map Relationships
            builder
                .HasMany(et => et.Employees)
                .WithOne(e => e.EmployeeType) //Relationship with Employee
                .OnDelete(DeleteBehavior.Restrict); //do not delete Employees when deleting employee type

            //Map Properties
            builder.Property(et => et.Id)
                .IsRequired()
                .HasColumnType<int>("int");
            
            builder.Property(et => et.Description)
                .IsRequired()
                .HasColumnType<string>("nvarchar(max)");


            //Seed Data
            builder.HasData(
                new EmployeeType
                {
                    Id = 1,
                    Description = "Worker"
                },
                new EmployeeType
                {
                    Id = 2,
                    Description = "Manager"
                }
            );
        }
    }
}
