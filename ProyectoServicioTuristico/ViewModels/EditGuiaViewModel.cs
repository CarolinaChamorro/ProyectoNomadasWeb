using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoServicioTuristico.ViewModels
{
    public class EditGuiaViewModel:CreateGuiaViewModel
    {
        [Key]
        public int GuiaIdEdit { get; set; }
        public string ExistingPhotoPath { get; set; }
    }
}
