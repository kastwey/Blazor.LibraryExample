namespace Blazor.LibraryExample.Server
{
	using Microsoft.AspNetCore;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.Configuration;

	/// <summary>
	/// program class
	/// </summary>
	public class Program
	{
		/// <summary>
		/// Defines the entry point of the application.
		/// </summary>
		/// <param name="args">The arguments.</param>
		public static void Main(string[] args)
		{
			BuildWebHost(args).Run();
		}

		/// <summary>
		/// Builds the web host.
		/// </summary>
		/// <param name="args">The arguments.</param>
		/// <returns><see cref="IWebHost"/> with default options</returns>
		public static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseConfiguration(new ConfigurationBuilder()
					.AddCommandLine(args)
					.Build())
				.UseStartup<Startup>()
				.Build();
	}
}
