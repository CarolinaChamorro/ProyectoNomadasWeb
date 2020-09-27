﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoServicioTuristico.Data;
using ProyectoServicioTuristico.Models;
using ProyectoServicioTuristico.ViewModels;

namespace ProyectoServicioTuristico.Controllers
{
    public class ClasificacionRutasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ClasificacionRutasController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }
        [AllowAnonymous]
        // GET: ClasificacionRutas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ClasificacionRutas
                .Include(c => c.Canton)
                .Include(c => c.Provincia)
                .Include(r => r.Rutas);
            return View(await applicationDbContext.ToListAsync());
            
        }
        [Authorize(Roles ="Administrador")]
        public async Task<IActionResult> Lista()
        {
            var applicationDbContext = _context.ClasificacionRutas
                .Include(c => c.Canton)
                .Include(c => c.Provincia)
                .Include(r => r.Rutas);
            return View(await applicationDbContext.ToListAsync());

        }
        [AllowAnonymous]
        public async Task<IActionResult> GuiasLista()
        {
            var guiasLista = _context.Guias
                .Include(i => i.DetalleGuiaIdiomas)
                .ThenInclude(i => i.Idioma);
            return View(await guiasLista.ToListAsync());
        }
        [AllowAnonymous]
        // GET: ClasificacionRutas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clasificacionRuta = await _context.ClasificacionRutas
                .Include(c => c.Canton)
                .Include(c => c.Provincia)
                .Include(r => r.Rutas)
                .Include(f => f.Rutas).ThenInclude(f => f.FotografiaRutas)
                .Include(g => g.Rutas).ThenInclude(g => g.Guia)
                .FirstOrDefaultAsync(m => m.ClasificacionRutaId == id);
            if (clasificacionRuta == null)
            {
                return NotFound();
            }

            return View(clasificacionRuta);
        }
        [AllowAnonymous]
        public async Task<IActionResult> RutaGuia(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var rutaGuia = await _context.Rutas
                .Include(r => r.Guia)
                .Include(r => r.Guia.DetalleGuiaIdiomas).ThenInclude(g => g.Idioma)
                .Include(f => f.FotografiaRutas)
                .FirstOrDefaultAsync(r => r.RutaId == id);
            if (rutaGuia == null)
            {
                return NotFound();
            }
            return View(rutaGuia);
        }
        [AllowAnonymous]
        public async Task<IActionResult> PerfilGuia(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var perfilGuia = await  _context.Guias
                .Include( g => g.Rutas)
                .Include(g => g.Rutas).ThenInclude(r => r.FotografiaRutas)
                .Include(g => g.Rutas).ThenInclude(r => r.ClasificacionRuta)
                .Include(f => f.FotografiaGuias)
                .Include(g => g.DetalleGuiaIdiomas).ThenInclude(r => r.Idioma)
                .FirstOrDefaultAsync(m => m.GuiaId == id);
            if (perfilGuia == null)
            {
                return NotFound();
            }
            return View(perfilGuia);
        }
        [Authorize(Roles = "Administrador")]
        // GET: ClasificacionRutas/Create
        public IActionResult Create()
        {
            ViewData["CantonId"] = new SelectList(_context.Cantones, "CantonId", "Nombre");
            ViewData["ProvinciaId"] = new SelectList(_context.Provincias, "ProvinciaId", "Nombre");
            return View();
        }

        // POST: ClasificacionRutas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateClasificacionViewModel clasifiViewModel)
        {
            var clasificacionRuta = new ClasificacionRuta();
            if (ModelState.IsValid)
            {
                string unicoNombreArchivo = UploadedFile(clasifiViewModel);

                clasificacionRuta.Nombre = clasifiViewModel.Nombre;
                clasificacionRuta.Descripcion = clasifiViewModel.Descripcion;
                clasificacionRuta.ProvinciaId = clasifiViewModel.ProvinciaId;
                clasificacionRuta.CantonId = clasifiViewModel.CantonId;
                clasificacionRuta.Foto = unicoNombreArchivo;

                _context.Add(clasificacionRuta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Lista));
            }

            ViewData["CantonId"] = new SelectList(_context.Cantones, "CantonId", "Nombre", clasificacionRuta.CantonId);
            ViewData["ProvinciaId"] =
                new SelectList(_context.Provincias, "ProvinciaId", "Nombre", clasificacionRuta.ProvinciaId);
            return View();
        }

        private string UploadedFile(CreateClasificacionViewModel createClasificacionViewModel)
        {
            string unicoArchivo = null;

            if (createClasificacionViewModel.ArchivoFoto != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/clasificaciones");
                unicoArchivo = Guid.NewGuid().ToString() + "_" + createClasificacionViewModel.ArchivoFoto.FileName;
                string filePath = Path.Combine(uploadsFolder, unicoArchivo);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    createClasificacionViewModel.ArchivoFoto.CopyTo(fileStream);
                }
            }
            return unicoArchivo;
        }
        [Authorize(Roles = "Administrador")]
        // GET: ClasificacionRutas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clasificacionRuta = await _context.ClasificacionRutas
                .FirstOrDefaultAsync(m=>m.ClasificacionRutaId==id);
            if (clasificacionRuta == null)
            {
                return NotFound();
            }
            var editmodel = new EditClasificacionViewModel
            {
                ClasificacionIdEdit=clasificacionRuta.ClasificacionRutaId,
                Nombre=clasificacionRuta.Nombre,
                Descripcion=clasificacionRuta.Descripcion,
                ProvinciaId=clasificacionRuta.ProvinciaId,
                CantonId=clasificacionRuta.CantonId,
                ExistingPhotoPath = clasificacionRuta.Foto
             };

            ViewData["CantonId"] = new SelectList(_context.Cantones, "CantonId", "Nombre", clasificacionRuta.CantonId);
            ViewData["ProvinciaId"] = new SelectList(_context.Provincias, "ProvinciaId", "Nombre", clasificacionRuta.ProvinciaId);
            return View(editmodel);
        }

        // POST: ClasificacionRutas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, EditClasificacionViewModel editmodel)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clasificacionRuta = await _context.ClasificacionRutas
                .FindAsync(id);
            if (ModelState.IsValid)
            {
                    clasificacionRuta.Nombre = editmodel.Nombre;
                    clasificacionRuta.Descripcion = editmodel.Descripcion;
                    clasificacionRuta.ProvinciaId = editmodel.ProvinciaId;
                    clasificacionRuta.CantonId = editmodel.CantonId;

                if (editmodel.ArchivoFoto != null)
                {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images/clasificaciones",
                            clasificacionRuta.Foto);
                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);
                    clasificacionRuta.Foto = UploadedFile(editmodel);
                }

                _context.Update(clasificacionRuta);
                    await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Lista));
            }
            ViewData["CantonId"] = new SelectList(_context.Cantones, "CantonId", "Nombre", clasificacionRuta.CantonId);
            ViewData["ProvinciaId"] = new SelectList(_context.Provincias, "ProvinciaId", "Nombre", clasificacionRuta.ProvinciaId);
            return View(editmodel);
        }
        [Authorize(Roles = "Administrador")]
        // GET: ClasificacionRutas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clasificacionRuta = await _context.ClasificacionRutas
                .Include(c => c.Canton)
                .Include(c => c.Provincia)
                .FirstOrDefaultAsync(m => m.ClasificacionRutaId == id);
            if (clasificacionRuta == null)
            {
                return NotFound();
            }

            return View(clasificacionRuta);
        }

        // POST: ClasificacionRutas/Delete/5
        [Authorize(Roles = "Administrador")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clasificacionRuta = await _context.ClasificacionRutas.FindAsync(id);
            //para borrar imagen
            var imagenPath = Path.Combine(webHostEnvironment.WebRootPath, "images/clasificaciones",clasificacionRuta.Foto);
            if (System.IO.File.Exists(imagenPath))
                System.IO.File.Delete(imagenPath);

            _context.ClasificacionRutas.Remove(clasificacionRuta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

        private bool ClasificacionRutaExists(int id)
        {
            return _context.ClasificacionRutas.Any(e => e.ClasificacionRutaId == id);
        }
    }
}