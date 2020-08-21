namespace ProyectoServicioTuristico.Models
{
    public class ModificacionRol
    {
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string[] AumentarIds { get; set; }
        public string[] BorrarIds { get; set; }
    }
}