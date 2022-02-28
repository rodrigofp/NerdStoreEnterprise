using NSE.Core.DomainObjects;

namespace NSE.Cliente.API.Models
{
	public class Cliente : Entity, IAggregateRoot
	{
		public string Nome { get; private set; }
		public string Email { get; private set; }
		public string Cpf { get; private set; }
		public bool Excluido { get; private set; }
		public Endereco Endereco { get; private set; }

		public Cliente(string nome, string email, string cpf)
		{
			Nome = nome;
			Email = email;
			Cpf = cpf;
			Excluido = false;
		}
	}

	public class Endereco : Entity
	{
		public string Logradouro { get; private set; }
		public string Numero { get; private set; }
		public string Complemento { get; private set; }
		public string Bairro { get; private set; }
		public string Cep { get; private set; }
		public string Cidade { get; private set; }
		public string Estado { get; private set; }

		public Endereco(string logradouro, string numero, string complemento, string bairro, string cep, string cidade, string estado)
		{
			Logradouro = logradouro;
			Numero = numero;
			Complemento = complemento;
			Bairro = bairro;
			Cep = cep;
			Cidade = cidade;
			Estado = estado;
		}
	}
}