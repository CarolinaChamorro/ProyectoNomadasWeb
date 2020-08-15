using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoServicioTuristico.Models
{
    public class ClasificacionRuta
    {
        [Key]
        public int ClasificacionRutaId { get; set; }
        [Required(ErrorMessage = "Ingrese nombre del de la clasificación de ruta")]
        [Display(Name = "Nombre de la clasificación")]
        public string Nombre { get; set; }
        //Id Provincia/Canton
        [ForeignKey("Provincia")]
        public int ProvinciaId { get; set; }
        public Provincia Provincia { get; set; }
        [ForeignKey("Caton")]
        public int CantonId { get; set; }
        public Canton Canton { get; set; }
    }
}
