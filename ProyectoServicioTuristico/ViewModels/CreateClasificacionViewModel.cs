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
    public class CreateClasificacionViewModel
    {
        [Key]
        public int ClasificacionRutaId { get; set; }
        [Required(ErrorMessage = "Ingrese el nombre de la clasificación")]
        [Display(Name = "Nombre de la clasificación")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Ingrese una descripción")]
        [Display(Name = "Descripción de la clasificación")]
        public string Descripcion { get; set; }
        //Id Provincia/Canton
        [Required(ErrorMessage = "Seleccione una provincia")]
        [ForeignKey("Provincia")]
        public int ProvinciaId { get; set; }
        public Provincia Provincia { get; set; }
        [Required(ErrorMessage = "Seleccione un cantón")]
        [ForeignKey("Canton")]
        public int CantonId { get; set; }
        public Canton Canton { get; set; }
        [Display(Name = "Foto de la clasificación")]
        public IFormFile ArchivoFoto { get; set; }
        //listas de rutas
        public List<Ruta> Ruta = new List<Ruta>();
    }
}
