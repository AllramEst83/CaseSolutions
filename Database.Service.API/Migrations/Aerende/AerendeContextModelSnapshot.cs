﻿// <auto-generated />
using System;
using Database.Service.API.Data.AerendeData.AerendeEntities.AerendeContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Database.Service.API.Migrations.Aerende
{
    [DbContext(typeof(AerendeContext))]
    partial class AerendeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Aerende.Service.API.Data.Adress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("StreetAdress");

                    b.Property<string>("Telephone");

                    b.Property<string>("ZipCode");

                    b.HasKey("Id");

                    b.ToTable("Adresss");
                });

            modelBuilder.Entity("Aerende.Service.API.Data.Clinic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AdressId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("AdressId");

                    b.ToTable("Clinics");
                });

            modelBuilder.Entity("Aerende.Service.API.Data.Doctor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ClinicId");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<int?>("TypeOfDoctorWrapperId");

                    b.HasKey("Id");

                    b.HasIndex("ClinicId");

                    b.HasIndex("TypeOfDoctorWrapperId");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("Aerende.Service.API.Data.Illness", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("IllnessSeverityWrapperId");

                    b.Property<string>("IllnessTitle");

                    b.Property<Guid?>("MedicalServiceId");

                    b.HasKey("Id");

                    b.HasIndex("IllnessSeverityWrapperId");

                    b.HasIndex("MedicalServiceId");

                    b.ToTable("Illnesses");
                });

            modelBuilder.Entity("Aerende.Service.API.Data.Insurance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("InsuranceCompanyId");

                    b.Property<int?>("TypeOfInsuranceWrapperId");

                    b.HasKey("Id");

                    b.HasIndex("InsuranceCompanyId");

                    b.HasIndex("TypeOfInsuranceWrapperId");

                    b.ToTable("Insurances");
                });

            modelBuilder.Entity("Aerende.Service.API.Data.InsuranceCompany", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AdressId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("AdressId");

                    b.ToTable("InsuranceCompanys");
                });

            modelBuilder.Entity("Aerende.Service.API.Data.MedicalService", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("DoctorId");

                    b.Property<DateTime>("EndTime");

                    b.Property<TimeSpan>("ExaminationDuration");

                    b.Property<double>("HourlyCost");

                    b.Property<Guid?>("PatientJournalId");

                    b.Property<DateTime>("StartTime");

                    b.Property<int?>("TypeOfExaminationWrapperId");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientJournalId");

                    b.HasIndex("TypeOfExaminationWrapperId");

                    b.ToTable("MedicalService");
                });

            modelBuilder.Entity("Aerende.Service.API.Data.Owner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AdressId");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<Guid?>("PatientJournalId");

                    b.Property<int>("SSN");

                    b.Property<string>("Telephone");

                    b.HasKey("Id");

                    b.HasIndex("AdressId");

                    b.HasIndex("PatientJournalId");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("Aerende.Service.API.Data.PatientJournal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AnimalSSN");

                    b.Property<Guid?>("ClinicId");

                    b.Property<string>("FirstName");

                    b.Property<Guid?>("InsuranceId");

                    b.Property<Guid>("InvoiceId");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.HasIndex("ClinicId");

                    b.HasIndex("InsuranceId");

                    b.ToTable("PatientJournals");
                });

            modelBuilder.Entity("Aerende.Service.API.Data.Prescription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<Guid?>("MedicalServiceId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("MedicalServiceId");

                    b.ToTable("Prescriptions");
                });

            modelBuilder.Entity("Database.Service.API.Data.AerendeData.AerendeEntities.Models.IllnessSeverityWrapper", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IllnessSeverity");

                    b.HasKey("Id");

                    b.ToTable("IllnessSeverityWrappers");
                });

            modelBuilder.Entity("Database.Service.API.Data.AerendeData.AerendeEntities.Models.TypeOfDoctorWrapper", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("TypeOfDoctor");

                    b.HasKey("Id");

                    b.ToTable("TypeOfDoctorWrappers");
                });

            modelBuilder.Entity("Database.Service.API.Data.AerendeData.AerendeEntities.Models.TypeOfExaminationWrapper", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("TypeOfExamination");

                    b.HasKey("Id");

                    b.ToTable("TypeOfExaminationWrappers");
                });

            modelBuilder.Entity("Database.Service.API.Data.AerendeData.AerendeEntities.Models.TypeOfInsuranceWrapper", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("TypeOfInsurance");

                    b.HasKey("Id");

                    b.ToTable("TypeOfInsuranceWrappers");
                });

            modelBuilder.Entity("Aerende.Service.API.Data.Clinic", b =>
                {
                    b.HasOne("Aerende.Service.API.Data.Adress", "Adress")
                        .WithMany()
                        .HasForeignKey("AdressId");
                });

            modelBuilder.Entity("Aerende.Service.API.Data.Doctor", b =>
                {
                    b.HasOne("Aerende.Service.API.Data.Clinic")
                        .WithMany("Doctors")
                        .HasForeignKey("ClinicId");

                    b.HasOne("Database.Service.API.Data.AerendeData.AerendeEntities.Models.TypeOfDoctorWrapper", "TypeOfDoctorWrapper")
                        .WithMany()
                        .HasForeignKey("TypeOfDoctorWrapperId");
                });

            modelBuilder.Entity("Aerende.Service.API.Data.Illness", b =>
                {
                    b.HasOne("Database.Service.API.Data.AerendeData.AerendeEntities.Models.IllnessSeverityWrapper", "IllnessSeverityWrapper")
                        .WithMany()
                        .HasForeignKey("IllnessSeverityWrapperId");

                    b.HasOne("Aerende.Service.API.Data.MedicalService")
                        .WithMany("Illnesses")
                        .HasForeignKey("MedicalServiceId");
                });

            modelBuilder.Entity("Aerende.Service.API.Data.Insurance", b =>
                {
                    b.HasOne("Aerende.Service.API.Data.InsuranceCompany", "InsuranceCompany")
                        .WithMany()
                        .HasForeignKey("InsuranceCompanyId");

                    b.HasOne("Database.Service.API.Data.AerendeData.AerendeEntities.Models.TypeOfInsuranceWrapper", "TypeOfInsuranceWrapper")
                        .WithMany()
                        .HasForeignKey("TypeOfInsuranceWrapperId");
                });

            modelBuilder.Entity("Aerende.Service.API.Data.InsuranceCompany", b =>
                {
                    b.HasOne("Aerende.Service.API.Data.Adress", "Adress")
                        .WithMany()
                        .HasForeignKey("AdressId");
                });

            modelBuilder.Entity("Aerende.Service.API.Data.MedicalService", b =>
                {
                    b.HasOne("Aerende.Service.API.Data.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId");

                    b.HasOne("Aerende.Service.API.Data.PatientJournal")
                        .WithMany("MedicalServices")
                        .HasForeignKey("PatientJournalId");

                    b.HasOne("Database.Service.API.Data.AerendeData.AerendeEntities.Models.TypeOfExaminationWrapper", "TypeOfExaminationWrapper")
                        .WithMany()
                        .HasForeignKey("TypeOfExaminationWrapperId");
                });

            modelBuilder.Entity("Aerende.Service.API.Data.Owner", b =>
                {
                    b.HasOne("Aerende.Service.API.Data.Adress", "Adress")
                        .WithMany()
                        .HasForeignKey("AdressId");

                    b.HasOne("Aerende.Service.API.Data.PatientJournal")
                        .WithMany("Owners")
                        .HasForeignKey("PatientJournalId");
                });

            modelBuilder.Entity("Aerende.Service.API.Data.PatientJournal", b =>
                {
                    b.HasOne("Aerende.Service.API.Data.Clinic", "Clinic")
                        .WithMany()
                        .HasForeignKey("ClinicId");

                    b.HasOne("Aerende.Service.API.Data.Insurance", "Insurance")
                        .WithMany()
                        .HasForeignKey("InsuranceId");
                });

            modelBuilder.Entity("Aerende.Service.API.Data.Prescription", b =>
                {
                    b.HasOne("Aerende.Service.API.Data.MedicalService")
                        .WithMany("Prescription")
                        .HasForeignKey("MedicalServiceId");
                });
#pragma warning restore 612, 618
        }
    }
}
