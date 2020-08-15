
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoServicioTuristico.Models
{
    public class Ruta
    {
        [Key]
        public int RutaId { get; set; }
        [Required(ErrorMessage = "Ingrese el nombre de la Ruta")]
        [Display(Name = "Nombre de la ruta")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Ingrese el punto de partida")]
        [Display(Name = "Punto de partida")]
        public string PuntoPartida { get; set; }
        [Required(ErrorMessage = "Ingrese su punto de destino")]
        [Display(Name = "Punto de destino")]
        public string PuntoLLegada { get; set; }
        [Required(ErrorMessage = "Ingrese un precio de la ruta")]
        [Display(Name = "Precio")]
        public double Precio { get; set; }
        [Required(ErrorMessage = "Ingrese una descripcion de servicios")]
        [Display(Name = "Descripción de servicios")]
        public string DescripcionServicios { get; set; }
        //Relacion con guia
        [ForeignKey("Guia")]
        public int GuiaId { get; set; }
        public Guia Guia { get; set; }
        [ForeignKey("ClasificacionRuta")]
        public int ClasificacinRutaId { get; set; }
        public ClasificacionRuta ClasificacionRuta { get; set; }

        //relacion fotografia
        public List<DetalleRutaFoto> DetalleRutaFoto = new List<DetalleRutaFoto>();
    }
}
