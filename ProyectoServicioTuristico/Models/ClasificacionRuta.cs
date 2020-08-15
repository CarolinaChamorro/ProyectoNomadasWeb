using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoServicioTuristico.Models
{
    public class ClasificacionRuta
    {
        [Key]
        public int ClasificacionRutaId { get; set; }
        [Required(ErrorMessage = "Ingrese el nombre de la clasificación")]
        [Display(Name = "Nombre de la clasificación")]
        public string Nombre { get; set; }
        //Id Provincia/Canton
        [ForeignKey("Provincia")]
        public int ProvinciaId { get; set; }
        public Provincia Provincia { get; set; }
        [ForeignKey("Canton")]
        public int CantonId { get; set; }
        public Canton Canton { get; set; }
        //listas de rutas
        public List<Ruta> Ruta = new List<Ruta>();
    }
}
