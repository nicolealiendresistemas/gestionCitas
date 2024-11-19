using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gestionCitas.Migrations
{
    /// <inheritdoc />
    public partial class @as : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "especialidades",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    descripcion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__especial__3213E83F9A946669", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pacientes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    fecha_nacimiento = table.Column<DateOnly>(type: "date", nullable: true),
                    telefono = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    direccion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__paciente__3213E83F7624F51C", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__roles__3213E83F2B868415", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuario = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__usuarios__3213E83F015CE0A2", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "medicos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuario_id = table.Column<int>(type: "int", nullable: true),
                    nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    especialidad_id = table.Column<int>(type: "int", nullable: true),
                    telefono = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    horario_consulta_inicio = table.Column<TimeOnly>(type: "time", nullable: true),
                    horario_consulta_fin = table.Column<TimeOnly>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__medicos__3213E83F230CA830", x => x.id);
                    table.ForeignKey(
                        name: "FK__medicos__especia__59063A47",
                        column: x => x.especialidad_id,
                        principalTable: "especialidades",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__medicos__usuario__5812160E",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "usuario_rol",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuario_id = table.Column<int>(type: "int", nullable: true),
                    rol_id = table.Column<int>(type: "int", nullable: true),
                    fecha_asignacion = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__usuario___3213E83F3A4A45A1", x => x.id);
                    table.ForeignKey(
                        name: "FK__usuario_r__rol_i__5165187F",
                        column: x => x.rol_id,
                        principalTable: "roles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__usuario_r__usuar__5070F446",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "citas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    paciente_id = table.Column<int>(type: "int", nullable: true),
                    medico_id = table.Column<int>(type: "int", nullable: true),
                    fecha = table.Column<DateTime>(type: "datetime", nullable: true),
                    motivo = table.Column<string>(type: "text", nullable: true),
                    estado = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__citas__3213E83F912C847D", x => x.id);
                    table.ForeignKey(
                        name: "FK__citas__medico_id__5CD6CB2B",
                        column: x => x.medico_id,
                        principalTable: "medicos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__citas__paciente___5BE2A6F2",
                        column: x => x.paciente_id,
                        principalTable: "pacientes",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "horarios_medicos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    medico_id = table.Column<int>(type: "int", nullable: true),
                    dia_semana = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    hora_inicio = table.Column<TimeOnly>(type: "time", nullable: true),
                    hora_fin = table.Column<TimeOnly>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__horarios__3213E83FE510813F", x => x.id);
                    table.ForeignKey(
                        name: "FK__horarios___medic__628FA481",
                        column: x => x.medico_id,
                        principalTable: "medicos",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "consultas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cita_id = table.Column<int>(type: "int", nullable: true),
                    fecha_consulta = table.Column<DateTime>(type: "datetime", nullable: true),
                    diagnostico = table.Column<string>(type: "text", nullable: true),
                    observaciones = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__consulta__3213E83F01E851CB", x => x.id);
                    table.ForeignKey(
                        name: "FK__consultas__cita___68487DD7",
                        column: x => x.cita_id,
                        principalTable: "citas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "fichas_medicas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cita_id = table.Column<int>(type: "int", nullable: true),
                    diagnostico = table.Column<string>(type: "text", nullable: true),
                    tratamiento = table.Column<string>(type: "text", nullable: true),
                    observaciones = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__fichas_m__3213E83FEC33AAFA", x => x.id);
                    table.ForeignKey(
                        name: "FK__fichas_me__cita___656C112C",
                        column: x => x.cita_id,
                        principalTable: "citas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "recordatorios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cita_id = table.Column<int>(type: "int", nullable: true),
                    fecha_envio = table.Column<DateTime>(type: "datetime", nullable: true),
                    metodo_envio = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    mensaje = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__recordat__3213E83FE9E22727", x => x.id);
                    table.ForeignKey(
                        name: "FK__recordato__cita___5FB337D6",
                        column: x => x.cita_id,
                        principalTable: "citas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "historial_medico",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    consulta_id = table.Column<int>(type: "int", nullable: true),
                    diagnostico = table.Column<string>(type: "text", nullable: true),
                    tratamiento = table.Column<string>(type: "text", nullable: true),
                    observaciones = table.Column<string>(type: "text", nullable: true),
                    fecha_registro = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__historia__3213E83FFC81D3D1", x => x.id);
                    table.ForeignKey(
                        name: "FK__historial__consu__6E01572D",
                        column: x => x.consulta_id,
                        principalTable: "consultas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "recetas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    consulta_id = table.Column<int>(type: "int", nullable: true),
                    medicamento = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    dosis = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    instrucciones = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__recetas__3213E83F8A3020E9", x => x.id);
                    table.ForeignKey(
                        name: "FK__recetas__consult__6B24EA82",
                        column: x => x.consulta_id,
                        principalTable: "consultas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_citas_medico_id",
                table: "citas",
                column: "medico_id");

            migrationBuilder.CreateIndex(
                name: "IX_citas_paciente_id",
                table: "citas",
                column: "paciente_id");

            migrationBuilder.CreateIndex(
                name: "IX_consultas_cita_id",
                table: "consultas",
                column: "cita_id");

            migrationBuilder.CreateIndex(
                name: "IX_fichas_medicas_cita_id",
                table: "fichas_medicas",
                column: "cita_id");

            migrationBuilder.CreateIndex(
                name: "IX_historial_medico_consulta_id",
                table: "historial_medico",
                column: "consulta_id");

            migrationBuilder.CreateIndex(
                name: "IX_horarios_medicos_medico_id",
                table: "horarios_medicos",
                column: "medico_id");

            migrationBuilder.CreateIndex(
                name: "IX_medicos_especialidad_id",
                table: "medicos",
                column: "especialidad_id");

            migrationBuilder.CreateIndex(
                name: "IX_medicos_usuario_id",
                table: "medicos",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_recetas_consulta_id",
                table: "recetas",
                column: "consulta_id");

            migrationBuilder.CreateIndex(
                name: "IX_recordatorios_cita_id",
                table: "recordatorios",
                column: "cita_id");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_rol_rol_id",
                table: "usuario_rol",
                column: "rol_id");

            migrationBuilder.CreateIndex(
                name: "UQ_usuario_rol",
                table: "usuario_rol",
                columns: new[] { "usuario_id", "rol_id" },
                unique: true,
                filter: "[usuario_id] IS NOT NULL AND [rol_id] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fichas_medicas");

            migrationBuilder.DropTable(
                name: "historial_medico");

            migrationBuilder.DropTable(
                name: "horarios_medicos");

            migrationBuilder.DropTable(
                name: "recetas");

            migrationBuilder.DropTable(
                name: "recordatorios");

            migrationBuilder.DropTable(
                name: "usuario_rol");

            migrationBuilder.DropTable(
                name: "consultas");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "citas");

            migrationBuilder.DropTable(
                name: "medicos");

            migrationBuilder.DropTable(
                name: "pacientes");

            migrationBuilder.DropTable(
                name: "especialidades");

            migrationBuilder.DropTable(
                name: "usuarios");
        }
    }
}
