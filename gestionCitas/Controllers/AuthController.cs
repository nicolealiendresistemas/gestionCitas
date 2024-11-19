using gestionCitas.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace gestionCitas.Controllers
{
    public class AuthController : Controller
    {
        private readonly GestioncitasContext _context;

        public AuthController(GestioncitasContext context)
        {
            _context = context;
        }

        // GET: Auth/Login
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Auth/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Usuario1,Password")] Usuario loginModel)
        {
            if (true)
            {
                var user = await _context.Usuarios
                    .Include(u => u.UsuarioRols)
                    .ThenInclude(ur => ur.Rol)
                    .FirstOrDefaultAsync(u => u.Usuario1 == loginModel.Usuario1 && u.Activo == true);

                if (user != null && VerifyPassword(loginModel.Password, user.Password))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Usuario1),
                        new Claim(ClaimTypes.Email, user.Email)
                    };

                    foreach (var userRole in user.UsuarioRols)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, userRole.Rol.Nombre));
                    }

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20),
                        IsPersistent = false,
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
            }
            return View(loginModel);
        }

        // GET: Auth/Register
        [AllowAnonymous]
        public IActionResult Register()
        {
            ViewBag.Roles = _context.Roles.ToList();
            return View();
        }

        // POST: Auth/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Usuario1,Password,Email")] Usuario usuario, int rolId)
        {
            if (ModelState.IsValid)
            {
                if (await _context.Usuarios.AnyAsync(u => u.Usuario1 == usuario.Usuario1 || u.Email == usuario.Email))
                {
                    ModelState.AddModelError("", "El usuario o email ya está en uso.");
                    ViewBag.Roles = _context.Roles.ToList();
                    return View(usuario);
                }

                usuario.Password = HashPassword(usuario.Password);
                usuario.Activo = true;

                _context.Add(usuario);
                await _context.SaveChangesAsync();

                var usuarioRol = new UsuarioRol
                {
                    UsuarioId = usuario.Id,
                    RolId = rolId,
                    FechaAsignacion = DateTime.Now
                };
                _context.Add(usuarioRol);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Login));
            }
            ViewBag.Roles = _context.Roles.ToList();
            return View(usuario);
        }

        // POST: Auth/Logout
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            var hashedEnteredPassword = HashPassword(enteredPassword);
            return storedHash == hashedEnteredPassword;
        }
    }
}
