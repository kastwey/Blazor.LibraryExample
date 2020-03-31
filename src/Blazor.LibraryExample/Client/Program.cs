using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Blazor.LibraryExample.Client.Contracts.Agents;
using Blazor.LibraryExample.Client.Agents;
using Blazor.LibraryExample.Client.Contracts.Services;
using Blazor.LibraryExample.Client.Services;

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
			builder.Services.AddSingleton<IBookService, BookService>();
			builder.Services.AddSingleton<IBookAgent, BookAgent>();
			builder.RootComponents.Add<App>("app");

			await builder.Build().RunAsync();
		}
	}
}
