using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TraingModuleSubjectModuleCollaborators : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_User_UserId",
                table: "Collaborators");

            migrationBuilder.DropForeignKey(
                name: "FK_HolidayPeriod_HolidayPlans_HolidayPlanDataModelId",
                table: "HolidayPeriod");

            migrationBuilder.DropIndex(
                name: "IX_Collaborators_UserId",
                table: "Collaborators");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HolidayPlans",
                table: "HolidayPlans");

            migrationBuilder.RenameTable(
                name: "HolidayPlans",
                newName: "HolidayPlan");

            migrationBuilder.RenameColumn(
                name: "Period__initDate",
                table: "Associations",
                newName: "PeriodDate__initDate");

            migrationBuilder.RenameColumn(
                name: "Period__finalDate",
                table: "Associations",
                newName: "PeriodDate__finalDate");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HolidayPlan",
                table: "HolidayPlan",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TrainingModuleCollaboratorDataModels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TrainingModuleId = table.Column<long>(type: "bigint", nullable: false),
                    CollaboratorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingModuleCollaboratorDataModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingModules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TrainingSubjectId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingModules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingSubjects",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Subject = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingSubjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingModules_Periods",
                columns: table => new
                {
                    TrainingModuleDataModelId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    _initDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    _finalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingModules_Periods", x => new { x.TrainingModuleDataModelId, x.Id });
                    table.ForeignKey(
                        name: "FK_TrainingModules_Periods_TrainingModules_TrainingModuleDataM~",
                        column: x => x.TrainingModuleDataModelId,
                        principalTable: "TrainingModules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_HolidayPeriod_HolidayPlan_HolidayPlanDataModelId",
                table: "HolidayPeriod",
                column: "HolidayPlanDataModelId",
                principalTable: "HolidayPlan",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HolidayPeriod_HolidayPlan_HolidayPlanDataModelId",
                table: "HolidayPeriod");

            migrationBuilder.DropTable(
                name: "TrainingModuleCollaboratorDataModels");

            migrationBuilder.DropTable(
                name: "TrainingModules_Periods");

            migrationBuilder.DropTable(
                name: "TrainingSubjects");

            migrationBuilder.DropTable(
                name: "TrainingModules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HolidayPlan",
                table: "HolidayPlan");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "User");

            migrationBuilder.RenameTable(
                name: "HolidayPlan",
                newName: "HolidayPlans");

            migrationBuilder.RenameColumn(
                name: "PeriodDate__initDate",
                table: "Associations",
                newName: "Period__initDate");

            migrationBuilder.RenameColumn(
                name: "PeriodDate__finalDate",
                table: "Associations",
                newName: "Period__finalDate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HolidayPlans",
                table: "HolidayPlans",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_UserId",
                table: "Collaborators",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_User_UserId",
                table: "Collaborators",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HolidayPeriod_HolidayPlans_HolidayPlanDataModelId",
                table: "HolidayPeriod",
                column: "HolidayPlanDataModelId",
                principalTable: "HolidayPlans",
                principalColumn: "Id");
        }
    }
}
