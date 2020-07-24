﻿// <auto-generated />
using System;
using ClientsManager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ClientsManager.Data.Migrations
{
    [DbContext(typeof(ClientsManagerDBContext))]
    partial class ClientsManagerDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ClientsManager.Models.BillableActivity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Employee_Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("Finish_DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("LegalCase_Id")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Start_DateTime")
                        .HasColumnType("DATETIME2 (7)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Employee_Id");

                    b.HasIndex("LegalCase_Id");

                    b.ToTable("BillableActivities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "this is the Billable Activity 1",
                            Employee_Id = 1,
                            Finish_DateTime = new DateTime(2020, 6, 20, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            LegalCase_Id = 1,
                            Price = 200.50m,
                            Start_DateTime = new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified),
                            Title = "Billable Activity 1"
                        },
                        new
                        {
                            Id = 2,
                            Description = "this is the Billable Activity 2",
                            Employee_Id = 1,
                            Finish_DateTime = new DateTime(2020, 6, 20, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            LegalCase_Id = 1,
                            Price = 200.50m,
                            Start_DateTime = new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified),
                            Title = "Billable Activity 2"
                        },
                        new
                        {
                            Id = 3,
                            Description = "this is the Billable Activity 3",
                            Employee_Id = 2,
                            Finish_DateTime = new DateTime(2020, 6, 20, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            LegalCase_Id = 1,
                            Price = 300.50m,
                            Start_DateTime = new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified),
                            Title = "Billable Activity 3"
                        },
                        new
                        {
                            Id = 4,
                            Description = "this is the Billable Activity 4",
                            Employee_Id = 2,
                            Finish_DateTime = new DateTime(2020, 6, 20, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            LegalCase_Id = 1,
                            Price = 100.50m,
                            Start_DateTime = new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified),
                            Title = "Billable Activity 4"
                        },
                        new
                        {
                            Id = 5,
                            Description = "this is the Billable Activity 5",
                            Employee_Id = 2,
                            Finish_DateTime = new DateTime(2020, 6, 20, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            LegalCase_Id = 2,
                            Price = 250.50m,
                            Start_DateTime = new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified),
                            Title = "Billable Activity 5"
                        },
                        new
                        {
                            Id = 6,
                            Description = "this is the Billable Activity 6",
                            Employee_Id = 3,
                            Finish_DateTime = new DateTime(2020, 6, 20, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            LegalCase_Id = 1,
                            Price = 450.50m,
                            Start_DateTime = new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified),
                            Title = "Billable Activity 6"
                        },
                        new
                        {
                            Id = 7,
                            Description = "this is the Billable Activity 7",
                            Employee_Id = 3,
                            Finish_DateTime = new DateTime(2020, 6, 20, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            LegalCase_Id = 1,
                            Price = 400.50m,
                            Start_DateTime = new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified),
                            Title = "Billable Activity 7"
                        },
                        new
                        {
                            Id = 8,
                            Description = "this is the Billable Activity 8",
                            Employee_Id = 4,
                            Finish_DateTime = new DateTime(2020, 6, 20, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            LegalCase_Id = 2,
                            Price = 350.50m,
                            Start_DateTime = new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified),
                            Title = "Billable Activity 8"
                        },
                        new
                        {
                            Id = 9,
                            Description = "this is the Billable Activity 9",
                            Employee_Id = 4,
                            Finish_DateTime = new DateTime(2020, 6, 20, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            LegalCase_Id = 2,
                            Price = 250.50m,
                            Start_DateTime = new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified),
                            Title = "Billable Activity 9"
                        },
                        new
                        {
                            Id = 10,
                            Description = "this is the Billable Activity 10",
                            Employee_Id = 5,
                            Finish_DateTime = new DateTime(2020, 6, 20, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            LegalCase_Id = 2,
                            Price = 500.50m,
                            Start_DateTime = new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified),
                            Title = "Billable Activity 10"
                        },
                        new
                        {
                            Id = 11,
                            Description = "this is the Billable Activity 11",
                            Employee_Id = 5,
                            Finish_DateTime = new DateTime(2020, 6, 20, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            LegalCase_Id = 1,
                            Price = 100.50m,
                            Start_DateTime = new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified),
                            Title = "Billable Activity 11"
                        },
                        new
                        {
                            Id = 12,
                            Description = "this is the Billable Activity 12",
                            Employee_Id = 6,
                            Finish_DateTime = new DateTime(2020, 6, 20, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            LegalCase_Id = 2,
                            Price = 150.50m,
                            Start_DateTime = new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified),
                            Title = "Billable Activity 12"
                        },
                        new
                        {
                            Id = 13,
                            Description = "this is the Billable Activity 13",
                            Employee_Id = 6,
                            Finish_DateTime = new DateTime(2020, 6, 20, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            LegalCase_Id = 2,
                            Price = 300.50m,
                            Start_DateTime = new DateTime(2020, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified),
                            Title = "Billable Activity 13"
                        });
                });

            modelBuilder.Entity("ClientsManager.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Mr. Jones State and family",
                            Name = "Mr. Jones State"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Big Company LLC",
                            Name = "Big Company LLC"
                        });
                });

            modelBuilder.Entity("ClientsManager.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EmployeeType_Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeType_Id");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EmployeeType_Id = 1,
                            Name = "John Smith",
                            Position = "Junior Lawyer"
                        },
                        new
                        {
                            Id = 2,
                            EmployeeType_Id = 1,
                            Name = "Jane Doe",
                            Position = "Junior Lawyer"
                        },
                        new
                        {
                            Id = 3,
                            EmployeeType_Id = 1,
                            Name = "Michael Jones",
                            Position = "Junior Lawyer"
                        },
                        new
                        {
                            Id = 4,
                            EmployeeType_Id = 1,
                            Name = "Eliza Deer",
                            Position = "Junior Lawyer"
                        },
                        new
                        {
                            Id = 5,
                            EmployeeType_Id = 2,
                            Name = "Peter Granger",
                            Position = "Partner"
                        },
                        new
                        {
                            Id = 6,
                            EmployeeType_Id = 2,
                            Name = "Mary Osterfitz",
                            Position = "Partner"
                        });
                });

            modelBuilder.Entity("ClientsManager.Models.EmployeeType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EmployeeTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Worker"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Manager"
                        });
                });

            modelBuilder.Entity("ClientsManager.Models.LegalCase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Client_Id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TrustFund")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("Client_Id");

                    b.ToTable("LegalCases");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Client_Id = 1,
                            Description = "Mr. Jones Will and State liquidation",
                            Title = "Mr. Jones Will",
                            TrustFund = 5000.00m
                        },
                        new
                        {
                            Id = 2,
                            Client_Id = 1,
                            Description = "Alpa Corp Real State Purchase Agreement to aquire Mr. Jones residential property in Houston TX",
                            Title = "Alpa Corp Real State Purchase Agreement",
                            TrustFund = 8000.00m
                        },
                        new
                        {
                            Id = 3,
                            Client_Id = 2,
                            Description = "Real State Commercial Lease from Santa Fe Realty",
                            Title = "Santa Fe Realty Commercial Lease",
                            TrustFund = 4500.00m
                        },
                        new
                        {
                            Id = 4,
                            Client_Id = 2,
                            Description = "Purchase Agreement for Tico Inc aquisition",
                            Title = "Tico Inc Purchase Agreement",
                            TrustFund = 3500.00m
                        });
                });

            modelBuilder.Entity("ClientsManager.Models.BillableActivity", b =>
                {
                    b.HasOne("ClientsManager.Models.Employee", "Employee")
                        .WithMany("BillableActivities")
                        .HasForeignKey("Employee_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClientsManager.Models.LegalCase", "LegalCase")
                        .WithMany("BillableActivities")
                        .HasForeignKey("LegalCase_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClientsManager.Models.Employee", b =>
                {
                    b.HasOne("ClientsManager.Models.EmployeeType", "EmployeeType")
                        .WithMany("Employees")
                        .HasForeignKey("EmployeeType_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClientsManager.Models.LegalCase", b =>
                {
                    b.HasOne("ClientsManager.Models.Client", "Client")
                        .WithMany("LegalCases")
                        .HasForeignKey("Client_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
