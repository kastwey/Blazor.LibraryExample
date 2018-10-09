namespace Blazor.LibraryExample.Interop
{
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Blazor;
	using Microsoft.JSInterop;

	/// <summary>
	/// Clas to wrapp JS functions into c#functions
	/// </summary>
	public static class JsInterop
	{
		/// <summary>
		/// Prompts the specified message asynchronous.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <returns>true if the user press Accept, false otherwise</returns>
		public static Task<string> PromptAsync(string message)
		{
			return JSRuntime.Current.InvokeAsync<string>(
				"interopJsFunctions.showPrompt",
				message);
		}

		/// <summary>
		/// Alerts the specified message asynchronous.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		public static Task AlertAsync(string message)
		{
			return JSRuntime.Current.InvokeAsync<string>(
				"interopJsFunctions.alert",
				message);
		}

		/// <summary>
		/// Do focus into the specified element asynchronous.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		public static async Task FocusAsync(ElementRef element)
		{
			await JSRuntime.Current.InvokeAsync<string>(
				"interopJsFunctions.focus", element);
		}

		/// <summary>
		/// Do focus in the element specified by its ID.
		/// </summary>
		/// <param name="selector">The selector.</param>
		/// <returns>
		/// A <see cref="Task" /> representing the asynchronous operation.
		/// </returns>
		/// <exception cref="System.ArgumentNullException">selector</exception>
		public static async Task FocusAsync(string selector)
		{
			CheckSelectorValidity(selector);

			await JSRuntime.Current.InvokeAsync<string>(
				"interopJsFunctions.focusByQuerySelector", selector);
		}

		/// <summary>
		/// Do focus in the firs header of the specified level asynchronous.
		/// </summary>
		/// <param name="headerLevel">The header level.</param>
		/// <returns>
		/// A <see cref="Task" /> representing the asynchronous operation.
		/// </returns>
		public static async Task FocusInHeaderAsync(int headerLevel)
		{
			await JSRuntime.Current.InvokeAsync<object>(
				"interopJsFunctions.focusInHeader", headerLevel);
		}

		/// <summary>
		/// Performs click in the specified <paramref name="element" />
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns>
		/// A <see cref="Task" /> representing the asynchronous operation.
		/// </returns>
		public static async Task ClickAsync(ElementRef element)
		{
			await JSRuntime.Current.InvokeAsync<object>(
					"interopJsFunctions.click", element);
		}

		/// <summary>
		/// Sets the page title asynchronous.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		public static async Task SetTitleAsync(string title)
		{
			await JSRuntime.Current.InvokeAsync<object>(
					"interopJsFunctions.setTitle", title);
		}

		/// <summary>
		/// Gets the document title asynchronous.
		/// </summary>
		/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		public static async Task<string> GetTitleAsync()
		{
			return await JSRuntime.Current.InvokeAsync<string>(
					"interopJsFunctions.getTitle");
		}

		/// <summary>
		/// Invoke the JS confirm Function to demand confirmation for an action
		/// </summary>
		/// <param name="message">The message.</param>
		/// <returns>True if user press in OK, false otherwise</returns>
		public static async Task<bool> ConfirmAsync(string message)
		{
			return await JSRuntime.Current.InvokeAsync<bool>(
					"interopJsFunctions.confirm", message);
		}

		private static void CheckSelectorValidity(string selector)
		{
			if (selector == null)
			{
				throw new System.ArgumentNullException(nameof(selector));
			}
		}
	}
}
