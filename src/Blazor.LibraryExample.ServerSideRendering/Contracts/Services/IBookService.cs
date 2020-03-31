namespace Blazor.LibraryExample.ServerSideRendering.Contracts.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Blazor.LibraryExample.Shared.Entities;

	/// <summary>
	/// interface to abstract several book services
	/// </summary>
	public interface IBookService
	{
		/// <summary>
		/// Validates the specified book.
		/// </summary>
		/// <param name="book">The book.</param>
		/// <returns>A list with all errors found</returns>
		List<string> Validate(Book book);
	}
}