namespace Blazor.LibraryExample.ServerSideRendering.Contracts.Repositories
{
	using System.Threading.Tasks;
	using Blazor.LibraryExample.ServerSideRendering.Repositories;
	using Blazor.LibraryExample.Shared.Entities;

	/// <summary>
	/// The books repository abstraction.
	/// </summary>
	public interface IBooksRepository
	{
		/// <summary>
		/// Gets the novelties asynchronous.
		/// </summary>
		/// <param name="pageNumber">The page number.</param>
		/// <param name="resultsPerPage">The results per page.</param>
		/// <param name="order">The order.</param>
		/// <returns>the novelties.</returns>
		Task<PaginatedResults<Book>> GetNoveltiesAsync(int pageNumber, int resultsPerPage, BookOrder order = BookOrder.ByCreationDateDescending);

		/// <summary>
		/// Gets the most downloadeds asynchronous.
		/// </summary>
		/// <param name="pageNumber">The page number.</param>
		/// <param name="resultsPerPage">The results per page.</param>
		/// <param name="order">The order.</param>
		/// <returns>The books most downloaded.</returns>
		Task<PaginatedResults<Book>> GetMostDownloadedsAsync(int pageNumber, int resultsPerPage, BookOrder order = BookOrder.ByNumberOfDownloadsDescending);

		/// <summary>
		/// Gets a book by its identifier asynchronous.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>The book found, null if it was not found.</returns>
		Task<Book> GetByIdAsync(int id);

		/// <summary>
		/// Adds a book asynchronous.
		/// </summary>
		/// <param name="book">The book.</param>
		/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<Book> AddAsync(Book book);

		/// <summary>
		/// Edits a book asynchronous.
		/// </summary>
		/// <param name="book">The book.</param>
		/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task EditAsync(Book book);

		/// <summary>
		/// Deletes a book asynchronous.
		/// </summary>
		/// <param name="bookId">The book identifier.</param>
		/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task DeleteAsync(int bookId);

		/// <summary>
		/// Searches by keyword asynchronous.
		/// </summary>
		/// <param name="searchTerm">The search term.</param>
		/// <param name="pageNumber">The page number.</param>
		/// <param name="resultsPerPage">The results per page.</param>
		/// <param name="order">The order.</param>
		/// <returns>The books which matches with the specified term.</returns>
		Task<PaginatedResults<Book>> SearchAsync(string searchTerm, int pageNumber, int resultsPerPage, BookOrder order);
	}
}
