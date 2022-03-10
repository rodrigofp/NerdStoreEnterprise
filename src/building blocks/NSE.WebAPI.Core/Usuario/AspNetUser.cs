using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace NSE.WebAPI.Core.Usuario
{
	public class AspNetUser : IAspNetUser
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
}