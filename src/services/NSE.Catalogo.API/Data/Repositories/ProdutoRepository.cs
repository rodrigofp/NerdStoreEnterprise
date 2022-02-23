using Microsoft.EntityFrameworkCore;
using NSE.Catalogo.API.Models;
using NSE.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSE.Catalogo.API.Data.Repositories
{
	public class ProdutoRepository : IProdutoRepository
	{
		private readonly CatalogoContext _context;
		public IUnitOfWork UnitOfWork => _context;

		public ProdutoRepository(CatalogoContext context)
		{
			_context = context;
		}

		#region Query

		public async Task<Produto> ObterPorId(Guid id) => await _context.Produtos.FindAsync(id);

		public async Task<IEnumerable<Produto>> ObterTodos() => await _context.Produtos.AsNoTracking().ToListAsync();

		#endregion

		#region Command

		public void Adicionar(Produto produto) => _context.Produtos.Add(produto);

		public void Atualizar(Produto produto) => _context.Produtos.Update(produto);

		#endregion

		public void Dispose() => _context?.Dispose();
	}
}