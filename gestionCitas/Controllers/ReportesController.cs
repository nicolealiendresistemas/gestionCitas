using Microsoft.AspNetCore.Mvc;
using gestionCitas.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ClosedXML.Excel;
using OfficeOpenXml;

namespace gestionCitas.Controllers
{
    public class ReportesController : Controller
    {
        private readonly GestioncitasContext _context;

        public ReportesController(GestioncitasContext context)
        {
            _context = context;
        }

        // Acción para mostrar la lista de médicos
        public IActionResult Index()
        {
            var medicos = _context.Medicos
                .Include(m => m.Especialidad) // Asegúrate de incluir Especialidad para evitar nulls
                .ToList();
            return View(medicos);
        }

        // Acción para mostrar las citas de un médico específico
        public IActionResult CitasPorMedico(int id)
        {
            var citas = _context.Citas
                .Where(c => c.MedicoId == id)
                .Include(c => c.Paciente)
                .Select(c => new
                {
                    c.Fecha,
                    c.Motivo,
                    Paciente = c.Paciente.Nombre,
                    c.Estado
                })
                .ToList();

            var medico = _context.Medicos.Find(id);

            ViewBag.MedicoId = id;
            ViewBag.Medico = medico?.Nombre;
            return View(citas);
        }

        // Acción para generar el PDF
        public IActionResult GenerarPDF(int id)
        {
            var citas = _context.Citas
                .Where(c => c.MedicoId == id)
                .Select(c => new
                {
                    c.Fecha,
                    c.Motivo,
                    Paciente = c.Paciente.Nombre,
                    c.Estado
                }).ToList();

            var medico = _context.Medicos.Include(m => m.Especialidad).FirstOrDefault(m => m.Id == id);

            using (var memoryStream = new MemoryStream())
            {
                var document = new Document(PageSize.A4, 25, 25, 30, 30);
                var writer = PdfWriter.GetInstance(document, memoryStream);

                writer.PageEvent = new WatermarkHandler("Confidencial"); // Agregar marca de agua
                document.Open();

                // Estilo de fuente
                var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.BLACK);
                var subtitleFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.DARK_GRAY);
                var tableHeaderFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.WHITE);
                var cellFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK);
                var footerFont = FontFactory.GetFont(FontFactory.HELVETICA, 8, BaseColor.GRAY);

                // Agregar el logo
                string logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "logo.jpg");
                if (System.IO.File.Exists(logoPath))
                {
                    Image logo = Image.GetInstance(logoPath);
                    logo.ScaleAbsolute(50, 50); // Escalar el logo
                    logo.Alignment = Image.ALIGN_LEFT;
                    document.Add(logo);
                }

                // Título con fecha
                var title = new Paragraph($"Reporte de Citas del Médico: {medico.Nombre}", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 10
                };
                document.Add(title);

                var subtitle = new Paragraph($"Especialidad: {medico.Especialidad?.Nombre ?? "N/A"}", subtitleFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 10
                };
                document.Add(subtitle);

                // Fecha de creación del PDF
                var fechaCreacion = new Paragraph($"Fecha de creación del reporte: {DateTime.Now:dd/MM/yyyy HH:mm}", footerFont)
                {
                    Alignment = Element.ALIGN_RIGHT,
                    SpacingAfter = 20
                };
                document.Add(fechaCreacion);

                // Crear la tabla
                var table = new PdfPTable(4) { WidthPercentage = 100 };
                table.SetWidths(new[] { 20f, 40f, 20f, 20f });

                // Encabezado de la tabla
                var headerBackgroundColor = new BaseColor(63, 81, 181); // Azul oscuro
                foreach (var header in new[] { "Fecha", "Motivo", "Paciente", "Estado" })
                {
                    var cell = new PdfPCell(new Phrase(header, tableHeaderFont))
                    {
                        BackgroundColor = headerBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        Padding = 5
                    };
                    table.AddCell(cell);
                }

                // Agregar filas a la tabla
                foreach (var cita in citas)
                {
                    table.AddCell(new PdfPCell(new Phrase(cita.Fecha.HasValue ? cita.Fecha.Value.ToString("dd/MM/yyyy HH:mm") : "Sin fecha", cellFont)) { Padding = 5 });
                    table.AddCell(new PdfPCell(new Phrase(cita.Motivo, cellFont)) { Padding = 5 });
                    table.AddCell(new PdfPCell(new Phrase(cita.Paciente, cellFont)) { Padding = 5 });
                    table.AddCell(new PdfPCell(new Phrase(cita.Estado, cellFont)) { Padding = 5 });
                }

                document.Add(table);

                // Cerrar el documento
                document.Close();

                var content = memoryStream.ToArray();
                return File(content, "application/pdf", $"Reporte_Citas_Medico_{medico.Nombre}.pdf");
            }
        }

        public IActionResult GenerarExcel(int id)
        {
            // Configurar el contexto de licencia
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Obtener las citas del médico
            var citas = _context.Citas
                .Where(c => c.MedicoId == id)
                .Include(c => c.Paciente)
                .Select(c => new
                {
                    c.Fecha,
                    c.Motivo,
                    Paciente = c.Paciente.Nombre,
                    c.Estado
                })
                .ToList();

            // Obtener la información del médico
            var medico = _context.Medicos
                .Include(m => m.Especialidad)
                .FirstOrDefault(m => m.Id == id);

            using (var memoryStream = new MemoryStream())
            {
                using (var package = new ExcelPackage(memoryStream))
                {
                    var worksheet = package.Workbook.Worksheets.Add("Citas");

                    // Configurar estilos generales
                    worksheet.Cells.Style.Font.Name = "Calibri";
                    worksheet.Cells.Style.Font.Size = 11;

                    // Agregar título del reporte
                    worksheet.Cells[1, 1].Value = "Reporte de Citas del Médico";
                    worksheet.Cells[1, 1, 1, 4].Merge = true; // Unir celdas
                    worksheet.Cells[1, 1].Style.Font.Bold = true;
                    worksheet.Cells[1, 1].Style.Font.Size = 16;
                    worksheet.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    // Agregar información del médico
                    worksheet.Cells[2, 1].Value = $"Nombre: {medico.Nombre}";
                    worksheet.Cells[3, 1].Value = $"Especialidad: {medico.Especialidad?.Nombre ?? "N/A"}";
                    worksheet.Cells[4, 1].Value = $"Fecha de generación: {DateTime.Now:dd/MM/yyyy}";
                    worksheet.Cells[2, 1, 4, 4].Style.Font.Bold = true;

                    // Agregar encabezado de la tabla
                    worksheet.Cells[6, 1].Value = "Fecha";
                    worksheet.Cells[6, 2].Value = "Motivo";
                    worksheet.Cells[6, 3].Value = "Paciente";
                    worksheet.Cells[6, 4].Value = "Estado";
                    worksheet.Cells[6, 1, 6, 4].Style.Font.Bold = true;
                    worksheet.Cells[6, 1, 6, 4].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[6, 1, 6, 4].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                    worksheet.Cells[6, 1, 6, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    // Agregar las filas de datos
                    int row = 7;
                    foreach (var cita in citas)
                    {
                        worksheet.Cells[row, 1].Value = cita.Fecha?.ToString("dd/MM/yyyy HH:mm");
                        worksheet.Cells[row, 2].Value = cita.Motivo;
                        worksheet.Cells[row, 3].Value = cita.Paciente;
                        worksheet.Cells[row, 4].Value = cita.Estado;
                        row++;
                    }

                    // Aplicar bordes a la tabla
                    var dataRange = worksheet.Cells[6, 1, row - 1, 4];
                    dataRange.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    // Ajustar el tamaño de las columnas
                    worksheet.Cells[1, 1, row - 1, 4].AutoFitColumns();

                    // Guardar el archivo
                    package.Save();
                }

                // Retornar el archivo generado
                return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Reporte_Citas_Medico_{medico.Nombre}.xlsx");
            }
        }




        // Clase para manejar la marca de agua
        public class WatermarkHandler : PdfPageEventHelper
        {
            private readonly string _watermarkText;

            public WatermarkHandler(string watermarkText)
            {
                _watermarkText = watermarkText;
            }

            public override void OnEndPage(PdfWriter writer, Document document)
            {
                var content = writer.DirectContentUnder;
                var font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 100, BaseColor.LIGHT_GRAY); // Aumenta el tamaño de la fuente
                var phrase = new Phrase(_watermarkText, font);

                // Calcula el centro de la página
                float x = document.PageSize.Width / 2;
                float y = document.PageSize.Height / 2;

                // Ajusta la posición, el ángulo y el tamaño
                ColumnText.ShowTextAligned(
                    content,
                    Element.ALIGN_CENTER,
                    phrase,
                    x,  // Coordenada X al centro
                    y,  // Coordenada Y al centro
                    45  // Ángulo de rotación
                );
            }
        }

    }
}
