using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Service.API.Migrations.Aerende
{
    public partial class chagedInvoices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    IssueDate = table.Column<DateTime>(nullable: false),
                    PatientJournalId = table.Column<Guid>(nullable: true),
                    TotalSum = table.Column<double>(nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_PatientJournalId",
                table: "Invoice",
                column: "PatientJournalId");
        }
    }
}
