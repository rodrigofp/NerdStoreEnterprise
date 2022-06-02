using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Extensions
{
	public class CarrinhoViewComponent : ViewComponent
	{
		private readonly ICarrinhoService _carrinhoService;

		public CarrinhoViewComponent(ICarrinhoService carrinhoService)
		{
			_carrinhoService = carrinhoService;
		}

		public async Task<IViewComponentResult> InvokeAsync() 
			=> View(await _carrinhoService.ObterCarrinho() ?? new CarrinhoViewModel());
	}
}