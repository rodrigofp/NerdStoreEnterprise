using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Controllers
{
	public class IdentidadeController : MainController
	{
		private readonly IAutenticacaoService _autenticacaoService;

		public IdentidadeController(IAutenticacaoService autenticacaoService)
		{
			_autenticacaoService = autenticacaoService;
		}

		[HttpGet]
		[Route("nova-conta")]
		public IActionResult Registro()
		{
			return View();
		}

		[HttpPost]
		[Route("nova-conta")]
		public async Task<IActionResult> Registro(UsuarioRegistro usuarioRegistro)
		{
			if (!ModelState.IsValid) return View(usuarioRegistro);

			var resposta = await _autenticacaoService.Registro(usuarioRegistro);

			if (ResponsePossuiErros(resposta.ResponseResult)) return View(usuarioRegistro);

			await RealizarLogin(resposta);

			return RedirectToAction("index", "Catalogo");
		}

		[HttpGet]
		[Route("login")]
		public IActionResult Login(string returnUrl = null)
		{
			ViewData["ReturnUrl"] = returnUrl;
			return View();
		}

		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login(UsuarioLogin usuarioLogin, string returnUrl = null)
		{
			if (!ModelState.IsValid) return View(usuarioLogin);

			//API - Login
			var resposta = await _autenticacaoService.Login(usuarioLogin);

			if (ResponsePossuiErros(resposta.ResponseResult)) return View(usuarioLogin);

			// Realizar login na APP
			await RealizarLogin(resposta);

			if (string.IsNullOrEmpty(returnUrl)) return RedirectToAction("index", "Catalogo");

			return LocalRedirect(returnUrl);
		}

		[HttpGet]
		[Route("sair")]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("index", "Catalogo");
		}

		#region Métodos

		private async Task RealizarLogin(UsuarioRespostaLogin resposta)
		{
			var token = ObterTokenFormatado(resposta.AccessToken);

			var claims = new List<Claim>();
			claims.Add(new Claim("JWT", resposta.AccessToken));
			claims.AddRange(token.Claims);

			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

			var authProprieties = new AuthenticationProperties
			{
				ExpiresUtc = System.DateTimeOffset.UtcNow.AddMinutes(60),
				IsPersistent = true
			};

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(claimsIdentity),
				authProprieties);
		}

		private static JwtSecurityToken ObterTokenFormatado(string jwtToken) => new JwtSecurityTokenHandler().ReadJwtToken(jwtToken) as JwtSecurityToken;

		#endregion
	}
}