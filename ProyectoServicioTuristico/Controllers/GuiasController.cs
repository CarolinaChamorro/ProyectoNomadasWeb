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
        [Authorize(Roles = "Administrador")]
        // GET: Guias
        public async Task<IActionResult> Index()
        {

            return View(await _context.Guias.ToListAsync());
        }
        [Authorize(Roles ="Administrador")]
        public async Task<IActionResult> PerfilGuia(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var perfilGuia = await _context.Guias
                .Include(g => g.Rutas)
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
        [Authorize(Roles = "Guia")]
        // GET: Vista-Externa-Guias
        public async Task<IActionResult> Vista()
        {

            return View(await _context.Guias.ToListAsync());
        }
        [AllowAnonymous]
        // GET: Guias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var guia = await _context.Guias
                .Include(g => g.Rutas)
                .Include(g => g.Rutas).ThenInclude(r => r.FotografiaRutas)
                .Include(g => g.Rutas).ThenInclude(r => r.ClasificacionRuta)
                .Include(g => g.DetalleGuiaIdiomas).ThenInclude(i => i.Idioma)
                .Include(g=>g.FotografiaGuias)
                .FirstOrDefaultAsync(m => m.GuiaId == id);
            if (guia == null)
            {
                return NotFound();
            }

            return View(guia);
        }
        [Authorize(Roles = "Guia")]
        // GET: Guias/Create
        public IActionResult Create()
        {
            var guia = new Guia();
            guia.DetalleGuiaIdiomas = new List<DetalleGuiaIdioma>();
            ListaAsignadoIdiomaData(guia);
            return View();
        }

        // POST: Guias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Guia")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGuiaViewModel guiamodel, string[] selectedIdiomas)
        {
            var guia = new Guia();
            if (selectedIdiomas != null)
            {
                guia.DetalleGuiaIdiomas = new List<DetalleGuiaIdioma>();
                foreach (var idioma in selectedIdiomas)
                {
                    var idiomaToAdd = new DetalleGuiaIdioma { GuiaId = guia.GuiaId,IdiomaId = int.Parse(idioma) };
                    guia.DetalleGuiaIdiomas.Add(idiomaToAdd);
                }
            }
            string unicoNombreArchivo = UploadedFile(guiamodel);
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                guia.PrimerNombre = guiamodel.PrimerNombre;
                guia.SegundoNombre = guiamodel.SegundoNombre;
                guia.ApellidoPaterno = guiamodel.ApellidoPaterno;
                guia.ApellidoMaterno = guiamodel.ApellidoMaterno;
                guia.NombreCompleto = guiamodel.PrimerNombre + " " + guiamodel.ApellidoPaterno;
                guia.Edad = guiamodel.Edad;
                guia.Sexo = guiamodel.Sexo;
                guia.Telefono = guiamodel.Telefono;
                guia.FotoPerfil = unicoNombreArchivo;
                guia.Identidad = await _userManager.GetUserNameAsync(user);

                _context.Add(guia);
                await _context.SaveChangesAsync();
                return Redirect($"https://localhost:44334/Guias/Details/{guia.GuiaId}");
            }

            ListaAsignadoIdiomaData(guia);
            return View(guia);
        }
        //Subir foto del guia en createguiaviewmodel
        private string UploadedFile(CreateGuiaViewModel guiamodel)
        {
            string unicoArchivo = null;

            if (guiamodel.ArchivoImagen != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/guias");
                unicoArchivo = Guid.NewGuid().ToString() + "_" + guiamodel.ArchivoImagen.FileName;
                string filePath = Path.Combine(uploadsFolder, unicoArchivo);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    guiamodel.ArchivoImagen.CopyTo(fileStream);
                }
            }
            return unicoArchivo;
        }
       [Authorize(Roles = "Guia")]
        // GET: Guias/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var guia = await _context.Guias
                .Include(i => i.DetalleGuiaIdiomas)
                    .ThenInclude(y => y.Idioma)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.GuiaId == id);
            if (guia == null)
            {
                return NotFound();
            }
            var editmodel = new EditGuiaViewModel
            {
                GuiaIdEdit = guia.GuiaId,
                PrimerNombre = guia.PrimerNombre,
                SegundoNombre = guia.SegundoNombre,
                ApellidoPaterno = guia.ApellidoPaterno,
                ApellidoMaterno = guia.ApellidoMaterno,
                Edad = guia.Edad,
                Sexo = guia.Sexo,
                Telefono = guia.Telefono,
                ExistingPhotoPath = guia.FotoPerfil
            };
            ListaAsignadoIdiomaData(guia);
            return View(editmodel);
        }

        private void ListaAsignadoIdiomaData(Guia guia)
        {
            var todoIdiomas = _context.Idiomas;
            var guiaIdiomas = new HashSet<int>(guia.DetalleGuiaIdiomas
                .Select(c => c.IdiomaId));
            var viewAsignado = new List<AsignacionIdiomaData>();
            foreach (var idioma in todoIdiomas)
            {
                viewAsignado.Add(new AsignacionIdiomaData
                {
                    IdiomaID = idioma.IdiomaId,
                    Nombre = idioma.Nombre,
                    Asignado = guiaIdiomas.Contains(idioma.IdiomaId)
                });
            }
            ViewData["Idiomas"] = viewAsignado;
        }

        // POST: Guias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Guia")]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, EditGuiaViewModel editmodel, string[] selectedIdiomas)
        {
            if (id == null)
            {
                return NotFound();
            }
            var guiaToUpdate = await _context.Guias
                .Include(i => i.DetalleGuiaIdiomas)
                   .ThenInclude(i => i.Idioma)
                .FirstOrDefaultAsync(m => m.GuiaId == editmodel.GuiaIdEdit);

            if (ModelState.IsValid)
            {
                var guia = await _context.Guias.FirstOrDefaultAsync(m => m.GuiaId == editmodel.GuiaIdEdit);
                guia.PrimerNombre = editmodel.PrimerNombre;
                guia.SegundoNombre = editmodel.SegundoNombre;
                guia.ApellidoPaterno = editmodel.ApellidoPaterno;
                guia.ApellidoMaterno = editmodel.ApellidoMaterno;
                guia.NombreCompleto = editmodel.PrimerNombre + " " + editmodel.SegundoNombre;
                guia.Edad = editmodel.Edad;
                guia.Sexo = editmodel.Sexo;
                guia.Telefono = editmodel.Telefono;
                if (editmodel.ArchivoImagen != null)
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images/guias",
                        guia.FotoPerfil);
                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);

                    guia.FotoPerfil = UploadedFile(editmodel);
                }
                if (await TryUpdateModelAsync<Guia>(
                    guiaToUpdate,
                    "",
                    i => i.PrimerNombre,
                    i => i.SegundoNombre,
                    i => i.ApellidoPaterno,
                    i => i.ApellidoMaterno,
                    i => i.NombreCompleto,
                    i => i.Edad,
                    i => i.Sexo,
                    i => i.Telefono,
                    i => i.FotoPerfil))
                {

                    UpdateGuiaIdiomas(selectedIdiomas, guiaToUpdate);
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateException /* ex */)
                    {
                        //Log the error (uncomment ex variable name and write a log.)
                        ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists, " +
                            "see your system administrator.");
                    }
                }
                _context.Update(guia);
                _context.SaveChanges();
                return RedirectToAction(nameof(PerfilGuia));
            }
            UpdateGuiaIdiomas(selectedIdiomas, guiaToUpdate);
            ListaAsignadoIdiomaData(guiaToUpdate);
            return View(editmodel);
        }

        private void UpdateGuiaIdiomas(string[] selectedIdiomas, Guia guiaToUpdate)
        {
            if (selectedIdiomas == null)
            {
                guiaToUpdate.DetalleGuiaIdiomas = new List<DetalleGuiaIdioma>();
                return;
            }
            var selectedIdiomasHS = new HashSet<string>(selectedIdiomas);
            var guiaIdiomas = new HashSet<int>
                (guiaToUpdate.DetalleGuiaIdiomas.Select(c => c.Idioma.IdiomaId));
            foreach (var idioma in _context.Idiomas)
            {
                if (selectedIdiomasHS.Contains(idioma.IdiomaId.ToString()))
                {
                    if (!guiaIdiomas.Contains(idioma.IdiomaId))
                    {
                        guiaToUpdate.DetalleGuiaIdiomas.Add(new DetalleGuiaIdioma { GuiaId = guiaToUpdate.GuiaId, IdiomaId = idioma.IdiomaId });
                    }
                }
                else
                {
                    if (guiaIdiomas.Contains(idioma.IdiomaId))
                    {
                        DetalleGuiaIdioma idiomaToRemove = guiaToUpdate.DetalleGuiaIdiomas.FirstOrDefault(i => i.IdiomaId == idioma.IdiomaId);
                        _context.Remove(idiomaToRemove);
                    }
                }

            }
        }
        [Authorize(Roles ="Administrador")]
        // GET: Guias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guia = await _context.Guias
                .Include(i => i.DetalleGuiaIdiomas)
                    .ThenInclude(y => y.Idioma)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.GuiaId == id);

            if (guia == null)
            {
                return NotFound();
            }

            return View(guia);
        }

        // POST: Guias/Delete/5
        [Authorize(Roles = "Administrador")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guia = await _context.Guias
                         .Include(i => i.DetalleGuiaIdiomas)
                         .FirstOrDefaultAsync(i => i.GuiaId == id);
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
