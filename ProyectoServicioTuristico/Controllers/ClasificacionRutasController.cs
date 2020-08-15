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
    public class ClasificacionRutasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClasificacionRutasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClasificacionRutas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ClasificacionRutas.Include(c => c.Canton).Include(c => c.Provincia);
            return View(await applicationDbContext.ToListAsync());
        }

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
                .FirstOrDefaultAsync(m => m.ClasificacionRutaId == id);
            if (clasificacionRuta == null)
            {
                return NotFound();
            }

            return View(clasificacionRuta);
        }

        // GET: ClasificacionRutas/Create
        public IActionResult Create()
        {
            ViewData["CantonId"] = new SelectList(_context.Cantones, "CantonId", "Nombre");
            ViewData["ProvinciaId"] = new SelectList(_context.Provincias, "ProvinciaId", "ProvinciaId");
            return View();
        }

        // POST: ClasificacionRutas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClasificacionRutaId,Nombre,ProvinciaId,CantonId")] ClasificacionRuta clasificacionRuta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clasificacionRuta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CantonId"] = new SelectList(_context.Cantones, "CantonId", "Nombre", clasificacionRuta.CantonId);
            ViewData["ProvinciaId"] = new SelectList(_context.Provincias, "ProvinciaId", "ProvinciaId", clasificacionRuta.ProvinciaId);
            return View(clasificacionRuta);
        }

        // GET: ClasificacionRutas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clasificacionRuta = await _context.ClasificacionRutas.FindAsync(id);
            if (clasificacionRuta == null)
            {
                return NotFound();
            }
            ViewData["CantonId"] = new SelectList(_context.Cantones, "CantonId", "Nombre", clasificacionRuta.CantonId);
            ViewData["ProvinciaId"] = new SelectList(_context.Provincias, "ProvinciaId", "ProvinciaId", clasificacionRuta.ProvinciaId);
            return View(clasificacionRuta);
        }

        // POST: ClasificacionRutas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClasificacionRutaId,Nombre,ProvinciaId,CantonId")] ClasificacionRuta clasificacionRuta)
        {
            if (id != clasificacionRuta.ClasificacionRutaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clasificacionRuta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClasificacionRutaExists(clasificacionRuta.ClasificacionRutaId))
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
            ViewData["CantonId"] = new SelectList(_context.Cantones, "CantonId", "Nombre", clasificacionRuta.CantonId);
            ViewData["ProvinciaId"] = new SelectList(_context.Provincias, "ProvinciaId", "ProvinciaId", clasificacionRuta.ProvinciaId);
            return View(clasificacionRuta);
        }

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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clasificacionRuta = await _context.ClasificacionRutas.FindAsync(id);
            _context.ClasificacionRutas.Remove(clasificacionRuta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClasificacionRutaExists(int id)
        {
            return _context.ClasificacionRutas.Any(e => e.ClasificacionRutaId == id);
        }
    }
}
