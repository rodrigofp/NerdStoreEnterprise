using FluentValidation.Results;
using NSE.Core.Data;
using System.Threading.Tasks;

namespace NSE.Core.Messages
{
	public abstract class CommandHandler
	{
		protected ValidationResult ValidationResult;

		public CommandHandler()
		{
			ValidationResult = new ValidationResult();
		}

		protected void AdicionarErro(string messagem)
		{
			ValidationResult.Errors.Add(new ValidationFailure(string.Empty, messagem));
		}

		protected async Task<ValidationResult> PersistirDados(IUnitOfWork uow)
		{
			if (!await uow.Commit()) AdicionarErro("Houve um erro ao persistir os dados");

			return ValidationResult;
		}
	}
}