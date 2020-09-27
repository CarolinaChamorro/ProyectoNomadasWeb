using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoServicioTuristico.ViewModels
{
    public class EditClasificacionViewModel:CreateClasificacionViewModel
    {
        [Key]
        public int ClasificacionIdEdit { get; set; }
        public string ExistingPhotoPath { get; set; }
    }
}
