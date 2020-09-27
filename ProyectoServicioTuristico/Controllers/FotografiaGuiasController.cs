using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoServicioTuristico.Data;
using ProyectoServicioTuristico.Models;
using ProyectoServicioTuristico.ViewModels;

namespace ProyectoServicioTuristico.Controllers
{
    public class FotografiaGuiasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _hostEnvironment;
        public FotografiaGuiasController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _hostEnvironment = hostEnvironment;
        }
        
        // GET: FotografiaGuias
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.FotografiaGuias.Include(f => f.Guia);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: FotografiaGuias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotografiaGuia = await _context.FotografiaGuias
                .Include(f => f.Guia)
                .FirstOrDefaultAsync(m => m.FotografiaId == id);
            if (fotografiaGuia == null)
            {
                return NotFound();
            }

            return View(fotografiaGuia);
        }
        [Authorize(Roles = "Guia")]
        // GET: FotografiaGuias/Create
        public IActionResult Create()
        {
            ViewData["GuiaId"] = new SelectList(_context.Guias, "GuiaId", "Identidad");
            return View();
        }

        // POST: FotografiaGuias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Guia")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateFotoGuiaViewModel fotoGuiaView)
        {
            var fotografiaGuia = new FotografiaGuia();
            if (ModelState.IsValid)
            {
                var guiaId = await ObtenerGuiaId();
                // Guardar imagen en fisico en la ruta wwwrot/images
                string unicoNombreArchivo = UploadedFile(fotoGuiaView);
                    fotografiaGuia.DescripcionFoto = fotoGuiaView.DescripcionFoto;
                    fotografiaGuia.Fotos = unicoNombreArchivo;
                fotografiaGuia.GuiaId = guiaId;

                _context.Add(fotografiaGuia);
                await _context.SaveChangesAsync();
                return Redirect($"https://localhost:44334/Guias/Details/{guiaId}");
            }
            ViewData["GuiaId"] = new SelectList(_context.Guias, "GuiaId", "Identidad", fotografiaGuia.GuiaId);
            return View(fotoGuiaView);
        }

        private string UploadedFile(CreateFotoGuiaViewModel fotoGuiaView)
        {

            string unicoArchivo = null;

            if (fotoGuiaView.ArchivoFoto != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images/guias/book");
                unicoArchivo = Guid.NewGuid().ToString() + "_" + fotoGuiaView.ArchivoFoto.FileName;
                string filePath = Path.Combine(uploadsFolder, unicoArchivo);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    fotoGuiaView.ArchivoFoto.CopyTo(fileStream);
                }
            }
            return unicoArchivo;
        }

        private async Task<int> ObtenerGuiaId()
        {
            var user = await _userManager.GetUserAsync(User);
            var guia = await _context.Guias
                .FirstOrDefaultAsync(m => m.Identidad == user.Email);
            return guia.GuiaId;
        }
        [Authorize(Roles = "Guia")]
        // GET: FotografiaGuias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotografiaGuia = await _context.FotografiaGuias.FindAsync(id);
            if (fotografiaGuia == null)
            {
                return NotFound();
            }
            ViewData["GuiaId"] = new SelectList(_context.Guias, "GuiaId", "Identidad", fotografiaGuia.GuiaId);
            return View(fotografiaGuia);
        }

        // POST: FotografiaGuias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Guia")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FotografiaId,GuiaId,Fotos,DescripcionFoto")] FotografiaGuia fotografiaGuia)
        {
            if (id != fotografiaGuia.FotografiaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fotografiaGuia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FotografiaGuiaExists(fotografiaGuia.FotografiaId))
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
            ViewData["GuiaId"] = new SelectList(_context.Guias, "GuiaId", "Identidad", fotografiaGuia.GuiaId);
            return View(fotografiaGuia);
        }
        [Authorize(Roles = "Guia")]
        // GET: FotografiaGuias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotografiaGuia = await _context.FotografiaGuias
                .Include(f => f.Guia)
                .FirstOrDefaultAsync(m => m.FotografiaId == id);
            if (fotografiaGuia == null)
            {
                return NotFound();
            }

            return View(fotografiaGuia);
        }

        // POST: FotografiaGuias/Delete/5
        [Authorize(Roles = "Guia")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fotografiaGuia = await _context.FotografiaGuias.FindAsync(id);
            _context.FotografiaGuias.Remove(fotografiaGuia);
            //para borrar imagen
            var imagenPath = Path.Combine(_hostEnvironment.WebRootPath, "images/guias/book", fotografiaGuia.Fotos);
            if (System.IO.File.Exists(imagenPath))
                System.IO.File.Delete(imagenPath);

            await _context.SaveChangesAsync();
            return Redirect($"https://localhost:44334/Guias/Details/{fotografiaGuia.GuiaId}");
        }

        private bool FotografiaGuiaExists(int id)
        {
            return _context.FotografiaGuias.Any(e => e.FotografiaId == id);
        }
    }
}
