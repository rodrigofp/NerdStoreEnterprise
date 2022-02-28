using Microsoft.EntityFrameworkCore;
using NSE.Core.Data;
using System.Threading.Tasks;

namespace NSE.Cliente.API.Data
{
	public class ClienteContext : DbContext, IUnitOfWork
	{
		public ClienteContext(DbContextOptions<ClienteContext> options)
			: base(options) { }

		public async Task<bool> Commit() => await base.SaveChangesAsync() > 0;
	}
}
