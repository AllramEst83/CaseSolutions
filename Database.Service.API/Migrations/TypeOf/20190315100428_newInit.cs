using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Service.API.Migrations.TypeOf
{
    public partial class newInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IllnessSeverityWrappers");

            migrationBuilder.DropTable(
                name: "TypeOfDoctorWrappers");

            migrationBuilder.DropTable(
                name: "TypeOfExaminationWrappers");

            migrationBuilder.DropTable(
                name: "TypeOfInsuranceWrappers");
        }
    }
}
