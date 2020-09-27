using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoServicioTuristico.Models
{
    public class FotografiaRuta : Fotografia
    {
        [Key]
        public int FotografiaId { get; set; }
        //Foreign key de ruta 
        [ForeignKey("Ruta")]
        public int? RutaId { get; set;  } 
        public Ruta Ruta { get; set; }
    }
}