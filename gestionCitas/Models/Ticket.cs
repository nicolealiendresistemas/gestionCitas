using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestionCitas.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; } // ID del ticket

        [Required]
        public int PacienteId { get; set; }

        [ForeignKey("PacienteId")]
        public Paciente Paciente { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
