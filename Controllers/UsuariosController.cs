using CrudNet6.Datos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CrudNet6.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        //debemos utilizar los objetos de Identity que configuramos en program.
        public UsuariosController(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        // para que cualquier usuario este autenticado o no
        // pueda invocar esta acción. colocamos
        // para eso agregamos el using correspondiente
        // Microsoft.AspNetCore.Authorization;
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Registro()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registro(RegistroViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }
            var usuario = new IdentityUser()
            {
                Email = modelo.Email,
                UserName = modelo.Email
            };
            var resultado = await userManager.CreateAsync(usuario, password:
           modelo.Password);
            if (resultado.Succeeded)
            {
                await signInManager.SignInAsync(usuario, isPersistent: true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(modelo);
            }
        }

        // agregamos para login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }
            //lockouOnfailure si el usuario se equivoca varias veces
            // colocando el password podemos cerrar la cuenta
            var resultado = await signInManager.PasswordSignInAsync(modelo.Email,
            modelo.Password, modelo.Recuerdame, lockoutOnFailure: false);
            if (resultado.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Nombre o Passwork incorrectos ");
                return View(modelo);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Login", "Usuarios");
        }
    }
}
