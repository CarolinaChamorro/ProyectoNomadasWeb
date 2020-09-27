using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoServicioTuristico.Models
{
    public class Provincia
    {
        [Key]
        public int ProvinciaId { get; set; }
        [Required(ErrorMessage = "Ingrese la provincia")]
        [Display(Name = "Provincia")]
        public string Nombre { get; set; }
        //ListCanton/ClasificacionRuta
        public List<Canton> Canton = new List<Canton>();
        public List<ClasificacionRuta> ClasificacionRutas = new List<ClasificacionRuta>();

    }
}
