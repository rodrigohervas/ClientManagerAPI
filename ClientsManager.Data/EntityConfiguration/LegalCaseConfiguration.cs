using ClientsManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientsManager.Data.EntityConfiguration
{
    class LegalCaseConfiguration : IEntityTypeConfiguration<LegalCase>
    {

        //add one-to-many relationship with BillableActivities

        public void Configure(EntityTypeBuilder<LegalCase> builder)
        {
            //Map to table
            builder.ToTable("LegalCases");

            //Map Primary Key field
            builder.HasKey(lc => lc.Id);

            //Map Relationships
            //one-to-many with Client
            builder
                .HasOne(lc => lc.Client)
                .WithMany(cl => cl.LegalCases) //Relationship with Client
                .HasForeignKey(lc => lc.Client_Id); //FK from Client

            //one-to-many with LegalCase
            builder
                .HasMany(lc => lc.BillableActivities)
                .WithOne(ba => ba.LegalCase) //Relationship with BillableActivity
                .HasForeignKey(ba => ba.LegalCase_Id); //FK from LegalCase

            //Map Properties
            builder.Property(lc => lc.Id)
                .IsRequired()
                .HasColumnType<int>("int");
            
            builder.Property(lc => lc.Client_Id)
                .IsRequired()
                .HasColumnType<int>("int");

            builder.Property(lc => lc.Title)
                .IsRequired()
                .HasColumnType<string>("nvarchar(max)");
            
            builder.Property(lc => lc.Description)
                .IsRequired()
                .HasColumnType<string>("nvarchar(max)");
            
            builder.Property(lc => lc.TrustFund)
                .IsRequired()
                .HasColumnType<decimal>("decimal(18,2)");


            //Seed Table
            builder.HasData(
                new LegalCase() 
                { 
                    Id = 1,
                    Client_Id = 1, 
                    Title = "Mr. Jones Will", 
                    Description = "Mr. Jones Will and State liquidation", 
                    TrustFund = 5000.00m
                },
                new LegalCase()
                {
                    Id = 2,
                    Client_Id = 1,
                    Title = "Alpa Corp Real State Purchase Agreement",
                    Description = "Alpa Corp Real State Purchase Agreement to aquire Mr. Jones residential property in Houston TX",
                    TrustFund = 8000.00m
                },
                new LegalCase()
                {
                    Id = 3,
                    Client_Id = 2,
                    Title = "Santa Fe Realty Commercial Lease",
                    Description = "Real State Commercial Lease from Santa Fe Realty",
                    TrustFund = 4500.00m
                },
                new LegalCase()
                {
                    Id = 4,
                    Client_Id = 2,
                    Title = "Tico Inc Purchase Agreement",
                    Description = "Purchase Agreement for Tico Inc aquisition",
                    TrustFund = 3500.00m
                });
        }
    }
}
