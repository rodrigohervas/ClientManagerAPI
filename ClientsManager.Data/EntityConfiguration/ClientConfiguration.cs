using ClientsManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientsManager.Data.EntityConfiguration
{
    class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            //Map to table
            builder.ToTable("Clients");

            //Map Primary Key field
            builder.HasKey(c => c.Id);

            //Map Properties
            builder.Property(c => c.Id)
                .IsRequired()
                .HasColumnType<int>("int");

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType<string>("nvarchar(max)");
            
            builder.Property(c => c.Description)
                .IsRequired(false)
                .HasColumnType<string>("nvarchar(max)");

            builder.Property(c => c.Website)
                .IsRequired(false)
                .HasColumnType<string>("nvarchar(350)");

            //Seed Table
            builder.HasData(
                new Client() 
                { 
                    Id = 1,
                    Name = "Mr. Jones State", 
                    Description = "Mr. Jones State and family"
                },
                new Client()
                {
                    Id = 2,
                    Name = "Big Company LLC",
                    Description = "Big Company LLC",
                    Website = "https://www.bigcompanyllc.com"
                });
        }
    }
}
