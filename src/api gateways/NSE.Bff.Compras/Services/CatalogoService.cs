﻿using Microsoft.Extensions.Options;
using NSE.Bff.Compras.Extensions;
using NSE.Bff.Compras.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NSE.Bff.Compras.Services
{
	public interface ICatalogoService 
	{
		Task<ItemProdutoDTO> ObterPorId(Guid Id);
	}

	public class CatalogoService : Service, ICatalogoService
	{
		private readonly HttpClient _httpClient;

		public CatalogoService(HttpClient httpClient, IOptions<AppServiceSettings> settings)
		{
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri(settings.Value.CatalogoUrl);
		}

		public async Task<ItemProdutoDTO> ObterPorId(Guid id)
		{
			var response = await _httpClient.GetAsync($"/catalogo/produtos/{id}");

			TratarErrosResponse(response);

			return await DeserializarObjetoResponse<ItemProdutoDTO>(response);
		}
	}
}
