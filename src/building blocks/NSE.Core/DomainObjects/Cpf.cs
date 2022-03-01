using NSE.Core.Utils;

namespace NSE.Core.DomainObjects
{
	public class Cpf
	{
		public const int CPF_MAX_LENGTH = 11;
		public string Numero { get; private set; }

		protected Cpf() { }

		public Cpf(string numero)
		{
			if (!Validar(numero)) throw new DomainException("CPF inválido");
			Numero = numero;
		}

		public static bool Validar(string cpf)
		{
			cpf = cpf.ApenasNumeros(cpf);

			if (cpf.Length > CPF_MAX_LENGTH)
				return false;

			while (cpf.Length != CPF_MAX_LENGTH)
				cpf = '0' + cpf;

			var igual = true;
			for (int i = 1; i < CPF_MAX_LENGTH && igual; i++)
				if (cpf[i] != cpf[0])
					igual = false;

			if (igual || cpf == "12345678909")
				return false;

			int[] numeros = new int[CPF_MAX_LENGTH];
			for (int i = 0; i < CPF_MAX_LENGTH; i++)
				numeros[i] = int.Parse(cpf[i].ToString());

			int soma = 0;
			for (int i = 0; i < 9; i++)
				soma += (10 - i) * numeros[i];

			int resultado = soma % CPF_MAX_LENGTH;
			if (resultado == 1 || resultado == 0)
			{
				if (numeros[9] != 0)
					return false;
			}
			else if (numeros[9] != 11 - resultado)
				return false;

			soma = 0;

			for (int i = 0; i < 10; i++)
				soma += (11 - i) * numeros[i];

			resultado = soma % CPF_MAX_LENGTH;

			if (resultado == 1 || resultado == 0)
			{
				if (numeros[10] != 0)
					return false;
			}
			else if (numeros[10] != CPF_MAX_LENGTH - resultado)
					return false;

			return true;
		}
	}
}