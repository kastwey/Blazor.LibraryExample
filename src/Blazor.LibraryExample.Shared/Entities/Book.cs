namespace Blazor.LibraryExample.Shared.Entities
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Text;

	/// <summary>
	/// The Book entity
	/// </summary>
	public class Book
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>
		/// The title.
		/// </value>
		[Required]
		[MaxLength(100, ErrorMessage = "El título del libro debe contener 100 caracteres como máximo.")]
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets the author.
		/// </summary>
		/// <value>
		/// The author.
		/// </value>
		[Required]
		[MaxLength(100, ErrorMessage = "El autor del libro debe contener 100 caracteres como máximo.")]
		public string Author { get; set; }

		/// <summary>
		/// Gets or sets the creation date.
		/// </summary>
		/// <value>
		/// The creation date.
		/// </value>
		public DateTime CreationDate { get; set; }

		/// <summary>
		/// Gets or sets the total downloads.
		/// </summary>
		/// <value>
		/// The total downloads.
		/// </value>
		public int TotalDownloads { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the book is indise a collection
		/// </summary>
		/// <value>
		///   <c>true</c> if [in collection]; otherwise, <c>false</c>.
		/// </value>
		public bool InCollection { get; set; }
	}
}
