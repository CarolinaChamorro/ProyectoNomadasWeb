using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoServicioTuristico.Models
{
    public class DetalleGuiaIdioma
    {
        [Key]
        public int DetalleGuiaIdiomaId { get; set; }
        [ForeignKey("Guia")]
        public int GuiaId { get; set; }
        public Guia Guia { get; set; }
        [ForeignKey("Idioma")]
        public int IdiomaId { get; set; }
        public Idioma Idioma { get; set; }
    }
}
