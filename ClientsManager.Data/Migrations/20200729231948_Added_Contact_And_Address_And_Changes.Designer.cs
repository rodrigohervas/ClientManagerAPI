﻿// <auto-generated />
using System;
using ClientsManager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ClientsManager.Data.Migrations
{
    [DbContext(typeof(ClientsManagerDBContext))]
    [Migration("20200729231948_Added_Contact_And_Address_And_Changes")]
    partial class Added_Contact_And_Address_And_Changes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ClientsManager.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(350)");

                    b.Property<int>("Client_Id")
                        .HasColumnType("int");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(75)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(75)");

                    b.Property<string>("StreetNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Client_Id");

                    b.ToTable("Address");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "Silver Spring",
                            Client_Id = 1,
                            Country = "United States",
                            State = "Maryland",
                            StreetNumber = "53 6th Drive"
                        },
                        new
                        {
                            Id = 2,
                            City = "Saint Joseph",
                            Client_Id = 1,
                            Country = "United States",
                            State = "Missouri",
                            StreetNumber = "11392 Mayer Point"
                        },
                        new
                        {
                            Id = 3,
                            City = "Amarillo",
                            Client_Id = 2,
                            Country = "United States",
                            State = "Texas",
                            StreetNumber = "58 Esch Center"
                        },
                        new
                        {
                            Id = 4,
                            City = "Austin",
                            Client_Id = 2,
                            Country = "United States",
                            State = "Texas",
                            StreetNumber = "8 Veith Circle"
                        },
                        new
                        {
                            Id = 5,
                            City = "Evansville",
                            Client_Id = 2,
                            Country = "United States",
                            State = "Indiana",
                            StreetNumber = "819 Golden Leaf Terrace"
                        });
                });

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

                    b.Property<string>("Website")
                        .HasColumnType("nvarchar(350)");

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
                            Name = "Big Company LLC",
                            Website = "https://www.bigcompanyllc.com"
                        });
                });

            modelBuilder.Entity("ClientsManager.Models.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Address_Id")
                        .HasColumnType("int");

                    b.Property<string>("Cellphone")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)");

                    b.Property<int>("Client_Id")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(320)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.HasIndex("Address_Id");

                    b.HasIndex("Client_Id");

                    b.ToTable("Contacts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address_Id = 1,
                            Cellphone = "572-634-0767",
                            Client_Id = 1,
                            Email = "mcoslett0@dedecms.com",
                            Name = "Myca Coslett",
                            Position = "Legal Assistant",
                            Telephone = "792-438-4570"
                        },
                        new
                        {
                            Id = 2,
                            Address_Id = 5,
                            Cellphone = "866-685-4130",
                            Client_Id = 1,
                            Email = "ddioniso1@printfriendly.com",
                            Name = "Dewitt Dioniso",
                            Position = "Project Manager",
                            Telephone = "943-333-2330"
                        },
                        new
                        {
                            Id = 3,
                            Address_Id = 3,
                            Cellphone = "524-411-4320",
                            Client_Id = 2,
                            Email = "cfolli2@ask.com",
                            Name = "Carolan Folli",
                            Position = "Structural Analysis Engineer",
                            Telephone = "760-208-3333"
                        },
                        new
                        {
                            Id = 4,
                            Address_Id = 5,
                            Cellphone = "701-853-8605",
                            Client_Id = 2,
                            Email = "eprudence3@jalbum.net",
                            Name = "Emmy Prudence",
                            Position = "Help Desk Technician",
                            Telephone = "402-106-2823"
                        },
                        new
                        {
                            Id = 5,
                            Address_Id = 5,
                            Cellphone = "412-291-0241",
                            Client_Id = 1,
                            Email = "rsimms4@tripadvisor.com",
                            Name = "Riane Simms",
                            Position = "Biostatistician I",
                            Telephone = "929-979-6229"
                        },
                        new
                        {
                            Id = 6,
                            Address_Id = 3,
                            Cellphone = "819-964-7799",
                            Client_Id = 1,
                            Email = "ggainfort5@imgur.com",
                            Name = "Gabriele Gainfort",
                            Position = "Senior Editor",
                            Telephone = "640-126-2181"
                        },
                        new
                        {
                            Id = 7,
                            Address_Id = 5,
                            Cellphone = "547-991-5392",
                            Client_Id = 1,
                            Email = "jgrave6@hc360.com",
                            Name = "Jimmy Grave",
                            Position = "Financial Advisor",
                            Telephone = "170-122-8134"
                        },
                        new
                        {
                            Id = 8,
                            Address_Id = 1,
                            Cellphone = "448-516-4205",
                            Client_Id = 2,
                            Email = "pcrilly7@123-reg.co.uk",
                            Name = "Pavel Crilly",
                            Position = "Librarian",
                            Telephone = "492-130-7365"
                        },
                        new
                        {
                            Id = 9,
                            Address_Id = 5,
                            Cellphone = "947-673-3903",
                            Client_Id = 1,
                            Email = "ltallboy8@reddit.com",
                            Name = "Lilias Tallboy",
                            Position = "Technical Writer",
                            Telephone = "847-641-7975"
                        },
                        new
                        {
                            Id = 10,
                            Address_Id = 2,
                            Cellphone = "768-183-0836",
                            Client_Id = 2,
                            Email = "aakett9@de.vu",
                            Name = "Ardine Akett",
                            Position = "Food Chemist",
                            Telephone = "674-875-8519"
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

            modelBuilder.Entity("ClientsManager.Models.Address", b =>
                {
                    b.HasOne("ClientsManager.Models.Client", "Client")
                        .WithMany("Addresses")
                        .HasForeignKey("Client_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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

            modelBuilder.Entity("ClientsManager.Models.Contact", b =>
                {
                    b.HasOne("ClientsManager.Models.Address", "Address")
                        .WithMany("Contacts")
                        .HasForeignKey("Address_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClientsManager.Models.Client", "Client")
                        .WithMany("Contacts")
                        .HasForeignKey("Client_Id")
                        .OnDelete(DeleteBehavior.Restrict)
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
