
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoServicioTuristico.Models
{
    public class Ruta
    {
        [Key]
        public int IdRuta { get; set; }
        public string Nombre { get; set; }
        public string PuntoPartida { get; set; }
        public string PuntoLLegada { get; set; }
        public string LugaresVisistar { get; set; }
        public decimal Precio { get; set; }
        public string DescripcionServicios { get; set; }
        //Relacion con guia
        public int IdGuia { get; set; }
        public Guia Guia { get; set; }
        //Clasificación Id
        public int IdClasificacionRuta { get; set; }
        public ClasificacionRuta ClasificacionRuta { get; set; }
        //relacion fotografia
        public List<DetalleRutaFoto> DetalleRutaFoto = new List<DetalleRutaFoto>();
    }
}
