namespace Blazor.LibraryExample.Client
{
	using Blazor.LibraryExample.Client.Contracts.Services;
	using Blazor.LibraryExample.Client.Services;
	using Microsoft.AspNetCore.Blazor.Builder;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	/// The application startup class
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// Configures the services.
		/// </summary>
		/// <param name="services">The services.</param>
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<IBookService, BookService>();
		}

		/// <summary>
		/// Configures the application.
		/// </summary>
		/// <param name="app">The blazor application.</param>
		public void Configure(IBlazorApplicationBuilder app)
		{
			app.AddComponent<App>("app");
		}
	}
}
