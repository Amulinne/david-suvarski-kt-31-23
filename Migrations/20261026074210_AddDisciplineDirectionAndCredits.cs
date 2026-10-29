using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace david_suvarski_kt_31_23.Migrations
{
    /// <inheritdoc />
    public partial class AddDisciplineDirectionAndCredits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "c_discipline_is_humanitarian",
                table: "cd_discipline",
                type: "bool",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "cd_credit",
                columns: table => new
                {
                    c_credit_id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_credit_is_passed = table.Column<bool>(type: "bool", nullable: false),
                    f_student_id = table.Column<int>(type: "int4", nullable: false),
                    f_discipline_id = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_credit_credit_id", x => x.c_credit_id);
                    table.ForeignKey(
                        name: "fk_credit_discipline_id",
                        column: x => x.f_discipline_id,
                        principalTable: "cd_discipline",
                        principalColumn: "c_discipline_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_credit_student_id",
                        column: x => x.f_student_id,
                        principalTable: "cd_student",
                        principalColumn: "c_student_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "idx_cd_credit_f_discipline_id",
                table: "cd_credit",
                column: "f_discipline_id");

            migrationBuilder.CreateIndex(
                name: "idx_cd_credit_f_student_id",
                table: "cd_credit",
                column: "f_student_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cd_credit");

            migrationBuilder.DropColumn(
                name: "c_discipline_is_humanitarian",
                table: "cd_discipline");
        }
    }
}
