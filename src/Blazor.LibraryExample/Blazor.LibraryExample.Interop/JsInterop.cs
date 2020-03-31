namespace Blazor.LibraryExample.Interop
{
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Components;
	using Microsoft.JSInterop;

	/// <summary>
	/// Clas to wrapp JS functions into c#functions.
	/// </summary>
	public static class JsInterop
	{
		/// <summary>
		/// Prompts the specified message asynchronous.
		/// </summary>
		/// <param name="jsRuntime">The js runtime.</param>
		/// <param name="message">The message.</param>
		/// <returns>
		/// true if the user press Accept, false otherwise.
		/// </returns>
		public static ValueTask<string> PromptAsync(IJSRuntime jsRuntime, string message)
		{
			return jsRuntime.InvokeAsync<string>(
				"interopFunctions.showPrompt",
				message);
		}

		/// <summary>
		/// Alerts the specified message asynchronous.
		/// </summary>
		/// <param name="jsRuntime">The js runtime.</param>
		/// <param name="message">The message.</param>
		/// <returns>
		/// A <see cref="Task" /> representing the asynchronous operation.
		/// </returns>
		public static ValueTask AlertAsync(IJSRuntime jsRuntime, string message) =>
			jsRuntime.InvokeVoidAsync(
				"interopFunctions.alert",
				message);

		/// <summary>
		/// Do focus into the specified element asynchronous.
		/// </summary>
		/// <param name="jsRuntime">The js runtime.</param>
		/// <param name="element">The element.</param>
		/// <returns>The task to track the async operation.</returns>
		public static ValueTask FocusAsync(IJSRuntime jsRuntime, ElementReference element) =>
			jsRuntime.InvokeVoidAsync(
				"interopFunctions.focus", element);

		/// <summary>
		/// Do focus in the element specified by its ID.
		/// </summary>
		/// <param name="jsRuntime">The js runtime.</param>
		/// <param name="selector">The selector.</param>
		/// <returns>The task to track the async operation.</returns>
		/// <exception cref="System.ArgumentNullException">selector.</exception>
		public static async ValueTask FocusAsync(IJSRuntime jsRuntime, string selector)
		{
			CheckSelectorValidity(selector);
			await jsRuntime.InvokeVoidAsync(
				"interopFunctions.focusByQuerySelector", selector);
		}

		/// <summary>
		/// Do focus in the firs header of the specified level asynchronous.
		/// </summary>
		/// <param name="jsRuntime">The js runtime.</param>
		/// <param name="headerLevel">The header level.</param>
		/// <returns>The task to track the async operation.</returns>
		public static ValueTask FocusInHeaderAsync(IJSRuntime jsRuntime, int headerLevel) =>
			jsRuntime.InvokeVoidAsync(
				"interopFunctions.focusInHeader", headerLevel);

		/// <summary>
		/// Performs click in the specified <paramref name="element" />.
		/// </summary>
		/// <param name="jsRuntime">The js runtime.</param>
		/// <param name="element">The element.</param>
		/// <returns>The task to track the async operation.</returns>
		public static ValueTask ClickAsync(IJSRuntime jsRuntime, ElementReference element) =>
			jsRuntime.InvokeVoidAsync(
					"interopFunctions.click", element);

		/// <summary>
		/// Sets the page title asynchronous.
		/// </summary>
		/// <param name="jsRuntime">The js runtime.</param>
		/// <param name="title">The title.</param>
		/// <returns>
		/// A <see cref="Task" /> representing the asynchronous operation.
		/// </returns>
		public static ValueTask SetTitleAsync(IJSRuntime jsRuntime, string title) =>
			jsRuntime.InvokeVoidAsync(
					"interopFunctions.setTitle", title);

		/// <summary>
		/// Gets the document title asynchronous.
		/// </summary>
		/// <param name="jsRuntime">The js runtime.</param>
		/// <returns>
		/// A <see cref="Task" /> representing the asynchronous operation.
		/// </returns>
		public static ValueTask<string> GetTitleAsync(IJSRuntime jsRuntime) =>
			jsRuntime.InvokeAsync<string>(
					"interopFunctions.getTitle");

		/// <summary>
		/// Invoke the JS confirm Function to demand confirmation for an action.
		/// </summary>
		/// <param name="jsRuntime">The js runtime.</param>
		/// <param name="message">The message.</param>
		/// <returns>
		/// True if user press in OK, false otherwise.
		/// </returns>
		public static ValueTask<bool> ConfirmAsync(IJSRuntime jsRuntime, string message) =>
			jsRuntime.InvokeAsync<bool>(
					"interopFunctions.confirm", message);

		private static void CheckSelectorValidity(string selector)
		{
			if (selector == null)
			{
				throw new System.ArgumentNullException(nameof(selector));
			}
		}
	}
}
