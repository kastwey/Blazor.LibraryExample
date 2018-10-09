namespace Blazor.LibraryExample.Shared.Entities
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Text;

	/// <summary>
	/// Class to store paginated results
	/// </summary>
	/// <typeparam name="TItem">The type of object that will be stored as IEnumerable in the Results property</typeparam>
	public class PaginatedResults<TItem>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PaginatedResults{TItem}"/> class.
		/// </summary>
		/// <param name="results">The results.</param>
		/// <param name="currentPage">The current page.</param>
		/// <param name="resultsPerPage">The results per page.</param>
		/// <param name="totalRows">The total rows.</param>
		public PaginatedResults(IEnumerable<TItem> results, int currentPage, int resultsPerPage, int totalRows)
		{
			this.Results = results;
			var metadata = new PaginatedResultsMetadata
			{
				CurrentPage = currentPage,
				ResultsPerPage = resultsPerPage,
				TotalRows = totalRows,
				TotalPages = totalRows / resultsPerPage
			};
			if (totalRows % resultsPerPage != 0)
			{
				metadata.TotalPages++;
			}

			this.PaginationInfo = metadata;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PaginatedResults{TItem}"/> class.
		/// </summary>
		public PaginatedResults()
		{
		}

		/// <summary>
		/// Gets the results.
		/// </summary>
		/// <value>
		/// The results.
		/// </value>
		public IEnumerable<TItem> Results { get; }

		/// <summary>
		/// Gets the pagination information.
		/// </summary>
		/// <value>
		/// The pagination information.
		/// </value>
		public PaginatedResultsMetadata PaginationInfo { get; }
	}
}
