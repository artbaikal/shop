using Microsoft.EntityFrameworkCore.Migrations;

namespace DBAcess.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Employees_EmployeeId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Orders",
                newName: "EmployeeID");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_EmployeeId",
                table: "Orders",
                newName: "IX_Orders_EmployeeID");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeID",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Employees_EmployeeID",
                table: "Orders",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Employees_EmployeeID",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "EmployeeID",
                table: "Orders",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_EmployeeID",
                table: "Orders",
                newName: "IX_Orders_EmployeeId");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Employees_EmployeeId",
                table: "Orders",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
