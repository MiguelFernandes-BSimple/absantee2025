using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Associations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Period__initDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Period__finalDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CollaboratorId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Associations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HolidayPlans",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CollaboratorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidayPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Acronym = table.Column<string>(type: "text", nullable: false),
                    PeriodDate__initDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PeriodDate__finalDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingPeriodDataModel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PeriodDate__initDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PeriodDate__finalDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPeriodDataModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserDataModel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Names = table.Column<string>(type: "text", nullable: false),
                    Surnames = table.Column<string>(type: "text", nullable: false),
                    PeriodDateTime__initDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PeriodDateTime__finalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDataModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HolidayPeriodDataModel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PeriodDate__initDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PeriodDate__finalDate = table.Column<DateOnly>(type: "date", nullable: false),
                    HolidayPlanDataModelId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidayPeriodDataModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HolidayPeriodDataModel_HolidayPlans_HolidayPlanDataModelId",
                        column: x => x.HolidayPlanDataModelId,
                        principalTable: "HolidayPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Collaborators",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    PeriodDateTime__initDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PeriodDateTime__finalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collaborators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Collaborators_UserDataModel_UserId",
                        column: x => x.UserId,
                        principalTable: "UserDataModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_UserId",
                table: "Collaborators",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HolidayPeriodDataModel_HolidayPlanDataModelId",
                table: "HolidayPeriodDataModel",
                column: "HolidayPlanDataModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Associations");

            migrationBuilder.DropTable(
                name: "Collaborators");

            migrationBuilder.DropTable(
                name: "HolidayPeriodDataModel");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "TrainingPeriodDataModel");

            migrationBuilder.DropTable(
                name: "UserDataModel");

            migrationBuilder.DropTable(
                name: "HolidayPlans");
        }
    }
}
