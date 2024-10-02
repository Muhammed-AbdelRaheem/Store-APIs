using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateAddProductTabels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CareteAt",
                table: "Types",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "CareteAt",
                table: "Products",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "CareteAt",
                table: "Brands",
                newName: "CreateAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "Types",
                newName: "CareteAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "Products",
                newName: "CareteAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "Brands",
                newName: "CareteAt");
        }
    }
}
