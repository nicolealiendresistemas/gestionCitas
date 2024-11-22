namespace gestionCitas.Models
{
    public class CarritoCitasItem
    {
        public int Id { get; set; }
        public string Paciente { get; set; }
        public string Medico { get; set; }
        public DateTime Fecha { get; set; }
        public string Motivo { get; set; }
    }
}
