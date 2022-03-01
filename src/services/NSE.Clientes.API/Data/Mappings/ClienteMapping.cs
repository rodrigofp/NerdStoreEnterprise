﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NSE.Clientes.API.Models;
using NSE.Core.DomainObjects;

namespace NSE.Clientes.API.Data.Mappings
{
	public class ClienteMapping : IEntityTypeConfiguration<Cliente>
	{
		public void Configure(EntityTypeBuilder<Cliente> builder)
		{
			builder.HasKey(c => c.Id);

			builder.Property(c => c.Nome)
				.IsRequired()
				.HasColumnType("varchar(200)");

			builder.OwnsOne(c => c.Cpf, tf =>
			{
				tf.Property(c => c.Numero)
					.IsRequired()
					.HasMaxLength(Cpf.CPF_MAX_LENGTH)
					.HasColumnName("Cpf")
					.HasColumnType($"varchar({Cpf.CPF_MAX_LENGTH})");
			});

			builder.OwnsOne(c => c.Email, tf =>
			{
				tf.Property(c => c.Endereco)
					.IsRequired()
					.HasColumnName("Email")
					.HasColumnType($"varchar({Email.ENDERECO_MAX_LENGTH})");
			});

			// 1 : 1 => Cliente : Endereco
			builder.HasOne(c => c.Endereco)
				.WithOne(c => c.Cliente);

			builder.ToTable("Clientes");
		}
	}
}
