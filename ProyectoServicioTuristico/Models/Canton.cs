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
        [Display(Name = "Nombre del cantón")]
        public string Nombre { get; set; }
        //IdProvincia/ClasificacionRuta
        [ForeignKey("Provincia")]
        public int ProvinciaId { get; set; }
        public Provincia Provincia { get; set; }
        public List<ClasificacionRuta> ClasificacionRuta = new List<ClasificacionRuta>();
    }
}
