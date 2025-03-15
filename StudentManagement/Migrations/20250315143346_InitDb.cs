using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagement.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "class",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    start_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    due_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_class", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "slot",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    desc = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_slot", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "spending",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    electric = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    water = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    rent = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    another = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    paid_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    salary = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    eating = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spending", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "student",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    full_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    dob = table.Column<DateTime>(type: "datetime", nullable: false),
                    gender = table.Column<bool>(type: "bit", nullable: false),
                    parent_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    phone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    student_img = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "timetable",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    week_day = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    slot_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_timetable", x => x.id);
                    table.ForeignKey(
                        name: "time_table_slot_id_fk",
                        column: x => x.slot_id,
                        principalTable: "slot",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "attendance",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    slot_id = table.Column<int>(type: "int", nullable: false),
                    student_id = table.Column<int>(type: "int", nullable: false),
                    is_attendance = table.Column<bool>(type: "bit", nullable: false),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attendance", x => x.id);
                    table.ForeignKey(
                        name: "attendance_student_id_fk",
                        column: x => x.student_id,
                        principalTable: "student",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_attendance_slot",
                        column: x => x.slot_id,
                        principalTable: "slot",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "student_class",
                columns: table => new
                {
                    student_id = table.Column<int>(type: "int", nullable: false),
                    class_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    modified_at = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("student_class_pk", x => new { x.student_id, x.class_id });
                    table.ForeignKey(
                        name: "student_class___fk_class",
                        column: x => x.class_id,
                        principalTable: "class",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "student_class___fk_student",
                        column: x => x.student_id,
                        principalTable: "student",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tuition",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    student_id = table.Column<int>(type: "int", nullable: false),
                    paid_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    due_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    actual_amount = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tuition", x => x.id);
                    table.ForeignKey(
                        name: "FK_tuition_student",
                        column: x => x.student_id,
                        principalTable: "student",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "class_timetable",
                columns: table => new
                {
                    class_id = table.Column<int>(type: "int", nullable: false),
                    time_table_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    modified_at = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("class_time_table_pk", x => new { x.class_id, x.time_table_id });
                    table.ForeignKey(
                        name: "class_time_table___fk_class",
                        column: x => x.class_id,
                        principalTable: "class",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "class_time_table___fk_time",
                        column: x => x.time_table_id,
                        principalTable: "timetable",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "student_timetable",
                columns: table => new
                {
                    student_id = table.Column<int>(type: "int", nullable: false),
                    time_table_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    modified_at = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("student_time_table_pk", x => new { x.student_id, x.time_table_id });
                    table.ForeignKey(
                        name: "student_time_table___fk_student",
                        column: x => x.student_id,
                        principalTable: "student",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "student_time_table___fk_time",
                        column: x => x.time_table_id,
                        principalTable: "timetable",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_attendance_slot_id",
                table: "attendance",
                column: "slot_id");

            migrationBuilder.CreateIndex(
                name: "IX_attendance_student_id",
                table: "attendance",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_class_timetable_time_table_id",
                table: "class_timetable",
                column: "time_table_id");

            migrationBuilder.CreateIndex(
                name: "IX_student_class_class_id",
                table: "student_class",
                column: "class_id");

            migrationBuilder.CreateIndex(
                name: "IX_student_timetable_time_table_id",
                table: "student_timetable",
                column: "time_table_id");

            migrationBuilder.CreateIndex(
                name: "IX_timetable_slot_id",
                table: "timetable",
                column: "slot_id");

            migrationBuilder.CreateIndex(
                name: "IX_tuition_student_id",
                table: "tuition",
                column: "student_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attendance");

            migrationBuilder.DropTable(
                name: "class_timetable");

            migrationBuilder.DropTable(
                name: "spending");

            migrationBuilder.DropTable(
                name: "student_class");

            migrationBuilder.DropTable(
                name: "student_timetable");

            migrationBuilder.DropTable(
                name: "tuition");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "class");

            migrationBuilder.DropTable(
                name: "timetable");

            migrationBuilder.DropTable(
                name: "student");

            migrationBuilder.DropTable(
                name: "slot");
        }
    }
}
