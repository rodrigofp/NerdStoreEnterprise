using Microsoft.AspNetCore.Mvc;
using NSE.Catalogo.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSE.Catalogo.API.Controllers
{
	[ApiController]
	public class CatalogoController : Controller
	{
		private readonly IProdutoRepository _produtoRepository;

		public CatalogoController(IProdutoRepository produtoRepository)
		{
			_produtoRepository = produtoRepository;
		}

		[HttpGet("catalogo/produtos")]
		public async Task<IEnumerable<Produto>> Index() => await _produtoRepository.ObterTodos();

		[HttpGet("catalogo/produto/{id}")]
		public async Task<Produto> ProdutoDetalhe(Guid id) => await _produtoRepository.ObterPorId(id);
	}
}