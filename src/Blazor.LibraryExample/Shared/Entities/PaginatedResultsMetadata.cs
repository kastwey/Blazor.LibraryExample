namespace Blazor.LibraryExample.Shared.Entities
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	/// <summary>
	/// Class to store information about current pagination
	/// </summary>
	public class PaginatedResultsMetadata
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
		/// Gets a value indicating whether this instance has previous page.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has previous page; otherwise, <c>false</c>.
		/// </value>
		public bool HasPreviousPage
		{
			get
			{
				return this.CurrentPage > 1;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance has next page.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has next page; otherwise, <c>false</c>.
		/// </value>
		public bool HasNextPage
		{
			get
			{
				return this.CurrentPage < this.TotalPages;
			}
		}

		/// <summary>
		/// Gets the results from.
		/// </summary>
		/// <value>
		/// The results from.
		/// </value>
		public int ResultsFrom => ((this.CurrentPage - 1) * this.ResultsPerPage) + 1;

		/// <summary>
		/// Gets the results to.
		/// </summary>
		/// <value>
		/// The results to.
		/// </value>
		public int ResultsTo
		{
			get
			{
				if (this.TotalRows == 0)
				{
					return 0;
				}

				var to = this.ResultsFrom + this.ResultsPerPage - 1;
				if ((to - 1) + this.ResultsPerPage > this.TotalRows)
				{
					to = this.TotalRows;
				}

				return to;
			}
		}
	}
}
