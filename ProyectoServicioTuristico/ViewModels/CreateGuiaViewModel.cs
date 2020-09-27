using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using ProyectoServicioTuristico.Models;

namespace ProyectoServicioTuristico.ViewModels
{
    public class CreateGuiaViewModel
    {
        [Key]
        public int GuiaId { get; set; }
        [Required(ErrorMessage = "Ingrese su primer nombre")]
        [Display(Name = "Primer Nombre")]
        public string PrimerNombre { get; set; }
        [Display(Name = "Segundo Nombre")]
        public string SegundoNombre { get; set; }
        [Required(ErrorMessage = "Ingrese su apellido paterno")]
        [Display(Name = "Apellidos Paterno")]
        public string ApellidoPaterno { get; set; }
        [Display(Name = "Apellidos Materno")]
        public string  ApellidoMaterno { get; set; }
        [Display(Name = "Nombre completo")]
        public string NombreCompleto { get; set; }
        [Required(ErrorMessage = "Ingrese su edad")]
        public string Edad { get; set; }
        [Required(ErrorMessage = "Ingrese su género")]
        public int Sexo { get; set; }
        [Required(ErrorMessage = "Ingrese su número de contacto")]
        public string Telefono { get; set; }
        [Display(Name = "Subir Fotografia")]
        public IFormFile ArchivoImagen { get; set; }
        public string Identidad { get; set; }
        public ICollection<DetalleGuiaIdioma> DetalleGuiaIdiomas { get; set; }
        //relacionruta
        public ICollection<Ruta> Rutas { get; set; }
        public ICollection<FotografiaGuia> FotografiaGuias { get; set; }
        public CreateGuiaViewModel()
        {
            NombreCompleto = PrimerNombre + " " + ApellidoPaterno;
        }
    }
}