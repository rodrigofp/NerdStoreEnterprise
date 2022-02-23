using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace NSE.WebApp.MVC.Extensions
{
	public interface IUser
	{
		string Name { get; }
		Guid ObterUserId();
		string ObterUserEmail();
		string ObterUserToken();
		bool EstaAutenticado();
		bool PossuiRole(string role);
		IEnumerable<Claim> ObterClaims();
		HttpContext ObterHttpContext();
	}

	public class AspNetUser : IUser
	{
		private readonly IHttpContextAccessor _accessor;

		public AspNetUser(IHttpContextAccessor httpContextAccessor)
		{
			_accessor = httpContextAccessor;
		}

		public string Name => _accessor.HttpContext.User.Identity.Name;

		public bool EstaAutenticado() => _accessor.HttpContext.User.Identity.IsAuthenticated;

		public IEnumerable<Claim> ObterClaims()
		{
			throw new NotImplementedException();
		}

		public HttpContext ObterHttpContext() => _accessor.HttpContext;

		public string ObterUserEmail() => EstaAutenticado()
			? _accessor.HttpContext.User.GetUserEmail()
			: string.Empty;

		public Guid ObterUserId() 
			=> EstaAutenticado() 
			? Guid.Parse(_accessor.HttpContext.User.GetUserId())
			: Guid.Empty;

		public string ObterUserToken() => EstaAutenticado()
			? _accessor.HttpContext.User.GetUserToken()
			: string.Empty;

		public bool PossuiRole(string role) => _accessor.HttpContext.User.IsInRole(role);
	}

	public static class ClaimsPrincipalExtensions
	{
		public static string GetUserId(this ClaimsPrincipal principal)
		{
			if (principal == null) throw new ArgumentException(nameof(principal));

			var claim = principal.FindFirst("sub");
			return claim?.Value;
		}

		public static string GetUserEmail(this ClaimsPrincipal principal)
		{
			if (principal == null) throw new ArgumentException(nameof(principal));

			var claim = principal.FindFirst("email");
			return claim?.Value;
		}

		public static string GetUserToken(this ClaimsPrincipal principal)
		{
			if (principal == null) throw new ArgumentException(nameof(principal));

			var claim = principal.FindFirst("JWT");
			return claim?.Value;
		}
	}
}