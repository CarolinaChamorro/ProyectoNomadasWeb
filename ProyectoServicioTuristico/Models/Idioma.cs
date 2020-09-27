using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoServicioTuristico.Models
{
    public class Idioma
    {
        [Key]
        public int IdiomaId { get; set; }
        [Required(ErrorMessage = "Ingrese el idioma")]
        [Display(Name = "Idioma")]
        public string Nombre { get; set; }
        //guia
        public ICollection<DetalleGuiaIdioma> DetalleGuiaIdiomas { get; set; }
    }
}
