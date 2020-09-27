using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace ProyectoServicioTuristico.ViewModels
{
    public class FotografiaViewModel
    {
        [Display(Name = "Subir Fotografia")]
        public List<IFormFile> ArchivoFotoFiles { get; set; }
        
        [Display(Name = "Descripcion de la imagen")]
        public string DescripcionFoto { get; set; }
    }
}