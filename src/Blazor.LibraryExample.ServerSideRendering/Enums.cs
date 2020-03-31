using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.LibraryExample.ServerSideRendering
{

	/// <summary>
	/// 
	/// </summary>
	public enum BookOrder
	{
		/// <summary>
		/// The by title ascending
		/// </summary>
		ByTitleAscending = 1,
		/// <summary>
		/// The by title descending
		/// </summary>
		ByTitleDescending = 2,
		/// <summary>
		/// The by author ascending
		/// </summary>
		ByAuthorAscending = 4,
		/// <summary>
		/// The by author descending
		/// </summary>
		ByAuthorDescending = 8,
		/// <summary>
		/// The by creation date ascending
		/// </summary>
		ByCreationDateAscending = 16,
		/// <summary>
		/// The by creation date descending
		/// </summary>
		ByCreationDateDescending = 32,
		/// <summary>
		/// The by number of downloads ascending
		/// </summary>
		ByNumberOFDownloadsAscending = 64,
		/// <summary>
		/// The by number of downloads descending
		/// </summary>
		ByNumberOfDownloadsDescending = 128
	}

	/// <summary>
	/// 
	/// </summary>
	public enum TableType
	{
		/// <summary>
		/// The novelties
		/// </summary>
		Novelties,
		/// <summary>
		/// The most downloadeds
		/// </summary>
		MostDownloadeds,
		/// <summary>
		/// The search results
		/// </summary>
		SearchResults
	}
}
