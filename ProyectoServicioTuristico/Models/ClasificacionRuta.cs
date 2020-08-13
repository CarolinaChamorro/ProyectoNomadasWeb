using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoServicioTuristico.Models
{
    public class ClasificacionRuta
    {
        [Key]
        public int IdClasificacionRuta { get; set; }
        public string Nombre { get; set; }
        //Id Provincia/Canton
        public int IdProvincia { get; set; }
        public Provincia Provincia { get; set; }
        public int IdCanton { get; set; }
        public Canton Canton { get; set; }
        //listas de rutas
        public List<Ruta> Ruta = new List<Ruta>();
    }
}
