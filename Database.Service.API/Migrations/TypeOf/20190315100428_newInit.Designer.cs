﻿// <auto-generated />
using Database.Service.API.Data.TypeOfData.TypeOfEntities.TypeOfContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Database.Service.API.Migrations.TypeOf
{
    [DbContext(typeof(TypeOfContext))]
    [Migration("20190315100428_newInit")]
    partial class newInit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ResponseModels.DatabaseModels.IllnessSeverityWrapper", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IllnessSeverity");

                    b.HasKey("Id");

                    b.ToTable("IllnessSeverityWrappers");
                });

            modelBuilder.Entity("ResponseModels.DatabaseModels.TypeOfDoctorWrapper", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("TypeOfDoctor");

                    b.HasKey("Id");

                    b.ToTable("TypeOfDoctorWrappers");
                });

            modelBuilder.Entity("ResponseModels.DatabaseModels.TypeOfExaminationWrapper", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("TypeOfExamination");

                    b.HasKey("Id");

                    b.ToTable("TypeOfExaminationWrappers");
                });

            modelBuilder.Entity("ResponseModels.DatabaseModels.TypeOfInsuranceWrapper", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("TypeOfInsurance");

                    b.HasKey("Id");

                    b.ToTable("TypeOfInsuranceWrappers");
                });
#pragma warning restore 612, 618
        }
    }
}
