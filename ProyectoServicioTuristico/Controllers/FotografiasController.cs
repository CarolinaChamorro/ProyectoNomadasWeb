﻿using System;
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
    public class FotografiasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FotografiasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Fotografias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fotografias.ToListAsync());
        }

        // GET: Fotografias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotografia = await _context.Fotografias
                .FirstOrDefaultAsync(m => m.FotografiaId == id);
            if (fotografia == null)
            {
                return NotFound();
            }

            return View(fotografia);
        }

        // GET: Fotografias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fotografias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FotografiaId,Fotos")] Fotografia fotografia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fotografia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fotografia);
        }

        // GET: Fotografias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotografia = await _context.Fotografias.FindAsync(id);
            if (fotografia == null)
            {
                return NotFound();
            }
            return View(fotografia);
        }

        // POST: Fotografias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FotografiaId,Fotos")] Fotografia fotografia)
        {
            if (id != fotografia.FotografiaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fotografia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FotografiaExists(fotografia.FotografiaId))
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
            return View(fotografia);
        }

        // GET: Fotografias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotografia = await _context.Fotografias
                .FirstOrDefaultAsync(m => m.FotografiaId == id);
            if (fotografia == null)
            {
                return NotFound();
            }

            return View(fotografia);
        }

        // POST: Fotografias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fotografia = await _context.Fotografias.FindAsync(id);
            _context.Fotografias.Remove(fotografia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FotografiaExists(int id)
        {
            return _context.Fotografias.Any(e => e.FotografiaId == id);
        }
    }
}