using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoServicioTuristico.Models
{
    public class DetalleGuiaRuta
    {
        //Key compuesta
        public int IdGuia { get; set; }
        public Guia Guia { get; set; }
        public int IdRuta { get; set; }
        public Ruta Ruta { get; set; }
    }
}
