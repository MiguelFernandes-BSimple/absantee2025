using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_UserDataModel_UserId",
                table: "Collaborators");

            migrationBuilder.DropForeignKey(
                name: "FK_HolidayPeriodDataModel_HolidayPlans_HolidayPlanDataModelId",
                table: "HolidayPeriodDataModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDataModel",
                table: "UserDataModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HolidayPeriodDataModel",
                table: "HolidayPeriodDataModel");

            migrationBuilder.RenameTable(
                name: "UserDataModel",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "HolidayPeriodDataModel",
                newName: "HolidayPeriod");

            migrationBuilder.RenameIndex(
                name: "IX_HolidayPeriodDataModel_HolidayPlanDataModelId",
                table: "HolidayPeriod",
                newName: "IX_HolidayPeriod_HolidayPlanDataModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HolidayPeriod",
                table: "HolidayPeriod",
                column: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_User_UserId",
                table: "Collaborators");

            migrationBuilder.DropForeignKey(
                name: "FK_HolidayPeriod_HolidayPlans_HolidayPlanDataModelId",
                table: "HolidayPeriod");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HolidayPeriod",
                table: "HolidayPeriod");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "UserDataModel");

            migrationBuilder.RenameTable(
                name: "HolidayPeriod",
                newName: "HolidayPeriodDataModel");

            migrationBuilder.RenameIndex(
                name: "IX_HolidayPeriod_HolidayPlanDataModelId",
                table: "HolidayPeriodDataModel",
                newName: "IX_HolidayPeriodDataModel_HolidayPlanDataModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDataModel",
                table: "UserDataModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HolidayPeriodDataModel",
                table: "HolidayPeriodDataModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_UserDataModel_UserId",
                table: "Collaborators",
                column: "UserId",
                principalTable: "UserDataModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HolidayPeriodDataModel_HolidayPlans_HolidayPlanDataModelId",
                table: "HolidayPeriodDataModel",
                column: "HolidayPlanDataModelId",
                principalTable: "HolidayPlans",
                principalColumn: "Id");
        }
    }
}
