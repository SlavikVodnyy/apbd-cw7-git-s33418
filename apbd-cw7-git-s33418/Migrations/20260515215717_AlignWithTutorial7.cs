using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apbd_cw7_git_s33418.Migrations
{
    /// <inheritdoc />
    public partial class AlignWithTutorial7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Product_Warranty_NonNegative",
                table: "PCs",
                sql: "[Warranty] >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Product_Weight_NonNegative",
                table: "PCs",
                sql: "[Weight] >= 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Product_Warranty_NonNegative",
                table: "PCs");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Product_Weight_NonNegative",
                table: "PCs");
        }
    }
}
