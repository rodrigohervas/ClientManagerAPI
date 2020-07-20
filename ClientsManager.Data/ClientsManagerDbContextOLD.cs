using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ClientsManager.Models;

namespace ClientsManager.Data
{
    /// <summary>
    /// DbContext class for ClientsManagerDB access. It represents a Unit Of Work
    /// </summary>
    public class ClientsManagerDBContextOLD : DbContext
    {
        /// <summary>
        /// DBContext constructor
        /// </summary>
        /// <param name="options">DbContextOptions<ClientsManagerDBContext> - DbContext config data: sql provider, connection string, etc.</param>
        public ClientsManagerDBContextOLD(DbContextOptions<ClientsManagerDBContext> options) : base(options)
        {

        }

        //DbSets: each DbSet represents a repository
        public DbSet<TimeFrame> TimeFrames { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }

        /// <summary>
        /// Method that configures the Entity using Fluent API, and seeds the DB with initial data
        /// </summary>
        /// <param name="modelBuilder">Model Builder to configure the seeding data</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TimeFrame Entity configuration
            
            //Keys and relationships
            modelBuilder.Entity<TimeFrame>()
                .HasKey(tf => tf.Id); //PK
            modelBuilder.Entity<TimeFrame>()
                .HasOne(tf => tf.Employee)
                .WithMany(tf => tf.TimeFrames) //Relationship with Employee
                .HasForeignKey(tf => tf.Employee_Id); //FK from Employee
            
            //properties
            modelBuilder.Entity<TimeFrame>().Property(tf => tf.Id)
                .IsRequired()
                .HasColumnType<int>("int");
            modelBuilder.Entity<TimeFrame>().Property(tf => tf.Employee_Id)
                .IsRequired()
                .HasColumnType<int>("int");
            modelBuilder.Entity<TimeFrame>().Property(tf => tf.Title)
                .IsRequired()
                .HasColumnType<string>("nvarchar(max)");
            modelBuilder.Entity<TimeFrame>().Property(tf => tf.Description)
                .IsRequired()
                .HasColumnType<string>("nvarchar(max)");
            modelBuilder.Entity<TimeFrame>().Property(tf => tf.Price)
                .IsRequired()
                .HasColumnType<decimal>("decimal(18,4)");
            modelBuilder.Entity<TimeFrame>().Property(tf => tf.Start_DateTime)
                .IsRequired()
                .HasColumnType<DateTime>("DATETIME2 (7)");
            modelBuilder.Entity<TimeFrame>().Property(tf => tf.Start_DateTime)
                .IsRequired()
                .HasColumnType<DateTime>("DATETIME2 (7)");


            //Employee Entity configuration

            //Keys and relationships
            modelBuilder.Entity<Employee>()
                .HasKey(e => e.Id); //PK
            modelBuilder.Entity<Employee>()
                .HasMany<TimeFrame>(e => e.TimeFrames)
                .WithOne(tf => tf.Employee); //Relationship with TimeFrame
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.EmployeeType)
                .WithMany(et => et.Employees) //Relationship with EmployeeType
                .HasForeignKey(e => e.EmployeeType_Id); //FK from EmployeeType

            //properties
            modelBuilder.Entity<Employee>().Property(e => e.Id)
                .IsRequired()
                .HasColumnType<int>("int");
            modelBuilder.Entity<Employee>().Property(e => e.Name)
                .IsRequired()
                .HasColumnType<string>("nvarchar(max)");
            modelBuilder.Entity<Employee>().Property(e => e.EmployeeType_Id)
                .IsRequired()
                .HasColumnType<int>("int");
            modelBuilder.Entity<Employee>().Property(e => e.Position)
                .IsRequired()
                .HasColumnType<string>("nvarchar(max)");


            //EmployeeType Entity configuration

            //Keys and relationships
            modelBuilder.Entity<EmployeeType>()
                .HasKey(et => et.Id); //PK
            modelBuilder.Entity<EmployeeType>()
                .HasMany(et => et.Employees)
                .WithOne(e => e.EmployeeType); //Relationship with Employee

            //properties
            modelBuilder.Entity<EmployeeType>().Property(et => et.Id)
                .IsRequired()
                .HasColumnType<int>("int");
            modelBuilder.Entity<EmployeeType>().Property(et => et.Description)
                .IsRequired()
                .HasColumnType<string>("nvarchar(max)");


            //Seed DB Tables
            DbContextSeeder.SeedDB(modelBuilder);
        }
    }
}
