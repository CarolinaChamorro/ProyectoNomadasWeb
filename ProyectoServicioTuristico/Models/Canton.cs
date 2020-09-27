using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoServicioTuristico.Models
{
    public class Canton
    {
        [Key]
        public int CantonId { get; set; }
        [Required(ErrorMessage = "Ingrese nombre del cantón")]
        [Display(Name = "Cantón")]
        public string Nombre { get; set; }
        //IdProvincia/ClasificacionRuta
        [Required(ErrorMessage = "Seleccione la provincia")]
        [ForeignKey("Provincia")]
        public int ProvinciaId { get; set; }
        public Provincia Provincia { get; set; }
        public List<ClasificacionRuta> ClasificacionRutas = new List<ClasificacionRuta>();
    }
}
