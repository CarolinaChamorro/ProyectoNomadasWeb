using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoServicioTuristico.Data;
using ProyectoServicioTuristico.Models;
using ProyectoServicioTuristico.ViewModels;

namespace ProyectoServicioTuristico.Controllers
{
    public class FotografiaRutasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _hostEnvironment;
        public FotografiaRutasController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IWebHostEnvironment hostEnvironment )
        {
            _context = context;
            _userManager = userManager;
            _hostEnvironment = hostEnvironment;
        }

        // GET: FotografiaRutas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.FotografiaRutas.Include(f => f.Ruta);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: FotografiaRutas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotografiaRuta = await _context.FotografiaRutas
                .Include(f => f.Ruta)
                .FirstOrDefaultAsync(m => m.FotografiaId == id);
            if (fotografiaRuta == null)
            {
                return NotFound();
            }

            return View(fotografiaRuta);
        }
        [Authorize(Roles = "Guia")]
        // GET: FotografiaRutas/Create
        public IActionResult Create()
        {
            ViewData["RutaId"] = new SelectList(_context.Rutas, "RutaId", "DescripcionServicios");
            return View();
        }

        // POST: FotografiaRutas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Guia")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FotografiaViewModel fotografia)
        {
            if (ModelState.IsValid)
            {
                // Guardar imagen en fisico en la ruta wwwrot/images
                var rutaId = await ObtenerRutaId();
                string nombreFoto = UploadFile(fotografia);
                var id = await ObtenerGuiaId();
                FotografiaRuta fotografiaRuta = new FotografiaRuta
                {
                    Fotos = nombreFoto,
                    DescripcionFoto = fotografia.DescripcionFoto,
                    RutaId = rutaId,
                };
                _context.Add(fotografiaRuta);
                await _context.SaveChangesAsync();
                return Redirect($"https://localhost:44334/Guias/Details/{id}");
            }
            return View();
        }

        private async Task<int> ObtenerGuiaId()
        {
            var user = await _userManager.GetUserAsync(User);
            var guia = await _context.Guias
                .FirstOrDefaultAsync(m => m.Identidad == user.Email);
            return guia.GuiaId;
        }

        private async Task<int> ObtenerRutaId()
        {
            var user = await _userManager.GetUserAsync(User);
            var guia = await _context.Guias
                .FirstOrDefaultAsync(m => m.Identidad == user.Email);
            var ruta = await _context.Rutas
                .OrderByDescending(r => r.RutaId)
                .FirstOrDefaultAsync(m => m.GuiaId == guia.GuiaId);
            return ruta.RutaId;
        }
        private string UploadFile(FotografiaViewModel fotografia)
        {
            string nombreFotografia = null;
            if (fotografia.ArchivoFotoFiles != null && fotografia.ArchivoFotoFiles.Count > 0)
            {
                foreach (IFormFile photo in fotografia.ArchivoFotoFiles)
                {
                    string guardarCarpetaImagen = Path.Combine(_hostEnvironment.WebRootPath, "images/rutas");
                    nombreFotografia = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string rutaImagen = Path.Combine(guardarCarpetaImagen, nombreFotografia);
                    using (var fileStream = new FileStream(rutaImagen, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                }
            }
            return nombreFotografia;
        }
        [Authorize(Roles = "Guia")]
        // GET: FotografiaRutas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotografiaRuta = await _context.FotografiaRutas.FindAsync(id);
            if (fotografiaRuta == null)
            {
                return NotFound();
            }
            ViewData["RutaId"] = new SelectList(_context.Rutas, "RutaId", "DescripcionServicios", fotografiaRuta.RutaId);
            return View(fotografiaRuta);
        }

        // POST: FotografiaRutas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Guia")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FotografiaId,RutaId,Fotos,DescripcionFoto")] FotografiaRuta fotografiaRuta)
        {
            if (id != fotografiaRuta.FotografiaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fotografiaRuta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FotografiaRutaExists(fotografiaRuta.FotografiaId))
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
            ViewData["RutaId"] = new SelectList(_context.Rutas, "RutaId", "DescripcionServicios", fotografiaRuta.RutaId);
            return View(fotografiaRuta);
        }
        [Authorize(Roles = "Guia")]
        // GET: FotografiaRutas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotografiaRuta = await _context.FotografiaRutas
                .Include(f => f.Ruta)
                .Include(r => r.Ruta).ThenInclude(g => g.Guia)
                .FirstOrDefaultAsync(m => m.FotografiaId == id);
            if (fotografiaRuta == null)
            {
                return NotFound();
            }

            return View(fotografiaRuta);
        }

        // POST: FotografiaRutas/Delete/5
        [Authorize(Roles = "Guia")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fotografiaRuta = await _context.FotografiaRutas.FindAsync(id);
            var guia = await ObtenerGuiaId();
            //Borrado de la imagen en la carṕeta root/image
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images/rutas", fotografiaRuta.Fotos);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            _context.FotografiaRutas.Remove(fotografiaRuta);
            await _context.SaveChangesAsync();
            return Redirect($"https://localhost:44334/Guias/Details/{guia}");
        }

        private bool FotografiaRutaExists(int id)
        {
            return _context.FotografiaRutas.Any(e => e.FotografiaId == id);
        }
    }
}
