using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoServicioTuristico.Models
{
    public class Idioma
    {
        [Key]
        public int IdiomaId { get; set; }
        [Display(Name = "Idioma")]
        public string Nombre { get; set; }
        //guia
        public List<DetalleGuiaIdioma> DetalleGuiaIdioma = new List<DetalleGuiaIdioma>();
    }
}
