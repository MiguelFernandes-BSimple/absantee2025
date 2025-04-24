using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class trainingPeriods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HolidayPeriod_HolidayPlan_HolidayPlanDataModelId",
                table: "HolidayPeriod");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainingModuleCollaborator",
                table: "TrainingModuleCollaborator");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HolidayPlan",
                table: "HolidayPlan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HolidayPeriod",
                table: "HolidayPeriod");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Associations",
                table: "Associations");

            migrationBuilder.RenameTable(
                name: "TrainingModuleCollaborator",
                newName: "AssociationTrainingModuleCollaborators");

            migrationBuilder.RenameTable(
                name: "HolidayPlan",
                newName: "HolidayPlans");

            migrationBuilder.RenameTable(
                name: "HolidayPeriod",
                newName: "HolidayPeriods");

            migrationBuilder.RenameTable(
                name: "Associations",
                newName: "AssociationsProjectCollaborator");

            migrationBuilder.RenameIndex(
                name: "IX_HolidayPeriod_HolidayPlanDataModelId",
                table: "HolidayPeriods",
                newName: "IX_HolidayPeriods_HolidayPlanDataModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssociationTrainingModuleCollaborators",
                table: "AssociationTrainingModuleCollaborators",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HolidayPlans",
                table: "HolidayPlans",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HolidayPeriods",
                table: "HolidayPeriods",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssociationsProjectCollaborator",
                table: "AssociationsProjectCollaborator",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TrainingPeriods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PeriodDate_InitDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PeriodDate_FinalDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPeriods", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_HolidayPeriods_HolidayPlans_HolidayPlanDataModelId",
                table: "HolidayPeriods",
                column: "HolidayPlanDataModelId",
                principalTable: "HolidayPlans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HolidayPeriods_HolidayPlans_HolidayPlanDataModelId",
                table: "HolidayPeriods");

            migrationBuilder.DropTable(
                name: "TrainingPeriods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HolidayPlans",
                table: "HolidayPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HolidayPeriods",
                table: "HolidayPeriods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssociationTrainingModuleCollaborators",
                table: "AssociationTrainingModuleCollaborators");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssociationsProjectCollaborator",
                table: "AssociationsProjectCollaborator");

            migrationBuilder.RenameTable(
                name: "HolidayPlans",
                newName: "HolidayPlan");

            migrationBuilder.RenameTable(
                name: "HolidayPeriods",
                newName: "HolidayPeriod");

            migrationBuilder.RenameTable(
                name: "AssociationTrainingModuleCollaborators",
                newName: "TrainingModuleCollaborator");

            migrationBuilder.RenameTable(
                name: "AssociationsProjectCollaborator",
                newName: "Associations");

            migrationBuilder.RenameIndex(
                name: "IX_HolidayPeriods_HolidayPlanDataModelId",
                table: "HolidayPeriod",
                newName: "IX_HolidayPeriod_HolidayPlanDataModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HolidayPlan",
                table: "HolidayPlan",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HolidayPeriod",
                table: "HolidayPeriod",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainingModuleCollaborator",
                table: "TrainingModuleCollaborator",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Associations",
                table: "Associations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HolidayPeriod_HolidayPlan_HolidayPlanDataModelId",
                table: "HolidayPeriod",
                column: "HolidayPlanDataModelId",
                principalTable: "HolidayPlan",
                principalColumn: "Id");
        }
    }
}
