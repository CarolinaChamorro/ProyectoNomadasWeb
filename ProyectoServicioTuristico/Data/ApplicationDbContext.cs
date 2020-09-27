using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProyectoServicioTuristico.Models;

namespace ProyectoServicioTuristico.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Canton> Cantones { get; set; }
        public DbSet<ClasificacionRuta> ClasificacionRutas { get; set; }
        public DbSet<DetalleGuiaIdioma> DetalleGuiaIdiomas { get; set; }
        public DbSet<FotografiaGuia> FotografiaGuias { get; set; }
        public DbSet<FotografiaRuta> FotografiaRutas { get; set; }
        public DbSet<Guia> Guias { get; set; }
        public DbSet<Idioma> Idiomas { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Ruta> Rutas { get; set; }
       
    }
}
