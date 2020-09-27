using Microsoft.AspNetCore.Http;
using ProyectoServicioTuristico.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoServicioTuristico.ViewModels
{
    public class CreateFotoGuiaViewModel
    {
        [Key]
        public int FotografiaId { get; set; }
        // Foreign Key de usuario
        [ForeignKey("Guia")]
        public int GuiaId { get; set; }
        public Guia Guia { get; set; }
        [Display(Name = "Subir Fotografia")]
        public IFormFile ArchivoFoto { get; set; }
        [Required(ErrorMessage = "Ingrese una descripción corta")]
        [Display(Name = "Descripcion de foto")]
        public string DescripcionFoto { get; set; }
    }
}
