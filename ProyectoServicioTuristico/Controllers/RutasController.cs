using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoServicioTuristico.Data;
using ProyectoServicioTuristico.Models;

namespace ProyectoServicioTuristico.Controllers
{
    public class RutasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RutasController(ApplicationDbContext context)
        {
            _context = context;
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

        // GET: Rutas/Create
        public IActionResult Create()
        {
            ViewData["ClasificacionRutaId"] = new SelectList(_context.ClasificacionRutas, "ClasificacionRutaId", "Nombre");
            ViewData["GuiaId"] = new SelectList(_context.Guias, "GuiaId", "ApellidoPaterno");
            return View();
        }

        // POST: Rutas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RutaId,Nombre,PuntoPartida,PuntoLLegada,Precio,DescripcionServicios,GuiaId,ClasificacionRutaId")] Ruta ruta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ruta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClasificacionRutaId"] = new SelectList(_context.ClasificacionRutas, "ClasificacionRutaId", "Nombre", ruta.ClasificacionRutaId);
            ViewData["GuiaId"] = new SelectList(_context.Guias, "GuiaId", "ApellidoPaterno", ruta.GuiaId);
            return View(ruta);
        }

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
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClasificacionRutaId"] = new SelectList(_context.ClasificacionRutas, "ClasificacionRutaId", "Nombre", ruta.ClasificacionRutaId);
            ViewData["GuiaId"] = new SelectList(_context.Guias, "GuiaId", "ApellidoPaterno", ruta.GuiaId);
            return View(ruta);
        }

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

        // POST: Rutas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ruta = await _context.Rutas.FindAsync(id);
            _context.Rutas.Remove(ruta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RutaExists(int id)
        {
            return _context.Rutas.Any(e => e.RutaId == id);
        }
    }
}
