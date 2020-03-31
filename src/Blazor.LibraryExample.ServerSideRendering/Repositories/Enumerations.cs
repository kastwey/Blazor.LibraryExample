namespace Blazor.LibraryExample.ServerSideRendering.Repositories
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	/// <summary>
	/// The different fields by which the books can be sorted.
	/// </summary>
	public enum BookOrder
	{
		/// <summary>
		/// oders the books by title ascending
		/// </summary>
		ByTitleAscending,

		/// <summary>
		/// Orders the books by title descending
		/// </summary>
		ByTitleDescending,

		/// <summary>
		/// Orders the books by author ascending
		/// </summary>
		ByAuthorAscending,

		/// <summary>
		/// Orders the books by author descending
		/// </summary>
		ByAuthorDescending,

		/// <summary>
		/// Orders the books by creation date ascending
		/// </summary>
		ByCreationDateAscending,

		/// <summary>
		/// Orders the books by creation date descending
		/// </summary>
		ByCreationDateDescending,

		/// <summary>
		/// Orders the books by number of downloads ascending
		/// </summary>
		ByNumberOFDownloadsAscending,

		/// <summary>
		/// Orders the books by number of downloads descending
		/// </summary>
		ByNumberOfDownloadsDescending,
	}
}