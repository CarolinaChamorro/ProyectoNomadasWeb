using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace ProyectoServicioTuristico.Models
{
    public class Guia
    {
        //Investigar identity/AspNetUser
        [Key]
        public int GuiaId { get; set; }
        [Required(ErrorMessage = "Ingrese su Primer Nombre")]
        [Display(Name = "Primer Nombre")]
        public string PrimerNombre { get; set; }
        [Display(Name = "Segundo Nombre")]
        public string SegundoNombre { get; set; }
        [Required(ErrorMessage = "Ingrese su Apellido Paterno")]
        [Display(Name = "Apellidos Paterno")]
        public string ApellidoPaterno { get; set; }
        [Display(Name = "Apellidos Materno")]
        public string  ApellidoMaterno { get; set; }
        [Display(Name = "Nombres Completos")]
        public string NombresCompletos => $"{PrimerNombre} {ApellidoPaterno}";
        public string Edad { get; set; }
        public int Sexo { get; set; }
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Ingresar su Foto Perfil")]
        [Display(Name = "Foto de Perfil")]
        public string FotoPerfil { get; set; }
        //relacion idiomas
        public List<DetalleGuiaIidioma> DetalleGuiaIidioma= new List<DetalleGuiaIidioma>();
        //relacionruta
        public List<Ruta> Ruta = new List<Ruta>();
    }
}
