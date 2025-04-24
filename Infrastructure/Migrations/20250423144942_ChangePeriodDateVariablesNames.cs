using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangePeriodDateVariablesNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PeriodDate__initDate",
                table: "Projects",
                newName: "PeriodDate_InitDate");

            migrationBuilder.RenameColumn(
                name: "PeriodDate__finalDate",
                table: "Projects",
                newName: "PeriodDate_FinalDate");

            migrationBuilder.RenameColumn(
                name: "PeriodDate__initDate",
                table: "HolidayPeriod",
                newName: "PeriodDate_InitDate");

            migrationBuilder.RenameColumn(
                name: "PeriodDate__finalDate",
                table: "HolidayPeriod",
                newName: "PeriodDate_FinalDate");

            migrationBuilder.RenameColumn(
                name: "PeriodDate__initDate",
                table: "Associations",
                newName: "PeriodDate_InitDate");

            migrationBuilder.RenameColumn(
                name: "PeriodDate__finalDate",
                table: "Associations",
                newName: "PeriodDate_FinalDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PeriodDate_InitDate",
                table: "Projects",
                newName: "PeriodDate__initDate");

            migrationBuilder.RenameColumn(
                name: "PeriodDate_FinalDate",
                table: "Projects",
                newName: "PeriodDate__finalDate");

            migrationBuilder.RenameColumn(
                name: "PeriodDate_InitDate",
                table: "HolidayPeriod",
                newName: "PeriodDate__initDate");

            migrationBuilder.RenameColumn(
                name: "PeriodDate_FinalDate",
                table: "HolidayPeriod",
                newName: "PeriodDate__finalDate");

            migrationBuilder.RenameColumn(
                name: "PeriodDate_InitDate",
                table: "Associations",
                newName: "PeriodDate__initDate");

            migrationBuilder.RenameColumn(
                name: "PeriodDate_FinalDate",
                table: "Associations",
                newName: "PeriodDate__finalDate");
        }
    }
}
