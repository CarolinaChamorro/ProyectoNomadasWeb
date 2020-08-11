namespace ProyectoServicioTuristico.Models
{
    public class DetalleGuiaIidioma
    {
        //Key compuesta
        public int IdGuia { get; set; }
        public Guia Guia { get; set; }
        public int IdIdioma { get; set; }
        public Idioma Idioma { get; set; }
    }
}
