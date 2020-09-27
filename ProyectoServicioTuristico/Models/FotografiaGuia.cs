using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoServicioTuristico.Models
{
    public class FotografiaGuia : Fotografia
    {
        [Key]
        public int FotografiaId { get; set; }
        // Foreign Key de usuario
        [ForeignKey("Guia")]
        public int GuiaId { get; set; }
        public Guia Guia { get; set; }
    }
}