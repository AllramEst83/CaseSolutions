using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Service.API.Migrations
{
    public partial class chagedInvoices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PatientJournalId",
                table: "Invoices",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Adress",
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
                    table.PrimaryKey("PK_Adress", x => x.Id);
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
                name: "Clinic",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    AdressId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clinic_Adress_AdressId",
                        column: x => x.AdressId,
                        principalTable: "Adress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceCompany",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    AdressId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceCompany", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InsuranceCompany_Adress_AdressId",
                        column: x => x.AdressId,
                        principalTable: "Adress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KindOfIllness",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    IllnessSeverityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KindOfIllness", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KindOfIllness_IllnessSeverityWrapper_IllnessSeverityId",
                        column: x => x.IllnessSeverityId,
                        principalTable: "IllnessSeverityWrapper",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
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
                    table.PrimaryKey("PK_Doctor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctor_Clinic_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Doctor_TypeOfDoctorWrapper_TypeOfDoctorWrapperId",
                        column: x => x.TypeOfDoctorWrapperId,
                        principalTable: "TypeOfDoctorWrapper",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Insurance",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InsuranceCompanyId = table.Column<Guid>(nullable: true),
                    TypeOfInsuranceWrapperId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Insurance_InsuranceCompany_InsuranceCompanyId",
                        column: x => x.InsuranceCompanyId,
                        principalTable: "InsuranceCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Insurance_TypeOfInsuranceWrapper_TypeOfInsuranceWrapperId",
                        column: x => x.TypeOfInsuranceWrapperId,
                        principalTable: "TypeOfInsuranceWrapper",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientJournal",
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
                    table.PrimaryKey("PK_PatientJournal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientJournal_Clinic_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatientJournal_Insurance_InsuranceId",
                        column: x => x.InsuranceId,
                        principalTable: "Insurance",
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
                    KindOfIllnesId = table.Column<Guid>(nullable: true),
                    PatientJournalId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalService_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalService_KindOfIllness_KindOfIllnesId",
                        column: x => x.KindOfIllnesId,
                        principalTable: "KindOfIllness",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalService_PatientJournal_PatientJournalId",
                        column: x => x.PatientJournalId,
                        principalTable: "PatientJournal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalService_TypeOfExaminationWrapper_TypeOfExaminationWrapperId",
                        column: x => x.TypeOfExaminationWrapperId,
                        principalTable: "TypeOfExaminationWrapper",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Owner",
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
                    table.PrimaryKey("PK_Owner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Owner_Adress_AdressId",
                        column: x => x.AdressId,
                        principalTable: "Adress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Owner_PatientJournal_PatientJournalId",
                        column: x => x.PatientJournalId,
                        principalTable: "PatientJournal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prescription",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MedicalServiceId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescription_MedicalService_MedicalServiceId",
                        column: x => x.MedicalServiceId,
                        principalTable: "MedicalService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PatientJournalId",
                table: "Invoices",
                column: "PatientJournalId");

            migrationBuilder.CreateIndex(
                name: "IX_Clinic_AdressId",
                table: "Clinic",
                column: "AdressId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_ClinicId",
                table: "Doctor",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_TypeOfDoctorWrapperId",
                table: "Doctor",
                column: "TypeOfDoctorWrapperId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurance_InsuranceCompanyId",
                table: "Insurance",
                column: "InsuranceCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurance_TypeOfInsuranceWrapperId",
                table: "Insurance",
                column: "TypeOfInsuranceWrapperId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceCompany_AdressId",
                table: "InsuranceCompany",
                column: "AdressId");

            migrationBuilder.CreateIndex(
                name: "IX_KindOfIllness_IllnessSeverityId",
                table: "KindOfIllness",
                column: "IllnessSeverityId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalService_DoctorId",
                table: "MedicalService",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalService_KindOfIllnesId",
                table: "MedicalService",
                column: "KindOfIllnesId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalService_PatientJournalId",
                table: "MedicalService",
                column: "PatientJournalId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalService_TypeOfExaminationWrapperId",
                table: "MedicalService",
                column: "TypeOfExaminationWrapperId");

            migrationBuilder.CreateIndex(
                name: "IX_Owner_AdressId",
                table: "Owner",
                column: "AdressId");

            migrationBuilder.CreateIndex(
                name: "IX_Owner_PatientJournalId",
                table: "Owner",
                column: "PatientJournalId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientJournal_ClinicId",
                table: "PatientJournal",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientJournal_InsuranceId",
                table: "PatientJournal",
                column: "InsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_MedicalServiceId",
                table: "Prescription",
                column: "MedicalServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_PatientJournal_PatientJournalId",
                table: "Invoices",
                column: "PatientJournalId",
                principalTable: "PatientJournal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_PatientJournal_PatientJournalId",
                table: "Invoices");

            migrationBuilder.DropTable(
                name: "Owner");

            migrationBuilder.DropTable(
                name: "Prescription");

            migrationBuilder.DropTable(
                name: "MedicalService");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "KindOfIllness");

            migrationBuilder.DropTable(
                name: "PatientJournal");

            migrationBuilder.DropTable(
                name: "TypeOfExaminationWrapper");

            migrationBuilder.DropTable(
                name: "TypeOfDoctorWrapper");

            migrationBuilder.DropTable(
                name: "IllnessSeverityWrapper");

            migrationBuilder.DropTable(
                name: "Clinic");

            migrationBuilder.DropTable(
                name: "Insurance");

            migrationBuilder.DropTable(
                name: "InsuranceCompany");

            migrationBuilder.DropTable(
                name: "TypeOfInsuranceWrapper");

            migrationBuilder.DropTable(
                name: "Adress");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_PatientJournalId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "PatientJournalId",
                table: "Invoices");
        }
    }
}
