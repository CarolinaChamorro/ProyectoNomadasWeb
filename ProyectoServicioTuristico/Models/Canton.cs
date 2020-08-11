using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoServicioTuristico.Models
{
    public class Canton
    {
        [Key]
        public int IdCanton { get; set; }
        public string Nombre { get; set; }
        //IdProvincia/ClasificacionRuta
        public int IdProvincia { get; set; }
        public Provincia Provincia { get; set; }
        public List<ClasificacionRuta> ClasificacionRuta = new List<ClasificacionRuta>();
    }
}
