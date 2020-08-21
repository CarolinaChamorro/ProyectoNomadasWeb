using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoServicioTuristico.Models
{
    public class Idioma
    {
        [Key]
        public int IdiomaId { get; set; }
        public string Nombre { get; set; }
        //guia
        public List<DetalleGuiaIidioma> DetalleGuiaIidioma = new List<DetalleGuiaIidioma>();
    }
}
