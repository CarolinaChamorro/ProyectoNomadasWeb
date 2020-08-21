using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace ProyectoServicioTuristico.ViewModels
{
    public class GuiaViewModel
    {
        [Key]
        public int GuiaId { get; set; }
        public string PrimerNombre { get; set; }
        [Display(Name = "Segundo Nombre")]
        public string SegundoNombre { get; set; }
        [Display(Name = "Apellidos Paterno")]
        public string ApellidoPaterno { get; set; }
        [Display(Name = "Apellidos Materno")]
        public string  ApellidoMaterno { get; set; }
        public string Edad { get; set; }
        public int Sexo { get; set; }
        public string Telefono { get; set; }
        [Display(Name = "Subir Fotografia")]
        public IFormFile ArchivoImagen { get; set; }
        public string Identidad { get; set; }
    }
}