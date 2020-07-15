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
    public class ClientsManagerDBContext : DbContext
    {
        /// <summary>
        /// DBContext constructor
        /// </summary>
        /// <param name="options">DbContextOptions<ClientsManagerDBContext> - DbContext config data: sql provider, connection string, etc.</param>
        public ClientsManagerDBContext(DbContextOptions<ClientsManagerDBContext> options) : base(options)
        {

        }

        //DbSets: each DbSet represents a repository
        public DbSet<TimeFrame> TimeFrames { get; set; }

        /// <summary>
        /// Method to seed the DB
        /// </summary>
        /// <param name="modelBuilder">Model Builder to configure the seeding data</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DbContextSeeder.SeedDB(modelBuilder);
        }
    }
}
