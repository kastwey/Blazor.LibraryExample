namespace Blazor.LibraryExample.ServerSideRendering.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Blazor.LibraryExample.ServerSideRendering.Contracts.Services;
	using Blazor.LibraryExample.Shared.Entities;

	/// <summary>
	/// Class to implement book services.
	/// </summary>
	public class BookService : IBookService
	{
		/// <summary>
		/// Validates the specified book.
		/// </summary>
		/// <param name="book">The book.</param>
		/// <returns>All errors found.</returns>
		public List<string> Validate(Book book)
		{
			List<string> errors = new List<string>();
			if (string.IsNullOrWhiteSpace(book.Title))
			{
				errors.Add("El título es obligatorio.");
			}
			else if (book.Title.Length > 100)
			{
				errors.Add("El título del libro no puede contener más de 100 caracteres.");
			}

			if (string.IsNullOrWhiteSpace(book.Author))
			{
				errors.Add("El autor del libro es obligatorio.");
			}
			else if (book.Author.Length > 100)
			{
				errors.Add("El autor del libro no puede contener más de 100 caracteres.");
			}

			return errors;
		}
	}
}
