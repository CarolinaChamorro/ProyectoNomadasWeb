using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoServicioTuristico.Models
{
    public class Provincia
    {
        [Key]
        public int IdProvincia { get; set; }
        public string Nombre { get; set; }
        //ListCanton/ClasificacionRuta
        public List<Canton> Canton = new List<Canton>();
        public List<ClasificacionRuta> ClasificacionRuta = new List<ClasificacionRuta>();

    }
}
