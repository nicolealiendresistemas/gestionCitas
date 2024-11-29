using gestionCitas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace gestionCitas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GestioncitasContext _context;

        public HomeController(ILogger<HomeController> logger, GestioncitasContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Gráfico 1: Porcentaje de médicos por especialidad
            var especialidades = _context.Especialidades.Include(e => e.Medicos).ToList();
            var especialidadesLabels = especialidades.Select(e => e.Nombre).ToList();
            var especialidadesData = especialidades.Select(e => e.Medicos.Count()).ToList();

            ViewBag.EspecialidadesLabels = especialidadesLabels;
            ViewBag.EspecialidadesData = especialidadesData;

            // Gráfico 2: Pacientes por rango de edad
            var pacientes = _context.Pacientes.ToList();
            var pacientesEdad = new int[4];
            foreach (var paciente in pacientes)
            {
                var edad = paciente.FechaNacimiento.HasValue
                    ? DateTime.Now.Year - paciente.FechaNacimiento.Value.Year
                    : 0;

                if (edad <= 18) pacientesEdad[0]++;
                else if (edad <= 35) pacientesEdad[1]++;
                else if (edad <= 50) pacientesEdad[2]++;
                else pacientesEdad[3]++;
            }

            ViewBag.PacientesEdad = pacientesEdad;

            // Gráfico 3: Estado de las citas
            var citas = _context.Citas.ToList();
            var citasPendientes = citas.Count(c => c.Estado == "Pendiente");
            var citasConfirmadas = citas.Count(c => c.Estado == "Confirmada");
            var citasCanceladas = citas.Count(c => c.Estado == "Cancelada");

            ViewBag.CitasEstadoLabels = new[] { "Pendientes", "Confirmadas", "Canceladas" };
            ViewBag.CitasEstadoData = new[] { citasPendientes, citasConfirmadas, citasCanceladas };

            // Gráfico 4: Consultas recientes
            var diasSemanaLabels = new List<string> { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" };

            var horariosAgrupados = _context.HorariosMedicos
                .GroupBy(h => h.DiaSemana)
                .Select(g => new { Dia = g.Key, CantidadMedicos = g.Count() })
                .ToList();

            var medicosPorDiaData = diasSemanaLabels.Select(dia =>
                horariosAgrupados.FirstOrDefault(h => h.Dia == dia)?.CantidadMedicos ?? 0
            ).ToList();

            ViewBag.DiasSemanaLabels = diasSemanaLabels;
            ViewBag.MedicosPorDiaData = medicosPorDiaData;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
