using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caminhoes.Api.Migrations
{
    /// <inheritdoc />
    public partial class UniqueCodigoChassi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Caminhoes_CodigoChassi",
                table: "Caminhoes",
                column: "CodigoChassi",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Caminhoes_CodigoChassi",
                table: "Caminhoes");
        }
    }
}
