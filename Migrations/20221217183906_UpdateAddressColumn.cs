using Microsoft.EntityFrameworkCore.Migrations;

namespace ecommerceApi.Migrations
{
    public partial class UpdateAddressColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Adress2",
                table: "UserAddress",
                newName: "Address2");

            migrationBuilder.RenameColumn(
                name: "Adress1",
                table: "UserAddress",
                newName: "Address1");

            migrationBuilder.RenameColumn(
                name: "ShippingAdress_Adress2",
                table: "Orders",
                newName: "ShippingAdress_Address2");

            migrationBuilder.RenameColumn(
                name: "ShippingAdress_Adress1",
                table: "Orders",
                newName: "ShippingAdress_Address1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "01fa79b2-fa40-46e6-851b-bf1b367e9f9a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "8276b082-fee6-411e-bdd9-b14d7a823745");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address2",
                table: "UserAddress",
                newName: "Adress2");

            migrationBuilder.RenameColumn(
                name: "Address1",
                table: "UserAddress",
                newName: "Adress1");

            migrationBuilder.RenameColumn(
                name: "ShippingAdress_Address2",
                table: "Orders",
                newName: "ShippingAdress_Adress2");

            migrationBuilder.RenameColumn(
                name: "ShippingAdress_Address1",
                table: "Orders",
                newName: "ShippingAdress_Adress1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "675aa3ce-d902-4006-b70f-a94a88833b99");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "45ed425b-19d3-48b3-99b5-6eb2abb12262");
        }
    }
}
