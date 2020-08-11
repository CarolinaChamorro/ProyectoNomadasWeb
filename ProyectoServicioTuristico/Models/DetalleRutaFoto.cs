using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoServicioTuristico.Models
{
    public class DetalleRutaFoto
    {
        //Key compuesta
        public int IdRuta { get; set; }
        public Ruta Ruta { get; set; }
        public int IdFotografia { get; set; }
        public Fotografia Fotografia { get; set; }
    }
}
