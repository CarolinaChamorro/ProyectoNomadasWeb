using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ProyectoServicioTuristico.Data;
using ProyectoServicioTuristico.Models;
using ProyectoServicioTuristico.ViewModels;

namespace ProyectoServicioTuristico.Controllers
{
    public class GuiasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment webHostEnvironment;
        public GuiasController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            webHostEnvironment = hostEnvironment;
        }
        
        // GET: Guias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Guias.ToListAsync());
        }
        [Authorize(Roles ="Guia")]
        [Authorize]
        // GET: Guias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guia = await _context.Guias
                .FirstOrDefaultAsync(m => m.GuiaId == id);
            if (guia == null)
            {
                return NotFound();
            }

            return View(guia);
        }

        // GET: Guias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Guias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GuiaViewModel guiamodel)
        {
            string unicoNombreArchivo = UploadedFile(guiamodel);
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                var guia = new Guia
                {
                    PrimerNombre = guiamodel.PrimerNombre,
                    SegundoNombre = guiamodel.SegundoNombre,
                    ApellidoPaterno = guiamodel.ApellidoPaterno,
                    ApellidoMaterno = guiamodel.ApellidoMaterno,
                    NombreCompleto = guiamodel.PrimerNombre + " " + guiamodel.ApellidoPaterno,
                    Edad = guiamodel.Edad,
                    Sexo = guiamodel.Sexo,
                    Telefono = guiamodel.Telefono,
                    FotoPerfil = unicoNombreArchivo,
                    Identidad = await _userManager.GetUserNameAsync(user)
                    };
                    _context.Add(guia);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                
            }
            return View();
        }
        //Subir foto del guia
        private string UploadedFile(GuiaViewModel guiamodel)
        {
            string unicoArchivo = null;

            if (guiamodel.ArchivoImagen != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/guias");
                unicoArchivo = Guid.NewGuid().ToString() + "_" + guiamodel.ArchivoImagen.FileName;
                string filePath = Path.Combine(uploadsFolder, unicoArchivo);
                var fileStream = new FileStream(filePath, FileMode.Create);
                guiamodel.ArchivoImagen.CopyTo(fileStream);

            }
            return unicoArchivo;
        }

        // GET: Guias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guia = await _context.Guias.FindAsync(id);
            if (guia == null)
            {
                return NotFound();
            }
            return View(guia);
        }

        // POST: Guias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GuiaId,PrimerNombre,SegundoNombre,ApellidoPaterno,ApellidoMaterno,Edad,Sexo,Telefono,FotoPerfil")] Guia guia)
        {
           
            if (id != guia.GuiaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuiaExists(guia.GuiaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

       

        // GET: Guias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guia = await _context.Guias
                .FirstOrDefaultAsync(m => m.GuiaId == id);
            if (guia == null)
            {
                return NotFound();
            }

            return View(guia);
        }

        // POST: Guias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guia = await _context.Guias.FindAsync(id);
            //para borrar imagen
            var imagenPath = Path.Combine(webHostEnvironment.WebRootPath, "images/guias", guia.FotoPerfil);
            if (System.IO.File.Exists(imagenPath))
                System.IO.File.Delete(imagenPath);

            _context.Guias.Remove(guia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GuiaExists(int id)
        {
            return _context.Guias.Any(e => e.GuiaId == id);
        }
        
    }
}
