using Blazor.LibraryExample.Client.Agents;
using Blazor.LibraryExample.Client.Contracts.Agents;
using Blazor.LibraryExample.Client.Contracts.Services;
using Blazor.LibraryExample.Client.Services;
using Blazor.LibraryExample.Shared.Implementation;
using Blazor.LibraryExample.Shared.Implementations;
using Blazor.LibraryExample.Shared.Interfaces;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;


using System.Threading.Tasks;

namespace Blazor.LibraryExample.Client
{
	public class Program
	{
		/// <summary>
		/// Defines the entry point of the application.
		/// </summary>
		/// <param name="args">The arguments.</param>
		/// <returns>Task representing the asynchronous operation</returns>
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.Services.AddBaseAddressHttpClient();
			builder.Services.AddSingleton<IBookService, BookService>();
			builder.Services.AddSingleton<IBookAgent, BookAgent>();
			builder.Services.AddSingleton<IImageProcessorService, ImageProcessorService>();
			builder.Services.AddSingleton<IOpenCageDataAgent, OpenCageDataAgent>();
			builder.RootComponents.Add<App>("app");

			await builder.Build().RunAsync();
		}
	}
}
