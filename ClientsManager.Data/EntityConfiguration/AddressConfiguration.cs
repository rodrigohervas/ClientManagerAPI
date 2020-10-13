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
            builder.ToTable("Addresses");

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

            builder.Property(a => a.StateProvince)
                .IsRequired(false)
                .HasColumnType<string>("nvarchar(75)");

            builder.Property(a => a.ZipCode)
                .IsRequired(false)
                .HasColumnType<string>("nvarchar(15)");

            builder.Property(a => a.Country)
                .IsRequired(false)
                .HasColumnType<string>("nvarchar(75)");


            //Seed Table
            builder.HasData(
                new Address()
                {
                    Id = 1,
                    Client_Id = 1,
                    StreetNumber = "53 6th Drive",
                    City = "Silver Spring",
                    StateProvince = "Maryland",
                    ZipCode = "20811",
                    Country = "United States"
                },
                new Address()
                {
                    Id = 2,
                    Client_Id = 1,
                    StreetNumber = "11392 Mayer Point",
                    City = "Saint Joseph",
                    StateProvince = "Missouri",
                    ZipCode = "20812",
                    Country = "United States"
                },
                new Address()
                {
                    Id = 3,
                    Client_Id = 2,
                    StreetNumber = "58 Esch Center",
                    City = "Amarillo",
                    StateProvince = "Texas",
                    ZipCode = "20813",
                    Country = "United States"
                },
                new Address()
                {
                    Id = 4,
                    Client_Id = 2,
                    StreetNumber = "8 Veith Circle",
                    City = "Austin",
                    StateProvince = "Texas",
                    ZipCode = "20814",
                    Country = "United States"
                },
                new Address()
                {
                    Id = 5,
                    Client_Id = 2,
                    StreetNumber = "819 Golden Leaf Terrace",
                    City = "Evansville",
                    StateProvince = "Indiana",
                    ZipCode = "20815",
                    Country = "United States"
                }, 
                new Address()
                {
                    Id = 6,
                    Client_Id = 2,
                    StreetNumber = "Calle Lopez Masquez, 34",
                    City = "Madrid",
                    StateProvince = "Madrid",
                    ZipCode = "28032",
                    Country = "Spain"
                });

        }
    }
}
