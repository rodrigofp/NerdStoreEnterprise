using System.Text.RegularExpressions;

namespace NSE.Core.DomainObjects
{
	public class Email
	{
		public const int ENDERECO_MAX_LENGTH = 254;
		public const int ENDERECO_MIN_LENGTH = 5;

		public string Endereco { get; private set; }

		public Email() { }

		public Email(string endereco)
		{
			if (!Validar(endereco)) throw new DomainException("E-mail inválido");
			Endereco = endereco;
		}

		public static bool Validar(string email)
		{
			if (email.Length > ENDERECO_MAX_LENGTH || email.Length < ENDERECO_MIN_LENGTH || string.IsNullOrWhiteSpace(email))
				return false;

			var regexEmail = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
			return regexEmail.IsMatch(email);
		}
	}
}