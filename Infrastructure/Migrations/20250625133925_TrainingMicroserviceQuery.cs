using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TrainingMicroserviceQuery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssociationsProjectCollaborator");

            migrationBuilder.DropTable(
                name: "HolidayPeriods");

            migrationBuilder.DropTable(
                name: "HRManagers");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "TrainingPeriods");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "HolidayPlans");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssociationsProjectCollaborator",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CollaboratorId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    PeriodDate_FinalDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PeriodDate_InitDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociationsProjectCollaborator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HolidayPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CollaboratorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidayPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HRManagers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    PeriodDateTime__finalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PeriodDateTime__initDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRManagers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Acronym = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    PeriodDate_FinalDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PeriodDate_InitDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingPeriods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PeriodDate_FinalDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PeriodDate_InitDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPeriods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Names = table.Column<string>(type: "text", nullable: false),
                    Surnames = table.Column<string>(type: "text", nullable: false),
                    PeriodDateTime__finalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PeriodDateTime__initDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HolidayPeriods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HolidayPlanDataModelId = table.Column<Guid>(type: "uuid", nullable: true),
                    PeriodDate_FinalDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PeriodDate_InitDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidayPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HolidayPeriods_HolidayPlans_HolidayPlanDataModelId",
                        column: x => x.HolidayPlanDataModelId,
                        principalTable: "HolidayPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HolidayPeriods_HolidayPlanDataModelId",
                table: "HolidayPeriods",
                column: "HolidayPlanDataModelId");
        }
    }
}
