namespace Blazor.LibraryExample.Server.Repositories
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Blazor.LibraryExample.Server.Contracts.Repositories;
	using Blazor.LibraryExample.Server.Data;
	using Blazor.LibraryExample.Shared.Entities;
	using Microsoft.EntityFrameworkCore;
	using Newtonsoft.Json;

	/// <summary>
	/// The book repository class
	/// </summary>
	/// <seealso cref="Blazor.LibraryExample.Server.Contracts.Repositories.IBooksRepository" />
	public class BooksRepository : RepositoryBase<Book>, IBooksRepository
	{
		private readonly LibraryContext libraryContext;

		/// <summary>
		/// Initializes a new instance of the <see cref="BooksRepository"/> class.
		/// </summary>
		/// <param name="libraryContext">The library context.</param>
		public BooksRepository(LibraryContext libraryContext)
		{
			this.libraryContext = libraryContext;
		}

		/// <summary>
		/// Gets the most downloadeds books asynchronous.
		/// </summary>
		/// <param name="pageNumber">The page number.</param>
		/// <param name="resultsPerPage">The results per page.</param>
		/// <param name="order">The order.</param>
		/// <returns>The most downloaded books</returns>
		public async Task<PaginatedResults<Book>> GetMostDownloadedsAsync(int pageNumber, int resultsPerPage, BookOrder order = BookOrder.ByNumberOfDownloadsDescending)
		{
			var bookIds = libraryContext.Books.OrderByDescending(b => b.TotalDownloads).Take(20).Select(b => b.Id);
			var books = libraryContext.Books.Where(b => bookIds.Contains(b.Id));
			books = ApplyOrder(books, order);
			return await PaginateIQueryableAsync(books, pageNumber, resultsPerPage);
		}

		/// <summary>
		/// Gets the book novelties asynchronous.
		/// </summary>
		/// <param name="pageNumber">The page number.</param>
		/// <param name="resultsPerPage">The results per page.</param>
		/// <param name="order">The order.</param>
		/// <returns>The novelties</returns>
		public async Task<PaginatedResults<Book>> GetNoveltiesAsync(int pageNumber, int resultsPerPage, BookOrder order = BookOrder.ByCreationDateDescending)
		{
			DateTime thirtyDaysBefore = DateTime.Now.AddDays(-30);
			var books = libraryContext.Books.Where(b => b.CreationDate >= thirtyDaysBefore);
			books = ApplyOrder(books, order);
			return await PaginateIQueryableAsync(books, pageNumber, resultsPerPage);
		}

		/// <summary>
		/// Gets a book by its identifier asynchronous.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>
		/// The book found, null if it was not found
		/// </returns>
		public async Task<Book> GetByIdAsync(int id)
		{
			return await libraryContext.Books.FindAsync(id);
		}

		/// <summary>
		/// Adds a book asynchronous.
		/// </summary>
		/// <param name="book">The book.</param>
		/// <returns>
		/// A <see cref="T:System.Threading.Tasks.Task" /> representing the asynchronous operation.
		/// </returns>
		public async Task<Book> AddAsync(Book book)
		{
			book.CreationDate = DateTime.UtcNow;
			libraryContext.Books.Add(book);
			await libraryContext.SaveChangesAsync();
			return book;
		}

		/// <summary>
		/// Edits a book asynchronous.
		/// </summary>
		/// <param name="book">The book.</param>
		/// <returns>
		/// A <see cref="T:System.Threading.Tasks.Task" /> representing the asynchronous operation.
		/// </returns>
		public async Task EditAsync(Book book)
		{
			var bookFromDb = await libraryContext.Books.FindAsync(book.Id);
			bookFromDb.Title = book.Title;
			bookFromDb.Author = book.Author;
			bookFromDb.CreationDate = book.CreationDate;
			bookFromDb.TotalDownloads = book.TotalDownloads;
			bookFromDb.InCollection = book.InCollection;
			await libraryContext.SaveChangesAsync();
		}

		/// <summary>
		/// Deletes a book asynchronous.
		/// </summary>
		/// <param name="bookId">The book identifier.</param>
		/// <returns>
		/// A <see cref="T:System.Threading.Tasks.Task" /> representing the asynchronous operation.
		/// </returns>
		public async Task DeleteAsync(int bookId)
		{
			libraryContext.Books.Remove(await libraryContext.Books.FindAsync(bookId));
			await libraryContext.SaveChangesAsync();
		}

		/// <summary>
		/// Searches by keyword asynchronous.
		/// </summary>
		/// <param name="searchTerm">The search term.</param>
		/// <param name="pageNumber">The page number.</param>
		/// <param name="resultsPerPage">The results per page.</param>
		/// <param name="order">The order.</param>
		/// <returns>
		/// The books which matches with the specified term
		/// </returns>
		public async Task<PaginatedResults<Book>> SearchAsync(string searchTerm, int pageNumber, int resultsPerPage, BookOrder order)
		{
			var books = libraryContext.Books.Where(b => EF.Functions.Like(b.Title, $"%{searchTerm}%") || EF.Functions.Like(b.Author, $"%{searchTerm}%"));
			books = ApplyOrder(books, order);
			return await this.PaginateIQueryableAsync(books, pageNumber, resultsPerPage);
		}

		/// <summary>
		/// Applies the order to the specified IQueryable object.
		/// </summary>
		/// <param name="books">The books.</param>
		/// <param name="order">The order.</param>
		/// <returns>The books ordered by <paramref name="order"/></returns>
		private IQueryable<Book> ApplyOrder(IQueryable<Book> books, BookOrder order)
		{
			switch (order)
			{
				default:
					books = books.OrderBy(b => b.Title);
					break;
				case BookOrder.ByTitleDescending:
					books = books.OrderByDescending(b => b.Title);
					break;
				case BookOrder.ByAuthorAscending:
					books = books.OrderBy(b => b.Author);
					break;
				case BookOrder.ByAuthorDescending:
					books = books.OrderByDescending(b => b.Author);
					break;
				case BookOrder.ByCreationDateAscending:
					books = books.OrderBy(b => b.CreationDate);
					break;
				case BookOrder.ByCreationDateDescending:
					books = books.OrderByDescending(b => b.CreationDate);
					break;
				case BookOrder.ByNumberOFDownloadsAscending:
					books = books.OrderBy(b => b.TotalDownloads);
					break;
				case BookOrder.ByNumberOfDownloadsDescending:
					books = books.OrderByDescending(b => b.TotalDownloads);
					break;
			}

			return books;
		}
	}
}
