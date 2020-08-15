
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoServicioTuristico.Models
{
    public class Ruta
    {
        [Key]
        public int RutaId { get; set; }
        [Required(ErrorMessage = "Ingrese el nombre de la ruta")]
        [Display(Name = "Nombre de la ruta")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Ingrese el punto de partida")]
        [Display(Name = "Punto de partida")]
        public string PuntoPartida { get; set; }
        [Required(ErrorMessage = "Ingrese el punto de destino")]
        [Display(Name = "Punto de destino")]
        public string PuntoLLegada { get; set; }
        [Required(ErrorMessage = "Ingrese el precio de la ruta")]
        [Display(Name = "Precio de la ruta")]
        public double Precio { get; set; }
        [Required(ErrorMessage = "Ingrese la descripción de los servicios")]
        [Display(Name = "Servicios")]
        public string DescripcionServicios { get; set; }
        //Relacion con guia
        [ForeignKey("Guia")]
        public int GuiaId { get; set; }
        public Guia Guia { get; set; }
        //Clasificación Id
        public int ClasificacionRutaId { get; set; }
        [ForeignKey("ClasificacionRutaId")]
        public ClasificacionRuta ClasificacionRuta { get; set; }
        //relacion fotografia
        public List<DetalleRutaFoto> DetalleRutaFoto = new List<DetalleRutaFoto>();
    }
}
