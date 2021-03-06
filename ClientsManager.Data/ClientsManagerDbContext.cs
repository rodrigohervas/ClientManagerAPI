﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ClientsManager.Models;
using ClientsManager.Data.EntityConfiguration;

namespace ClientsManager.Data
{
    /// <summary>
    /// DbContext class for ClientsManagerDB access. It represents a Unit Of Work
    /// </summary>
    public class ClientsManagerDbContext : DbContext
    {
        /// <summary>
        /// DBContext constructor
        /// </summary>
        /// <param name="options">DbContextOptions<ClientsManagerDBContext> - DbContext config data: sql provider, connection string, etc.</param>
        public ClientsManagerDbContext(DbContextOptions<ClientsManagerDbContext> options) : base(options)
        {

        }

        //DbSets
        public DbSet<BillableActivity> BillableActivities { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<LegalCase> LegalCases { get; set;  }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Contact> Contacts { get; set; }


        /// <summary>
        /// Method that configures the Entity using Fluent API, and seeds the DB with initial data
        /// </summary>
        /// <param name="modelBuilder">Model Builder to configure the seeding data</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Get the entity configuration files form the same assembly where our DbContext lives at
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientsManagerDBContext).Assembly);

            //BillableActivity Entity configuration
            modelBuilder.ApplyConfiguration<BillableActivity>(new BillableActivityConfiguration());

            //Employee Entity configuration
            modelBuilder.ApplyConfiguration<Employee>(new EmployeeConfiguration());

            //EmployeeType Entity configuration
            modelBuilder.ApplyConfiguration<EmployeeType>(new EmployeeTypeConfiguration());

            //Case entity configuration
            modelBuilder.ApplyConfiguration<LegalCase>(new LegalCaseConfiguration());

            //Client entity configuration
            modelBuilder.ApplyConfiguration<Client>(new ClientConfiguration());

            //Address entity configuration
            modelBuilder.ApplyConfiguration<Address>(new AddressConfiguration());

            //Contact entity configuration
            modelBuilder.ApplyConfiguration<Contact>(new ContactConfiguration());
        }
    }
}
