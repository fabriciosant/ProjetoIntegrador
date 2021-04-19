using LoKMais.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Linq;
using System.Threading.Tasks;

namespace LoKMais.Controllers
{
    public class AdministracaoController : Controller
    {
        private readonly IToastNotification _toastNotification;
        private readonly UserManager<Cliente> _userManager;

        public AdministracaoController(IToastNotification toastNotification,
            UserManager<Cliente> userManager)
        {
            _toastNotification = toastNotification;
            _userManager = userManager;
        }
        #region Lista de Usuarios
        //[Authorize(Roles = "ADMINISTRADOR")]
        [HttpGet]
        public async Task<IActionResult> ListaDeUsuarios()
        {
            var listaUsuario = await _userManager.Users.Where(x => x.Email != "Fabriciosan47@gmail.com").ToListAsync();
            return View(listaUsuario);
        }
        #endregion

        #region DeletarUsuario
        [HttpPost]
        public async Task<IActionResult> DeletarUsuario(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);

            if (usuario == null)
            {
                _toastNotification.AddErrorToastMessage("Usuário nao encontrado!");
                return RedirectToAction("ListaDeUsuarios");
            }
            else
            {
                var result = await _userManager.DeleteAsync(usuario);
                if (result.Succeeded)
                {
                    _toastNotification.AddSuccessToastMessage("Usuário Excluido com sucesso!");
                    return RedirectToAction("ListaDeUsuarios");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return RedirectToAction("ListaDeUsuarios");
        }
        #endregion
    }
}
