using System.ComponentModel.DataAnnotations;

namespace ProyectoServicioTuristico.Models
{
    public abstract class Fotografia
    {
       
        [Display(Name = "Fotos")]
        public string Fotos { get; set; }
        [Display(Name = "Descripcion de foto")]
        public string DescripcionFoto { get; set; }
    }
}
