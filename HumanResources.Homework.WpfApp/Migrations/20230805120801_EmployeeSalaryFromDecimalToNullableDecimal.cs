using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanResources.Homework.WpfApp.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeSalaryFromDecimalToNullableDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Salary",
                table: "Employees",
                type: "money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "v5yAvVi7npxHRy0dS22MSWzH2R8b+KYsklVTUuw5b0KZiajWhq2IFCDxmRRFPQwd");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "Password",
                value: "UH+aIdqpFJthTzntI/4GAPF6VcJKgO/Rk/RLR/2qRgOYRyrKzaFFFWi8F0ByM1iL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Salary",
                table: "Employees",
                type: "money",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "money",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "mAExk5kVNnvvI6YniJAKZ7ZVVySgOomkifmCvcevUwecUhOT2oYUYrr+uUGZkMw+");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "Password",
                value: "deqQKYMB8lqAN1nO8F6ZnAZXt9oLCgeeUqnsOpuVasqkuONU7hnZk7zRM8YHgRPo");
        }
    }
}
