using LoKMais.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoKMais.Controllers
{
    public class AutenticadorController : Controller
    {
        private readonly IToastNotification _toastNotification;
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ILogger<AutenticadorController> _logger;

        public AutenticadorController(IToastNotification toastNotification,
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            ILogger<AutenticadorController> logger)
        {
            _logger = logger;
            _signInManager = signInManager;
            _toastNotification = toastNotification;
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AcessoNegado() => View();
    }
}
