using Blazor.LibraryExample.Client.Contracts.Agents;
using Blazor.LibraryExample.Client.Dtos;
using Blazor.LibraryExample.Shared.Entities;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blazor.LibraryExample.Client.Agents
{
	public class BookAgent : IBookAgent
	{

		private readonly HttpClient _httpClient;

		public BookAgent(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		/// <summary>
		/// Gets the books asynchronous.
		/// </summary>
		/// <param name="tableType">Type of the table.</param>
		/// <param name="bookOrder">The book order.</param>
		/// <param name="searchTerm">The search term.</param>
		/// <param name="pageNumber">The page number.</param>
		/// <returns>
		/// The books found
		/// </returns>
		public async Task<PaginatedResultsDto<Book>> GetBooksAsync(TableType tableType, BookOrder bookOrder, string searchTerm, int pageNumber)
		{
			if (pageNumber <= 0)
			{
				pageNumber = 1;
			}
			
			string url = null;
			switch (tableType)
			{
				case TableType.Novelties:
					url = "api/books/novelties";
					break;
				case TableType.MostDownloadeds:
					url = "api/books/mostDownloadeds";
					break;
				case TableType.SearchResults:
					url = $"api/books/search?searchTerm={ searchTerm}";
					break;
			}

			url += (url.Contains("?") ? "&" : "?") +
			$"pageNumber={pageNumber}&resultsPerPage=10&order={bookOrder}";

			var books = await _httpClient.GetJsonAsync<PaginatedResultsDto<Book>>(url);
			return books;
		}
	}
}
