using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Address_HomeId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Address_OfficeId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteColor_Customers_CustomerId",
                table: "FavoriteColor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteColor",
                table: "FavoriteColor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.RenameTable(
                name: "FavoriteColor",
                newName: "FavoriteColors");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "Addresses");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteColor_CustomerId",
                table: "FavoriteColors",
                newName: "IX_FavoriteColors_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteColors",
                table: "FavoriteColors",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Addresses_HomeId",
                table: "Customers",
                column: "HomeId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Addresses_OfficeId",
                table: "Customers",
                column: "OfficeId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteColors_Customers_CustomerId",
                table: "FavoriteColors",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Addresses_HomeId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Addresses_OfficeId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteColors_Customers_CustomerId",
                table: "FavoriteColors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteColors",
                table: "FavoriteColors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "FavoriteColors",
                newName: "FavoriteColor");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "Address");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteColors_CustomerId",
                table: "FavoriteColor",
                newName: "IX_FavoriteColor_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteColor",
                table: "FavoriteColor",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Address_HomeId",
                table: "Customers",
                column: "HomeId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Address_OfficeId",
                table: "Customers",
                column: "OfficeId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteColor_Customers_CustomerId",
                table: "FavoriteColor",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
