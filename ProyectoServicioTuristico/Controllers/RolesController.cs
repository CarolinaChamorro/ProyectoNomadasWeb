using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProyectoServicioTuristico.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ProyectoServicioTuristico.Controllers
{
    //[Authorize(Roles ="Administrador,Usuario")]
    public class RolesController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<IdentityUser> userManager;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userMgr)
        {
            this.roleManager = roleManager;
            this.userManager = userMgr;
        }
        public ViewResult Index() => View(roleManager.Roles);
        public IActionResult Create() => View();
        [Authorize(Roles = "Administrador")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([Required] string nombre)
        {
            if (ModelState.IsValid)
            {
                IdentityResult resultado = await roleManager.CreateAsync(new IdentityRole(nombre));
                if (resultado.Succeeded)
                    return RedirectToAction("Index");
                Errores(resultado);
            }
            return View();
        }

        private void Errores(IdentityResult resultado)
        {
            foreach (var error in resultado.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole rolPorBorrar = await roleManager.FindByIdAsync(id);
            if (rolPorBorrar != null)
            {
                IdentityResult resultado = await roleManager.DeleteAsync(rolPorBorrar);

                if (resultado.Succeeded)
                    return RedirectToAction("Index");
                Errores(resultado);
            }
            else
            {
                ModelState.AddModelError("","No hay el rol");
            }
            return View("Index",roleManager.Roles);
        }

        public async Task<IActionResult> Update(string id)
        {
            IdentityRole rol = await roleManager.FindByIdAsync(id);
            List<IdentityUser> miembros = new List<IdentityUser>();
            List<IdentityUser> noMiembros = new List<IdentityUser>();
            foreach(IdentityUser usuario in userManager.Users)
            {
                var lista = await userManager.IsInRoleAsync(usuario, rol.Name) ? miembros : noMiembros;
                lista.Add(usuario);
            }
            var modelo = new EdicionRol
            {
                Rol = rol,
                Miembros = miembros,
                NoMiembros = noMiembros
            };
            return View(modelo);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ModificacionRol modificacion)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in modificacion.AumentarIds ?? new string[] { })
                {
                    IdentityUser user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.AddToRoleAsync(user, modificacion.RoleName);
                        if (!result.Succeeded)
                            Errores(result);
                    }
                }
                foreach (string userId in modificacion.BorrarIds ?? new string[] { })
                {
                    IdentityUser user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.RemoveFromRoleAsync(user, modificacion.RoleName);
                        if (!result.Succeeded)
                            Errores(result);
                    }
                }
            }
            if (ModelState.IsValid)
                return RedirectToAction(nameof(Index));
            else
                return await Update(modificacion.RoleId);
        }
    }
}
