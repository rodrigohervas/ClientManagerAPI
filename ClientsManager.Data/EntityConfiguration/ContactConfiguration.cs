using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ClientsManager.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientsManager.Data.EntityConfiguration
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            //Map to table
            builder.ToTable("Contacts");

            //Map Primary Key field
            builder.HasKey(co => co.Id);

            //Map Relationships
            //one-to-many with Client
            builder
                .HasOne(co => co.Client)
                .WithMany(cl => cl.Contacts)
                .HasForeignKey(co => co.Client_Id) //FK from Client
                .OnDelete(DeleteBehavior.Restrict); //don't cascade delete to prevent conflicts with Client

            //One-to-many with Address
            builder
                .HasOne(co => co.Address)
                .WithMany(a => a.Contacts)
                .HasForeignKey(co => co.Address_Id) //FK from Address
                .OnDelete(DeleteBehavior.Restrict); //don't cascade delete to prevent conflicts with Address

            //Map properties
            builder.Property(co => co.Id)
                .IsRequired()
                .HasColumnType<int>("int");

            builder.Property(co => co.Client_Id)
                .IsRequired()
                .HasColumnType<int>("int");

            builder.Property(co => co.Address_Id)
                .IsRequired()
                .HasColumnType<int>("int");

            builder.Property(co => co.Name)
                .IsRequired()
                .HasColumnType<string>("nvarchar(150)");

            builder.Property(co => co.Position)
                .IsRequired(false)
                .HasColumnType<string>("nvarchar(150)");

            builder.Property(co => co.Telephone)
                .IsRequired()
                .HasColumnType<string>("nvarchar(15)");

            builder.Property(co => co.Cellphone)
                .IsRequired(false)
                .HasColumnType<string>("nvarchar(15)");

            builder.Property(co => co.Email)
                .IsRequired(false)
                .HasColumnType<string>("nvarchar(320)");

            //Seed Table
            builder.HasData(
                new Contact()
                {
                    Id = 1,
                    Client_Id = 1,
                    Address_Id = 1,
                    Name = "Myca Coslett",
                    Position = "Legal Assistant",
                    Telephone = "792-438-4570",
                    Cellphone = "572-634-0767",
                    Email = "mcoslett0@dedecms.com"
                },
                new Contact()
                {
                    Id = 2,
                    Client_Id = 1,
                    Address_Id = 5,
                    Name = "Dewitt Dioniso",
                    Position = "Project Manager",
                    Telephone = "943-333-2330",
                    Cellphone = "866-685-4130",
                    Email = "ddioniso1@printfriendly.com"
                },
                new Contact()
                {
                    Id = 3,
                    Client_Id = 2,
                    Address_Id = 3,
                    Name = "Carolan Folli",
                    Position = "Structural Analysis Engineer",
                    Telephone = "760-208-3333",
                    Cellphone = "524-411-4320",
                    Email = "cfolli2@ask.com"
                },
                new Contact()
                {
                    Id = 4,
                    Client_Id = 2,
                    Address_Id = 5,
                    Name = "Emmy Prudence",
                    Position = "Help Desk Technician",
                    Telephone = "402-106-2823",
                    Cellphone = "701-853-8605",
                    Email = "eprudence3@jalbum.net"
                },
                new Contact()
                {
                    Id = 5,
                    Client_Id = 1,
                    Address_Id = 5,
                    Name = "Riane Simms",
                    Position = "Biostatistician I",
                    Telephone = "929-979-6229",
                    Cellphone = "412-291-0241",
                    Email = "rsimms4@tripadvisor.com"
                },
                new Contact()
                {
                    Id = 6,
                    Client_Id = 1,
                    Address_Id = 3,
                    Name = "Gabriele Gainfort",
                    Position = "Senior Editor",
                    Telephone = "640-126-2181",
                    Cellphone = "819-964-7799",
                    Email = "ggainfort5@imgur.com"
                },
                new Contact()
                {
                    Id = 7,
                    Client_Id = 1,
                    Address_Id = 5,
                    Name = "Jimmy Grave",
                    Position = "Financial Advisor",
                    Telephone = "170-122-8134",
                    Cellphone = "547-991-5392",
                    Email = "jgrave6@hc360.com"
                },
                new Contact()
                {
                    Id = 8,
                    Client_Id = 2,
                    Address_Id = 1,
                    Name = "Pavel Crilly",
                    Position = "Librarian",
                    Telephone = "492-130-7365",
                    Cellphone = "448-516-4205",
                    Email = "pcrilly7@123-reg.co.uk"
                },
                new Contact()
                {
                    Id = 9,
                    Client_Id = 1,
                    Address_Id = 5,
                    Name = "Lilias Tallboy",
                    Position = "Technical Writer",
                    Telephone = "847-641-7975",
                    Cellphone = "947-673-3903",
                    Email = "ltallboy8@reddit.com"
                },
                new Contact()
                {
                    Id = 10,
                    Client_Id = 2,
                    Address_Id = 2,
                    Name = "Ardine Akett",
                    Position = "Food Chemist",
                    Telephone = "674-875-8519",
                    Cellphone = "768-183-0836",
                    Email = "aakett9@de.vu"
                });
        }
    }
}
