using Blazor.LibraryExample.ServerSideRendering.Dtos;
using Blazor.LibraryExample.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.LibraryExample.ServerSideRendering.Contracts.Agents
{
	public interface IBookAgent
	{

		/// <summary>
		/// Gets the books.
		/// </summary>
		/// <param name="tableType">Type of the table.</param>
		/// <param name="bookOrder">The book order.</param>
		/// <param name="searchTerm">The search term.</param>
		/// <param name="pageNumber">The page number.</param>
		/// <returns>
		/// The books found
		/// </returns>
		Task<PaginatedResultsDto<Book>> GetBooksAsync(string baseUri, TableType tableType, BookOrder bookOrder, string searchTerm, int pageNumber);
	}
}
