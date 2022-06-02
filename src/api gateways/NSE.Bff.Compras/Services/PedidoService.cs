﻿using Microsoft.Extensions.Options;
using NSE.Bff.Compras.Extensions;
using System;
using System.Net.Http;

namespace NSE.Bff.Compras.Services
{
	public interface IPedidoService { }

	public class PedidoService : Service, IPedidoService
	{
		private readonly HttpClient _httpClient;

		public PedidoService(HttpClient httpClient, IOptions<AppServiceSettings> settings)
		{
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri(settings.Value.PedidoUrl);
		}
	}
}