using ClientsManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientsManager.Data.Configuration
{
    public class TimeFrameConfiguration : IEntityTypeConfiguration<TimeFrame>
    {
        public void Configure(EntityTypeBuilder<TimeFrame> builder)
        {
            //Map TimeFrame entity to table TimeFrames
            builder.ToTable("TimeFrames");
            
            //Keys and relationships
            builder.HasKey(tf => tf.Id); //PK
            builder
                .HasOne(tf => tf.Employee)
                .WithMany(tf => tf.TimeFrames) //Relationship with Employee
                .HasForeignKey(tf => tf.Employee_Id); //FK from Employee

            //properties
            builder.Property(tf => tf.Id)
                .IsRequired()
                .HasColumnType<int>("int");
            builder.Property(tf => tf.Employee_Id)
                .IsRequired()
                .HasColumnType<int>("int");
            builder.Property(tf => tf.Title)
                .IsRequired()
                .HasColumnType<string>("nvarchar(max)");
            builder.Property(tf => tf.Description)
                .IsRequired()
                .HasColumnType<string>("nvarchar(max)");
            builder.Property(tf => tf.Price)
                .IsRequired()
                .HasColumnType<decimal>("decimal(18,4)");
            builder.Property(tf => tf.Start_DateTime)
                .IsRequired()
                .HasColumnType<DateTime>("DATETIME2 (7)");
            builder.Property(tf => tf.Start_DateTime)
                .IsRequired()
                .HasColumnType<DateTime>("DATETIME2 (7)");

            //Seed Data
            builder.HasData(
                new TimeFrame
                {
                    Id = 1,
                    Employee_Id = 1,
                    Title = "timeframe 1",
                    Description = "this is the timeframe 1",
                    Price = 120.50m,
                    Start_DateTime = new DateTime(2020, 06, 20, 9, 30, 00),
                    Finish_DateTime = new DateTime(2020, 06, 20, 16, 00, 00)
                },
                new TimeFrame
                {
                    Id = 2,
                    Employee_Id = 1,
                    Title = "timeframe 2",
                    Description = "this is the timeframe 2",
                    Price = 400m,
                    Start_DateTime = new DateTime(2020, 06, 21, 10, 00, 00),
                    Finish_DateTime = new DateTime(2020, 06, 21, 17, 30, 00)
                },
                new TimeFrame
                {
                    Id = 3,
                    Employee_Id = 2,
                    Title = "timeframe 3",
                    Description = "this is the timeframe 3",
                    Price = 300.50m,
                    Start_DateTime = new DateTime(2020, 06, 23, 8, 15, 00),
                    Finish_DateTime = new DateTime(2020, 06, 23, 13, 30, 00)
                },
                new TimeFrame
                {
                    Id = 4,
                    Employee_Id = 3,
                    Title = "timeframe 4",
                    Description = "this is the timeframe 4",
                    Price = 120.50m,
                    Start_DateTime = new DateTime(2020, 06, 20, 9, 30, 00),
                    Finish_DateTime = new DateTime(2020, 06, 20, 16, 00, 00)
                },
                new TimeFrame
                {
                    Id = 5,
                    Employee_Id = 3,
                    Title = "timeframe 5",
                    Description = "this is the timeframe 5",
                    Price = 400m,
                    Start_DateTime = new DateTime(2020, 06, 21, 10, 00, 00),
                    Finish_DateTime = new DateTime(2020, 06, 21, 17, 30, 00)
                },
                new TimeFrame
                {
                    Id = 6,
                    Employee_Id = 4,
                    Title = "timeframe 6",
                    Description = "this is the timeframe 6",
                    Price = 300.50m,
                    Start_DateTime = new DateTime(2020, 06, 23, 8, 15, 00),
                    Finish_DateTime = new DateTime(2020, 06, 23, 13, 30, 00)
                },
                new TimeFrame
                {
                    Id = 7,
                    Employee_Id = 5,
                    Title = "timeframe 7",
                    Description = "this is the timeframe 7",
                    Price = 300.50m,
                    Start_DateTime = new DateTime(2020, 06, 23, 8, 15, 00),
                    Finish_DateTime = new DateTime(2020, 06, 23, 13, 30, 00)
                },
                new TimeFrame
                {
                    Id = 8,
                    Employee_Id = 5,
                    Title = "timeframe 8",
                    Description = "this is the timeframe 8",
                    Price = 300.50m,
                    Start_DateTime = new DateTime(2020, 06, 23, 8, 15, 00),
                    Finish_DateTime = new DateTime(2020, 06, 23, 13, 30, 00)
                },
                new TimeFrame
                {
                    Id = 9,
                    Employee_Id = 6,
                    Title = "timeframe 9",
                    Description = "this is the timeframe 9",
                    Price = 300.50m,
                    Start_DateTime = new DateTime(2020, 06, 23, 8, 15, 00),
                    Finish_DateTime = new DateTime(2020, 06, 23, 13, 30, 00)
                }
            );
        }
    }
}
