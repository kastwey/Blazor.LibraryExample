using Blazor.LibraryExample.ServerSideRendering.Contracts.Agents;
using Blazor.LibraryExample.ServerSideRendering.Dtos;
using Blazor.LibraryExample.Shared.Entities;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blazor.LibraryExample.ServerSideRendering.Agents
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
		public async Task<PaginatedResultsDto<Book>> GetBooksAsync(string baseUri, TableType tableType, BookOrder bookOrder, string searchTerm, int pageNumber)
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
			url = $"{baseUri}{url}";
			var result = await _httpClient.GetAsync(url);
			result.EnsureSuccessStatusCode();
			var content = await result.Content.ReadAsStringAsync();
			var books = JsonConvert.DeserializeObject< PaginatedResultsDto<Book>>(content);
			return books;
		}
	}
}
