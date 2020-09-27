using System;
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
    public class RutasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _hostEnvironment;
        public RutasController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _hostEnvironment = hostEnvironment;

        }

        // GET: Rutas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Rutas.Include(r => r.ClasificacionRuta).Include(r => r.Guia);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Rutas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ruta = await _context.Rutas
                .Include(r => r.ClasificacionRuta)
                .Include(r => r.Guia)
                .FirstOrDefaultAsync(m => m.RutaId == id);
            if (ruta == null)
            {
                return NotFound();
            }

            return View(ruta);
        }
        [Authorize(Roles = "Guia")]
        // GET: Rutas/Create
        public IActionResult Create()
        {
            ViewData["ClasificacionRutaId"] = new SelectList(_context.ClasificacionRutas, "ClasificacionRutaId", "Nombre");
            return View();
        }

        // POST: Rutas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Guia")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ruta rutamodel)
        {
            if (ModelState.IsValid)
            {
                var guiaId = await ObtenerGuiaId();
                Ruta ruta = new Ruta
                {
                    Nombre = rutamodel.Nombre,
                    PuntoPartida=rutamodel.PuntoPartida,
                    PuntoLLegada=rutamodel.PuntoLLegada,
                    Precio=rutamodel.Precio,
                    DescripcionServicios=rutamodel.DescripcionServicios,
                    GuiaId=guiaId,
                    ClasificacionRutaId=rutamodel.ClasificacionRutaId
                };
                
                
                _context.Add(ruta);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "FotografiaRutas");
            }
            ViewData["ClasificacionRutaId"] = new SelectList(_context.ClasificacionRutas, "ClasificacionRutaId", "Nombre", rutamodel.ClasificacionRutaId);
            //ViewData["GuiaId"] = new SelectList(_context.Guias, "GuiaId", "Identidad", rutamodel.GuiaId);
            return View();
        }

        private async Task<int> ObtenerGuiaId()
        {
            var user = await _userManager.GetUserAsync(User);
            var guia = await _context.Guias
                .FirstOrDefaultAsync(m => m.Identidad == user.Email);
            return guia.GuiaId;
        }
        [Authorize(Roles = "Guia")]
        // GET: Rutas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ruta = await _context.Rutas.FindAsync(id);
            if (ruta == null)
            {
                return NotFound();
            }
            ViewData["ClasificacionRutaId"] = new SelectList(_context.ClasificacionRutas, "ClasificacionRutaId", "Nombre", ruta.ClasificacionRutaId);
            ViewData["GuiaId"] = new SelectList(_context.Guias, "GuiaId", "ApellidoPaterno", ruta.GuiaId);
            return View(ruta);
        }

        // POST: Rutas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Guia")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RutaId,Nombre,PuntoPartida,PuntoLLegada,Precio,DescripcionServicios,GuiaId,ClasificacionRutaId")] Ruta ruta)
        {
            if (id != ruta.RutaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ruta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RutaExists(ruta.RutaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect($"https://localhost:44334/Guias/Details/{ruta.GuiaId}");
            }
            ViewData["ClasificacionRutaId"] = new SelectList(_context.ClasificacionRutas, "ClasificacionRutaId", "Nombre", ruta.ClasificacionRutaId);
            ViewData["GuiaId"] = new SelectList(_context.Guias, "GuiaId", "ApellidoPaterno", ruta.GuiaId);
            return View(ruta);
        }
        [Authorize(Roles = "Guia")]
        // GET: Rutas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ruta = await _context.Rutas
                .Include(r => r.ClasificacionRuta)
                .Include(r => r.Guia)
                .FirstOrDefaultAsync(m => m.RutaId == id);
            if (ruta == null)
            {
                return NotFound();
            }

            return View(ruta);
        }
        [Authorize(Roles = "Guia")]
        // POST: Rutas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ruta = await _context.Rutas.FindAsync(id);
            _context.Rutas.Remove(ruta);
            await _context.SaveChangesAsync();
            return Redirect($"https://localhost:44334/Guias/Details/{ruta.GuiaId}");
        }

        private bool RutaExists(int id)
        {
            return _context.Rutas.Any(e => e.RutaId == id);
        }
    }
}