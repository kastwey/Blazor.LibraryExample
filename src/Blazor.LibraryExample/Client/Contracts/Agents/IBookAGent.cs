using Blazor.LibraryExample.Client.Dtos;
using Blazor.LibraryExample.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.LibraryExample.Client.Contracts.Agents
{
	public interface IBookAgent
	{

		/// <summary>
		/// Gets the books.
		/// </summary>
		/// <param name="baseUri">The base URI.</param>
		/// <param name="tableType">Type of the table.</param>
		/// <param name="bookOrder">The book order.</param>
		/// <param name="searchTerm">The search term.</param>
		/// <param name="pageNumber">The page number.</param>
		/// <returns>
		/// The books found
		/// </returns>
		Task<PaginatedResultsDto<Book>> GetBooksAsync(TableType tableType, BookOrder bookOrder, string searchTerm, int pageNumber);
	}
}
