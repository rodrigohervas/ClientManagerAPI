using ClientsManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientsManager.Data.EntityConfiguration
{
    public class BillableActivityConfiguration : IEntityTypeConfiguration<BillableActivity>
    {
        public void Configure(EntityTypeBuilder<BillableActivity> builder)
        {
            //Map BillableActivity entity to table BillableActivities
            builder.ToTable("BillableActivities");
            
            //Map Primary Key
            builder.HasKey(ba => ba.Id); //PK

            //Map Relationships
            builder
                .HasOne(ba => ba.Employee)
                .WithMany(ba => ba.BillableActivities) //Relationship with Employee
                .HasForeignKey(ba => ba.Employee_Id)
                .OnDelete(DeleteBehavior.Restrict); //FK from Employee

            //Map Properties
            builder.Property(ba => ba.Id)
                .IsRequired()
                .HasColumnType<int>("int");
            
            builder.Property(ba => ba.LegalCase_Id)
                .IsRequired()
                .HasColumnType<int>("int");
            
            builder.Property(ba => ba.Employee_Id)
                .IsRequired()
                .HasColumnType<int>("int");
            
            builder.Property(ba => ba.Title)
                .IsRequired()
                .HasColumnType<string>("nvarchar(max)");
            
            builder.Property(ba => ba.Description)
                .IsRequired(false)
                .HasColumnType<string>("nvarchar(max)");
            
            builder.Property(ba => ba.Price)
                .IsRequired()
                .HasColumnType<decimal>("decimal(18,2)")
                .HasDefaultValue<decimal>(0.0);
            
            builder.Property(ba => ba.Start_DateTime)
                .IsRequired()
                .HasColumnType<DateTime>("DATETIME2 (7)")
                .HasDefaultValue<DateTime>(DateTime.Now);
            
            builder.Property(ba => ba.Start_DateTime)
                .IsRequired()
                .HasColumnType<DateTime>("DATETIME2 (7)")
                .HasDefaultValue<DateTime>(DateTime.Now);

            //Seed Data
            builder.HasData(
                new BillableActivity
                {
                    Id = 1,
                    LegalCase_Id = 1, 
                    Employee_Id = 1,
                    Title = "Billable Activity 1",
                    Description = "this is the Billable Activity 1",
                    Price = 200.50m,
                    Start_DateTime = new DateTime(2020, 06, 20, 9, 30, 00),
                    Finish_DateTime = new DateTime(2020, 06, 20, 16, 00, 00)
                },
                new BillableActivity
                {
                    Id = 2,
                    LegalCase_Id = 1,
                    Employee_Id = 1,
                    Title = "Billable Activity 2",
                    Description = "this is the Billable Activity 2",
                    Price = 200.50m,
                    Start_DateTime = new DateTime(2020, 06, 20, 9, 30, 00),
                    Finish_DateTime = new DateTime(2020, 06, 20, 16, 00, 00)
                },
                new BillableActivity
                {
                    Id = 3,
                    LegalCase_Id = 1,
                    Employee_Id = 2,
                    Title = "Billable Activity 3",
                    Description = "this is the Billable Activity 3",
                    Price = 300.50m,
                    Start_DateTime = new DateTime(2020, 06, 20, 9, 30, 00),
                    Finish_DateTime = new DateTime(2020, 06, 20, 16, 00, 00)
                },
                new BillableActivity
                {
                    Id = 4,
                    LegalCase_Id = 1,
                    Employee_Id = 2,
                    Title = "Billable Activity 4",
                    Description = "this is the Billable Activity 4",
                    Price = 100.50m,
                    Start_DateTime = new DateTime(2020, 06, 20, 9, 30, 00),
                    Finish_DateTime = new DateTime(2020, 06, 20, 16, 00, 00)
                },
                new BillableActivity
                {
                    Id = 5,
                    LegalCase_Id = 2,
                    Employee_Id = 2,
                    Title = "Billable Activity 5",
                    Description = "this is the Billable Activity 5",
                    Price = 250.50m,
                    Start_DateTime = new DateTime(2020, 06, 20, 9, 30, 00),
                    Finish_DateTime = new DateTime(2020, 06, 20, 16, 00, 00)
                },
                new BillableActivity
                {
                    Id = 6,
                    LegalCase_Id = 1,
                    Employee_Id = 3,
                    Title = "Billable Activity 6",
                    Description = "this is the Billable Activity 6",
                    Price = 450.50m,
                    Start_DateTime = new DateTime(2020, 06, 20, 9, 30, 00),
                    Finish_DateTime = new DateTime(2020, 06, 20, 16, 00, 00)
                },
                new BillableActivity
                {
                    Id = 7,
                    LegalCase_Id = 1,
                    Employee_Id = 3,
                    Title = "Billable Activity 7",
                    Description = "this is the Billable Activity 7",
                    Price = 400.50m,
                    Start_DateTime = new DateTime(2020, 06, 20, 9, 30, 00),
                    Finish_DateTime = new DateTime(2020, 06, 20, 16, 00, 00)
                },
                new BillableActivity
                {
                    Id = 8,
                    LegalCase_Id = 2,
                    Employee_Id = 4,
                    Title = "Billable Activity 8",
                    Description = "this is the Billable Activity 8",
                    Price = 350.50m,
                    Start_DateTime = new DateTime(2020, 06, 20, 9, 30, 00),
                    Finish_DateTime = new DateTime(2020, 06, 20, 16, 00, 00)
                },
                new BillableActivity
                {
                    Id = 9,
                    LegalCase_Id = 2,
                    Employee_Id = 4,
                    Title = "Billable Activity 9",
                    Description = "this is the Billable Activity 9",
                    Price = 250.50m,
                    Start_DateTime = new DateTime(2020, 06, 20, 9, 30, 00),
                    Finish_DateTime = new DateTime(2020, 06, 20, 16, 00, 00)
                },
                new BillableActivity
                {
                    Id = 10,
                    LegalCase_Id = 2,
                    Employee_Id = 5,
                    Title = "Billable Activity 10",
                    Description = "this is the Billable Activity 10",
                    Price = 500.50m,
                    Start_DateTime = new DateTime(2020, 06, 20, 9, 30, 00),
                    Finish_DateTime = new DateTime(2020, 06, 20, 16, 00, 00)
                },
                new BillableActivity
                {
                    Id = 11,
                    LegalCase_Id = 1,
                    Employee_Id = 5,
                    Title = "Billable Activity 11",
                    Description = "this is the Billable Activity 11",
                    Price = 100.50m,
                    Start_DateTime = new DateTime(2020, 06, 20, 9, 30, 00),
                    Finish_DateTime = new DateTime(2020, 06, 20, 16, 00, 00)
                },
                new BillableActivity
                {
                    Id = 12,
                    LegalCase_Id = 2,
                    Employee_Id = 6,
                    Title = "Billable Activity 12",
                    Description = "this is the Billable Activity 12",
                    Price = 150.50m,
                    Start_DateTime = new DateTime(2020, 06, 20, 9, 30, 00),
                    Finish_DateTime = new DateTime(2020, 06, 20, 16, 00, 00)
                },
                new BillableActivity
                {
                    Id = 13,
                    LegalCase_Id = 2,
                    Employee_Id = 6,
                    Title = "Billable Activity 13",
                    Description = "this is the Billable Activity 13",
                    Price = 300.50m,
                    Start_DateTime = new DateTime(2020, 06, 20, 9, 30, 00),
                    Finish_DateTime = new DateTime(2020, 06, 20, 16, 00, 00)
                }
            );
        }
    }
}
