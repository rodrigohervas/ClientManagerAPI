using ClientsManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientsManager.Data.EntityConfiguration
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            //Map to table
            builder.ToTable("Address");

            //Map Primary Key field
            builder.HasKey(a => a.Id);

            //Map Relationships
            //one-to-many with Client
            builder
                .HasOne(a => a.Client)
                .WithMany(cl => cl.Addresses)
                .HasForeignKey(a => a.Client_Id); //Relationship with Client

            
            //Map Properties
            builder.Property(a => a.Id)
                .IsRequired()
                .HasColumnType<int>("int");

            builder.Property(a => a.Client_Id)
                .IsRequired()
                .HasColumnType<int>("int");

            builder.Property(a => a.StreetNumber)
                .IsRequired()
                .HasColumnType<string>("nvarchar(max)");

            builder.Property(a => a.City)
                .IsRequired()
                .HasColumnType<string>("nvarchar(350)");

            //string State
            builder.Property(a => a.State)
                .IsRequired()
                .HasColumnType<string>("nvarchar(75)");

            builder.Property(a => a.Country)
                .IsRequired()
                .HasColumnType<string>("nvarchar(75)");


            //Seed Table
            builder.HasData(
                new Address()
                {
                    Id = 1,
                    Client_Id = 1,
                    StreetNumber = "53 6th Drive",
                    City = "Silver Spring",
                    State = "Maryland",
                    Country = "United States"
                },
                new Address()
                {
                    Id = 2,
                    Client_Id = 1,
                    StreetNumber = "11392 Mayer Point",
                    City = "Saint Joseph",
                    State = "Missouri",
                    Country = "United States"
                },
                new Address()
                {
                    Id = 3,
                    Client_Id = 2,
                    StreetNumber = "58 Esch Center",
                    City = "Amarillo",
                    State = "Texas",
                    Country = "United States"
                },
                new Address()
                {
                    Id = 4,
                    Client_Id = 2,
                    StreetNumber = "8 Veith Circle",
                    City = "Austin",
                    State = "Texas",
                    Country = "United States"
                },
                new Address()
                {
                    Id = 5,
                    Client_Id = 2,
                    StreetNumber = "819 Golden Leaf Terrace",
                    City = "Evansville",
                    State = "Indiana",
                    Country = "United States"
                });

        }
    }
}
