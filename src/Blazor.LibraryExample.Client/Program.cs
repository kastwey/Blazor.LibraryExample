namespace Blazor.LibraryExample.Client
{
	using Microsoft.AspNetCore.Blazor.Hosting;

	/// <summary>
	/// The program class
	/// </summary>
	public class Program
	{
		/// <summary>
		/// Defines the entry point of the application.
		/// </summary>
		/// <param name="args">The arguments.</param>
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		/// <summary>
		/// Creates the host builder.
		/// </summary>
		/// <param name="args">The arguments.</param>
		/// <returns><see cref="IWebAssemblyHostBuilder"/></returns>
		public static IWebAssemblyHostBuilder CreateHostBuilder(string[] args) =>
			BlazorWebAssemblyHost.CreateDefaultBuilder()
				.UseBlazorStartup<Startup>();
	}
}
