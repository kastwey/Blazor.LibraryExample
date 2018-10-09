namespace Blazor.LibraryExample.Server.Data.EntityConfiguration
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Blazor.LibraryExample.Shared.Entities;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	/// <summary>
	/// Class to configure the Book entity
	/// </summary>
	public class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
	{
		/// <summary>
		/// Configures the specified book.
		/// </summary>
		/// <param name="builder">The book.</param>
		public void Configure(EntityTypeBuilder<Book> builder)
		{
			builder.Property(p => p.Id).ValueGeneratedOnAdd();
			builder.Property(p => p.CreationDate).HasColumnType("date");
			builder.HasIndex(p => p.Title);
			builder.HasIndex(p => p.Author);
		}
	}
}
