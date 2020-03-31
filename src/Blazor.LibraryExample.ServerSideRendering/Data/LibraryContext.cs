namespace Blazor.LibraryExample.ServerSideRendering.Data
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Blazor.LibraryExample.ServerSideRendering.Data.EntityConfiguration;
	using Blazor.LibraryExample.Shared.Entities;
	using Microsoft.EntityFrameworkCore;

	/// <summary>
	/// The library context to interact with the database.
	/// </summary>
	/// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
	public class LibraryContext : DbContext
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LibraryContext"/> class.
		/// </summary>
		/// <param name="options">The options.</param>
		public LibraryContext(DbContextOptions<LibraryContext> options)
			: base(options)
		{
		}

		/// <summary>
		/// Gets or sets the books.
		/// </summary>
		/// <value>
		/// The books.
		/// </value>
		public DbSet<Book> Books { get; set; }

		/// <summary>
		/// Override this method to further configure the model that was discovered by convention from the entity types
		/// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
		/// and re-used for subsequent instances of your derived context.
		/// </summary>
		/// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
		/// define extension methods on this object that allow you to configure aspects of the model that are specific
		/// to a given database.</param>
		/// <remarks>
		/// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
		/// then this method will not be run.
		/// </remarks>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new BookEntityTypeConfiguration());
			base.OnModelCreating(modelBuilder);
		}
	}
}
