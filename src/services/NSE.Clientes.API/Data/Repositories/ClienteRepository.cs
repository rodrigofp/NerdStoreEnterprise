using Microsoft.EntityFrameworkCore;
using NSE.Clientes.API.Models;
using NSE.Core.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSE.Clientes.API.Data.Repositories
{
	public class ClienteRepository : IClienteRepository
	{
		private readonly ClienteContext _context;

		public ClienteRepository(ClienteContext context)
		{
			_context = context;
		}

		public IUnitOfWork UnitOfWork => _context;

		public void Adicionar(Cliente cliente)
			=> _context.Clientes.Add(cliente);

		public async Task<Cliente> ObterPorCpf(string cpf)
			=> await _context.Clientes.FirstOrDefaultAsync(c => c.Cpf.Numero == cpf);

		public async Task<IEnumerable<Cliente>> ObterTodos() 
			=> await _context.Clientes.AsNoTracking().ToListAsync();

		public void Dispose() => _context?.Dispose();
	}
}