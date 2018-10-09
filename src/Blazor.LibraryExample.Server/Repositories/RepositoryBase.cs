namespace Blazor.LibraryExample.Server.Repositories
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Blazor.LibraryExample.Shared.Entities;
	using Microsoft.EntityFrameworkCore;

	/// <summary>
	/// The base repository from which other repositories should inherit.
	/// </summary>
	/// <typeparam name="T">The entity managed by the repository</typeparam>
	public abstract class RepositoryBase<T>
		where T : class
	{
		/// <summary>
		/// Paginates the i queryable asynchronous.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <param name="pageNumber">The page number.</param>
		/// <param name="resultsPerPage">The results per page.</param>
		/// <returns>The <paramref name="query"/> paginated with the <paramref name="pageNumber"/> and <paramref name="resultsPerPage"/> specified</returns>
		protected async Task<PaginatedResults<T>> PaginateIQueryableAsync(IQueryable<T> query, int pageNumber, int resultsPerPage)
		{
			int totalRows = await query.CountAsync();
			var paginatedRows = await query.Skip((pageNumber - 1) * resultsPerPage).Take(resultsPerPage).ToListAsync();
			return new PaginatedResults<T>(paginatedRows, pageNumber, resultsPerPage, totalRows);
		}
	}
}
