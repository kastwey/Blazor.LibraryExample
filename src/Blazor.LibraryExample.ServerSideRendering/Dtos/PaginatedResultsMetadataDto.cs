namespace Blazor.LibraryExample.ServerSideRendering.Dtos
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	/// <summary>
	/// Class to store information about current pagination
	/// </summary>
	public class PaginatedResultsMetadataDto
	{
		/// <summary>
		/// Gets or sets the current page.
		/// </summary>
		/// <value>
		/// The current page.
		/// </value>
		public int CurrentPage { get; set; }

		/// <summary>
		/// Gets or sets the results per page.
		/// </summary>
		/// <value>
		/// The results per page.
		/// </value>
		public int ResultsPerPage { get; set; }

		/// <summary>
		/// Gets or sets the total pages.
		/// </summary>
		/// <value>
		/// The total pages.
		/// </value>
		public int TotalPages { get; set; }

		/// <summary>
		/// Gets or sets the total rows.
		/// </summary>
		/// <value>
		/// The total rows.
		/// </value>
		public int TotalRows { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance has previous page.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has previous page; otherwise, <c>false</c>.
		/// </value>
		public bool HasPreviousPage { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance has next page.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has next page; otherwise, <c>false</c>.
		/// </value>
		public bool HasNextPage { get; set; }

		/// <summary>
		/// Gets or sets the results from.
		/// </summary>
		/// <value>
		/// The results from.
		/// </value>
		public int ResultsFrom { get; set; }

		/// <summary>
		/// Gets or sets the results to.
		/// </summary>
		/// <value>
		/// The results to.
		/// </value>
		public int ResultsTo { get; set; }
	}
}
