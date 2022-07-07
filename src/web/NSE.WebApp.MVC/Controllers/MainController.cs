﻿using Microsoft.AspNetCore.Mvc;
using NSE.Core.Communication;
using NSE.WebApp.MVC.Models;
using System.Linq;

namespace NSE.WebApp.MVC.Controllers
{
	public class MainController : Controller
	{
		protected bool ResponsePossuiErros(ResponseResult resposta)
		{
			if (resposta != null && resposta.Errors.Mensagens.Any())
			{
				foreach (var mensagem in resposta.Errors.Mensagens)
				{
					ModelState.AddModelError(string.Empty, mensagem);
				}
				return true;
			}

			return false;
		}

		protected void AdicionaErroValidacao(string mensagem)
			=> ModelState.AddModelError(string.Empty, mensagem);

		protected bool OperacaoValida()
			=> ModelState.ErrorCount == 0;
	}
}