using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services;
using System;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Controllers
{
	[Authorize]
	public class CarrinhoController : MainController
	{
		private readonly ICarrinhoService _carrinhoService;
		private readonly ICatalogoService _catalogoService;

		public CarrinhoController(ICarrinhoService carrinhoService,
			ICatalogoService catalogoService)
		{
			_carrinhoService = carrinhoService;
			_catalogoService = catalogoService;
		}


		[Route("carrinho")]
		public async Task<IActionResult> Index() 
			=> View(await _carrinhoService.ObterCarrinho());

		[HttpPost]
		[Route("carrinho/adicionar-item")]
		public async Task<IActionResult> AdicionarItemCarrinho(ItemProdutoViewModel itemProduto)
		{
			var produto = await _catalogoService.ObterPorId(itemProduto.ProdutoId);

			ValidarItemCarrinho(produto, itemProduto.Quantidade);

			if (!OperacaoValida())
				return View("Index", await _carrinhoService.ObterCarrinho());

			itemProduto.Nome = produto.Nome;
			itemProduto.Valor = produto.Valor;
			itemProduto.Imagem = produto.Imagem;

			var resposta = await _carrinhoService.AdicionarItemCarrinho(itemProduto);

			if (ResponsePossuiErros(resposta))
				return View("Index", await _carrinhoService.ObterCarrinho());

			return RedirectToAction("Index");
		}

		[HttpPost]
		[Route("carrinho/atualiza-item")]
		public async Task<IActionResult> AtualizarItemCarrinho(Guid produtoId, int quantidade)
		{
			var produto = await _catalogoService.ObterPorId(produtoId);

			ValidarItemCarrinho(produto, quantidade);

			if (!OperacaoValida())
				return View("Index", await _carrinhoService.ObterCarrinho());

			var itemProduto = new ItemProdutoViewModel
			{
				ProdutoId = produtoId,
				Quantidade = quantidade
			};

			var resposta = await _carrinhoService.AtualizarItemCarrinho(produtoId, itemProduto);

			if (ResponsePossuiErros(resposta))
				return View("Index", await _carrinhoService.ObterCarrinho());

			return RedirectToAction("Index");
		}

		[HttpPost]
		[Route("carrinho/remover-item")]
		public async Task<IActionResult> RemoverItemCarrinho(Guid produtoId)
		{
			var produto = await _catalogoService.ObterPorId(produtoId);

			if (produto == null)
			{
				AdicionaErroValidacao("Produto inexistente!");
				return View("Index", await _carrinhoService.ObterCarrinho());
			}

			var resposta = await _carrinhoService.RemoverItemCarrinho(produtoId);

			if (ResponsePossuiErros(resposta))
				return View("Index", await _carrinhoService.ObterCarrinho());

			return RedirectToAction("Index");
		}

		private void ValidarItemCarrinho(ProdutoViewModel produto, int quantidade)
		{
			if (produto == null) AdicionaErroValidacao("Produto inexistente");
			if (quantidade < 1) AdicionaErroValidacao($"Escolha ao menos uma unidade do produto {produto.Nome}");
			if (quantidade > produto.QuantidadeEstoque) AdicionaErroValidacao($"Não há estoque suficiente oara o produto {produto.Nome}");
		}
	}
}