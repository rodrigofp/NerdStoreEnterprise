using Microsoft.EntityFrameworkCore;
using NSE.Core.Data;
using System.Threading.Tasks;

namespace NSE.Carrinho.API.Data
{
	public class CarrinhoContext : DbContext, IUnitOfWork
	{
		public CarrinhoContext(DbContextOptions<CarrinhoContext> options)
			: base(options)
		{ }

		public async Task<bool> Commit() => await SaveChangesAsync() > 0;
	}
}