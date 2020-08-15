using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoServicioTuristico.Models
{
    public class DetalleRutaFoto
    {
        [Key]
        public int DetalleRutaFotoId { get; set; }
        //Key compuesta
        [ForeignKey("Ruta")]
        public int RutaId { get; set; }
        public Ruta Ruta { get; set; }
        [ForeignKey("Fotografia")]
        public int FotografiaId { get; set; }
        public Fotografia Fotografia { get; set; }
    }
}
