using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoServicioTuristico.Models
{
    public class Fotografia
    {
        [Key]
        public int FotografiaId { get; set; }
        [Required(ErrorMessage = "Ingrese fotos")]
        [Display(Name = "Fotos")]
        public string Fotos { get; set; }
        public List<DetalleRutaFoto> DetalleRutaFoto = new List<DetalleRutaFoto>();
    }
}
