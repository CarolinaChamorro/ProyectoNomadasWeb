using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace ProyectoServicioTuristico.Models
{
    public class Guia
    {
        [Key]
        public int GuiaId { get; set; }
        [Required(ErrorMessage = "Ingrese su primer nombre")]
        [Display(Name = "Primer nombre")]
        public string PrimerNombre { get; set; }
        [Display(Name = "Segundo nombre")]
        public string SegundoNombre { get; set; }
        [Required(ErrorMessage = "Ingrese su apellido paterno")]
        [Display(Name = "Apellido paterno")]
        public string ApellidoPaterno { get; set; }
        [Display(Name = "Apellido materno")]
        public string  ApellidoMaterno { get; set; }
        [Display(Name = "Nombre completo")]
        public string NombreCompleto{ get; set; }
        [Required(ErrorMessage = "Ingrese su edad")]
        [Display(Name = "Edad")]
        public string Edad { get; set; }
        [Required(ErrorMessage = "Ingrese su género")]
        [Display(Name = "Sexo")]
        public int Sexo { get; set; }
        [Display(Name = "Telefono")]
        public string Telefono { get; set; }
        //fotoPerfil
        [Required(ErrorMessage = "Ingrese su foto de perfil")]
        [Display(Name = "Foto de perfil")]
        public string FotoPerfil { get; set; }
        public string Identidad { get; set; }
        //relacion idiomas
        public List<DetalleGuiaIdioma> DetalleGuiaIdioma= new List<DetalleGuiaIdioma>();
        //relacionruta
        public List<Ruta> Ruta = new List<Ruta>();
        public Guia()
        {
            NombreCompleto = PrimerNombre + " " + ApellidoPaterno;
        }
    }
}
