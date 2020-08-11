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
        public int IdGuia { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string  ApellidoMaterno { get; set; }
        public string Edad { get; set; }
        public int Sexo { get; set; }
        public string Telefono { get; set; }
        //relacion idiomas
        public List<DetalleGuiaIidioma> DetalleGuiaIidioma= new List<DetalleGuiaIidioma>();
        //relacionruta
        public List<DetalleGuiaRuta> DetalleGuiaRuta = new List<DetalleGuiaRuta>();
        //fotoPerfil
        public string FotoPerfil { get; set; }
    }
}
