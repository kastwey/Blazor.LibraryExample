namespace Blazor.LibraryExample.Server.Controllers
{
	using System.Threading.Tasks;
	using Blazor.LibraryExample.Server.Contracts.Repositories;
	using Blazor.LibraryExample.Server.Repositories;
	using Blazor.LibraryExample.Shared.Entities;
	using Microsoft.AspNetCore.Mvc;

	/// <summary>
	/// The book controller
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
	[ApiController]
	[Route("~/api/[controller]")]
	public class BooksController : Controller
	{
		private readonly IBooksRepository booksRepository;

		/// <summary>
		/// Initializes a new instance of the <see cref="BooksController"/> class.
		/// </summary>
		/// <param name="booksRepository">The books repository.</param>
		public BooksController(IBooksRepository booksRepository)
		{
			this.booksRepository = booksRepository;
		}

		/// <summary>
		/// Gets the novelties.
		/// </summary>
		/// <param name="pageNumber">The page number.</param>
		/// <param name="resultsPerPage">The results per page.</param>
		/// <param name="order">The order.</param>
		/// <returns>All novelties</returns>
		[HttpGet("novelties")]
		public async Task<IActionResult> GetNovelties(int pageNumber = 1, int resultsPerPage = 10, BookOrder order = BookOrder.ByTitleAscending)
		{
			return Ok(await booksRepository.GetNoveltiesAsync(pageNumber, resultsPerPage, order));
		}

		/// <summary>
		/// Gets the books most downloadeds.
		/// </summary>
		/// <param name="pageNumber">The page number.</param>
		/// <param name="resultsPerPage">The results per page.</param>
		/// <param name="order">The order.</param>
		/// <returns>The most downloaded books</returns>
		[HttpGet("mostDownloadeds")]
		public async Task<IActionResult> GetMostDownloadeds(int pageNumber = 1, int resultsPerPage = 10, BookOrder order = BookOrder.ByNumberOfDownloadsDescending)
		{
			return Ok(await booksRepository.GetMostDownloadedsAsync(pageNumber, resultsPerPage, order));
		}

		/// <summary>
		/// Searches buts by specified keyword.
		/// </summary>
		/// <param name="searchTerm">The search term.</param>
		/// <param name="pageNumber">The page number.</param>
		/// <param name="resultsPerPage">The results per page.</param>
		/// <param name="order">The order.</param>
		/// <returns>The search results</returns>
		[HttpGet("search")]
		public async Task<IActionResult> Search(string searchTerm, int pageNumber = 1, int resultsPerPage = 10, BookOrder order = BookOrder.ByTitleAscending)
		{
			return Ok(await booksRepository.SearchAsync(searchTerm, pageNumber, resultsPerPage, order));
		}

		/// <summary>
		/// Gets the details of the specified book
		/// </summary>
		/// <param name="bookId">The book identifier.</param>
		/// <returns>The details for the specified book by <paramref name="bookId"/></returns>
		[HttpGet("details/{bookId}")]
		public async Task<IActionResult> Details(int bookId)
		{
			var book = await booksRepository.GetByIdAsync(bookId);
			if (book == null)
			{
				return NotFound();
			}

			return Ok(book);
		}

		/// <summary>
		/// Adds the specified book.
		/// </summary>
		/// <param name="book">The book.</param>
		/// <returns>if the book has been created successfully, the new entity with its generated identifier.</returns>
		[HttpPost("add")]
		public async Task<IActionResult> Add([FromBody]Book book)
		{
			var bookAdded = await booksRepository.AddAsync(book);
			return Created($"/books/details/{book.Id}", bookAdded);
		}

		/// <summary>
		/// Edits the specified book.
		/// </summary>
		/// <param name="bookId">The book identifier.</param>
		/// <param name="book">The book.</param>
		/// <returns>if the book has been edited successfully, an Action result with NoContentResult.</returns>
		[HttpPut("edit/{id}")]
		public async Task<IActionResult> Edit(int bookId, [FromBody]Book book)
		{
			await booksRepository.EditAsync(book);
			return NoContent();
		}

		/// <summary>
		/// Deletes the specified book.
		/// </summary>
		/// <param name="bookId">The book identifier.</param>
		/// <returns>If the book has been deleted successfully, ActionResult with NoContentResult.</returns>
		[HttpDelete("delete/{bookId}")]
		public async Task<IActionResult> Delete(int bookId)
		{
			await booksRepository.DeleteAsync(bookId);
			return NoContent();
		}

		[HttpGet("ReadSampleBook")]
		public async Task<IActionResult> ReadSampleBook()
		{
			return File(System.IO.File.ReadAllBytes("Files/perro.epub"), "application/epub+zip");
		}

	}
}