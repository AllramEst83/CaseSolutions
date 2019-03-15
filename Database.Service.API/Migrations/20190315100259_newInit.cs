using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Service.API.Migrations
{
    public partial class newInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StreetAdress = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    Telephone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IllnessSeverityWrapper",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IllnessSeverity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IllnessSeverityWrapper", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfDoctorWrapper",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TypeOfDoctor = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfDoctorWrapper", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfExaminationWrapper",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TypeOfExamination = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfExaminationWrapper", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfInsuranceWrapper",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TypeOfInsurance = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfInsuranceWrapper", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clinics",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    AdressId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clinics_Adresses_AdressId",
                        column: x => x.AdressId,
                        principalTable: "Adresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceCompanys",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    AdressId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceCompanys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InsuranceCompanys_Adresses_AdressId",
                        column: x => x.AdressId,
                        principalTable: "Adresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KindOfIllnesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    IllnessSeverityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KindOfIllnesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KindOfIllnesses_IllnessSeverityWrapper_IllnessSeverityId",
                        column: x => x.IllnessSeverityId,
                        principalTable: "IllnessSeverityWrapper",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    TypeOfDoctorWrapperId = table.Column<int>(nullable: true),
                    ClinicId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Doctors_TypeOfDoctorWrapper_TypeOfDoctorWrapperId",
                        column: x => x.TypeOfDoctorWrapperId,
                        principalTable: "TypeOfDoctorWrapper",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Insurances",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InsuranceCompanyId = table.Column<Guid>(nullable: true),
                    TypeOfInsuranceWrapperId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Insurances_InsuranceCompanys_InsuranceCompanyId",
                        column: x => x.InsuranceCompanyId,
                        principalTable: "InsuranceCompanys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Insurances_TypeOfInsuranceWrapper_TypeOfInsuranceWrapperId",
                        column: x => x.TypeOfInsuranceWrapperId,
                        principalTable: "TypeOfInsuranceWrapper",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientJournals",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    AnimalSSN = table.Column<string>(nullable: true),
                    InsuranceId = table.Column<Guid>(nullable: true),
                    ClinicId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientJournals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientJournals_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatientJournals_Insurances_InsuranceId",
                        column: x => x.InsuranceId,
                        principalTable: "Insurances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TotalSum = table.Column<double>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    IssueDate = table.Column<DateTime>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    PatientJournalId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_PatientJournals_PatientJournalId",
                        column: x => x.PatientJournalId,
                        principalTable: "PatientJournals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicalServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TypeOfExaminationWrapperId = table.Column<int>(nullable: true),
                    DoctorId = table.Column<Guid>(nullable: true),
                    HourlyCost = table.Column<double>(nullable: false),
                    ExaminationDuration = table.Column<TimeSpan>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    KindOfIllnesId = table.Column<Guid>(nullable: true),
                    PatientJournalId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalServices_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalServices_KindOfIllnesses_KindOfIllnesId",
                        column: x => x.KindOfIllnesId,
                        principalTable: "KindOfIllnesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalServices_PatientJournals_PatientJournalId",
                        column: x => x.PatientJournalId,
                        principalTable: "PatientJournals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalServices_TypeOfExaminationWrapper_TypeOfExaminationWrapperId",
                        column: x => x.TypeOfExaminationWrapperId,
                        principalTable: "TypeOfExaminationWrapper",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    AdressId = table.Column<int>(nullable: true),
                    SSN = table.Column<long>(nullable: false),
                    PatientJournalId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Owners_Adresses_AdressId",
                        column: x => x.AdressId,
                        principalTable: "Adresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Owners_PatientJournals_PatientJournalId",
                        column: x => x.PatientJournalId,
                        principalTable: "PatientJournals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MedicalServiceId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescriptions_MedicalServices_MedicalServiceId",
                        column: x => x.MedicalServiceId,
                        principalTable: "MedicalServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clinics_AdressId",
                table: "Clinics",
                column: "AdressId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_ClinicId",
                table: "Doctors",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_TypeOfDoctorWrapperId",
                table: "Doctors",
                column: "TypeOfDoctorWrapperId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceCompanys_AdressId",
                table: "InsuranceCompanys",
                column: "AdressId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_InsuranceCompanyId",
                table: "Insurances",
                column: "InsuranceCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_TypeOfInsuranceWrapperId",
                table: "Insurances",
                column: "TypeOfInsuranceWrapperId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_PatientJournalId",
                table: "Invoice",
                column: "PatientJournalId");

            migrationBuilder.CreateIndex(
                name: "IX_KindOfIllnesses_IllnessSeverityId",
                table: "KindOfIllnesses",
                column: "IllnessSeverityId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalServices_DoctorId",
                table: "MedicalServices",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalServices_KindOfIllnesId",
                table: "MedicalServices",
                column: "KindOfIllnesId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalServices_PatientJournalId",
                table: "MedicalServices",
                column: "PatientJournalId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalServices_TypeOfExaminationWrapperId",
                table: "MedicalServices",
                column: "TypeOfExaminationWrapperId");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_AdressId",
                table: "Owners",
                column: "AdressId");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_PatientJournalId",
                table: "Owners",
                column: "PatientJournalId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientJournals_ClinicId",
                table: "PatientJournals",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientJournals_InsuranceId",
                table: "PatientJournals",
                column: "InsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_MedicalServiceId",
                table: "Prescriptions",
                column: "MedicalServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "MedicalServices");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "KindOfIllnesses");

            migrationBuilder.DropTable(
                name: "PatientJournals");

            migrationBuilder.DropTable(
                name: "TypeOfExaminationWrapper");

            migrationBuilder.DropTable(
                name: "TypeOfDoctorWrapper");

            migrationBuilder.DropTable(
                name: "IllnessSeverityWrapper");

            migrationBuilder.DropTable(
                name: "Clinics");

            migrationBuilder.DropTable(
                name: "Insurances");

            migrationBuilder.DropTable(
                name: "InsuranceCompanys");

            migrationBuilder.DropTable(
                name: "TypeOfInsuranceWrapper");

            migrationBuilder.DropTable(
                name: "Adresses");
        }
    }
}
