using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Service.API.Migrations.Aerende
{
    public partial class addedAerendeDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adresss",
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
                    table.PrimaryKey("PK_Adresss", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IllnessSeverityWrappers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IllnessSeverity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IllnessSeverityWrappers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfDoctorWrappers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TypeOfDoctor = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfDoctorWrappers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfExaminationWrappers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TypeOfExamination = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfExaminationWrappers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfInsuranceWrappers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TypeOfInsurance = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfInsuranceWrappers", x => x.Id);
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
                        name: "FK_Clinics_Adresss_AdressId",
                        column: x => x.AdressId,
                        principalTable: "Adresss",
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
                        name: "FK_InsuranceCompanys_Adresss_AdressId",
                        column: x => x.AdressId,
                        principalTable: "Adresss",
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
                        name: "FK_Doctors_TypeOfDoctorWrappers_TypeOfDoctorWrapperId",
                        column: x => x.TypeOfDoctorWrapperId,
                        principalTable: "TypeOfDoctorWrappers",
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
                        name: "FK_Insurances_TypeOfInsuranceWrappers_TypeOfInsuranceWrapperId",
                        column: x => x.TypeOfInsuranceWrapperId,
                        principalTable: "TypeOfInsuranceWrappers",
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
                    AnimalSSN = table.Column<Guid>(nullable: false),
                    InsuranceId = table.Column<Guid>(nullable: true),
                    ClinicId = table.Column<Guid>(nullable: true),
                    InvoiceId = table.Column<Guid>(nullable: false)
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
                name: "MedicalService",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TypeOfExaminationWrapperId = table.Column<int>(nullable: true),
                    DoctorId = table.Column<Guid>(nullable: true),
                    HourlyCost = table.Column<double>(nullable: false),
                    ExaminationDuration = table.Column<TimeSpan>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    PatientJournalId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalService_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalService_PatientJournals_PatientJournalId",
                        column: x => x.PatientJournalId,
                        principalTable: "PatientJournals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalService_TypeOfExaminationWrappers_TypeOfExaminationWrapperId",
                        column: x => x.TypeOfExaminationWrapperId,
                        principalTable: "TypeOfExaminationWrappers",
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
                    Telephone = table.Column<string>(nullable: true),
                    AdressId = table.Column<int>(nullable: true),
                    SSN = table.Column<int>(nullable: false),
                    PatientJournalId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Owners_Adresss_AdressId",
                        column: x => x.AdressId,
                        principalTable: "Adresss",
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
                name: "Illnesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IllnessTitle = table.Column<string>(nullable: true),
                    IllnessSeverityWrapperId = table.Column<int>(nullable: true),
                    MedicalServiceId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Illnesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Illnesses_IllnessSeverityWrappers_IllnessSeverityWrapperId",
                        column: x => x.IllnessSeverityWrapperId,
                        principalTable: "IllnessSeverityWrappers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Illnesses_MedicalService_MedicalServiceId",
                        column: x => x.MedicalServiceId,
                        principalTable: "MedicalService",
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
                        name: "FK_Prescriptions_MedicalService_MedicalServiceId",
                        column: x => x.MedicalServiceId,
                        principalTable: "MedicalService",
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
                name: "IX_Illnesses_IllnessSeverityWrapperId",
                table: "Illnesses",
                column: "IllnessSeverityWrapperId");

            migrationBuilder.CreateIndex(
                name: "IX_Illnesses_MedicalServiceId",
                table: "Illnesses",
                column: "MedicalServiceId");

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
                name: "IX_MedicalService_DoctorId",
                table: "MedicalService",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalService_PatientJournalId",
                table: "MedicalService",
                column: "PatientJournalId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalService_TypeOfExaminationWrapperId",
                table: "MedicalService",
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
                name: "Illnesses");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "IllnessSeverityWrappers");

            migrationBuilder.DropTable(
                name: "MedicalService");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "PatientJournals");

            migrationBuilder.DropTable(
                name: "TypeOfExaminationWrappers");

            migrationBuilder.DropTable(
                name: "TypeOfDoctorWrappers");

            migrationBuilder.DropTable(
                name: "Clinics");

            migrationBuilder.DropTable(
                name: "Insurances");

            migrationBuilder.DropTable(
                name: "InsuranceCompanys");

            migrationBuilder.DropTable(
                name: "TypeOfInsuranceWrappers");

            migrationBuilder.DropTable(
                name: "Adresss");
        }
    }
}
