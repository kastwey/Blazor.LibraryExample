namespace Blazor.LibraryExample.ServerSideRendering.Dtos
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Text;

	/// <summary>
	/// Class which encapsulate information about paginated result
	/// </summary>
	/// <typeparam name="TItem">the type of object that will be stored as IEnumerable TItem in the Result property.</typeparam>
	public class PaginatedResultsDto<TItem>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PaginatedResultsDto{TItem}"/> class.
		/// </summary>
		public PaginatedResultsDto()
		{
		}

		/// <summary>
		/// Gets or sets the results.
		/// </summary>
		/// <value>
		/// The results.
		/// </value>
		public IEnumerable<TItem> Results { get; set; }

		/// <summary>
		/// Gets or sets the pagination information.
		/// </summary>
		/// <value>
		/// The pagination information.
		/// </value>
		public PaginatedResultsMetadataDto PaginationInfo { get; set;  }
	}
}
