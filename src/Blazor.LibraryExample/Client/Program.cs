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
using Blazor.LibraryExample.Shared.Implementation;
using Blazor.LibraryExample.Shared.Interfaces;
using OpenCage.Geocode;
using Blazor.LibraryExample.Shared.INterfaces;
using Blazor.LibraryExample.Shared.Implementations;

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
			builder.Services.AddSingleton<IImageProcessorService, ImageProcessorService>();
			builder.Services.AddSingleton<IGeocoder, Geocoder>(serviceProvider => new Geocoder("c2f2802b34c44e2b87d508cd6e3d523a"));
			builder.Services.AddSingleton<IOpenCageDataAgent, OpenCageDataAgent>();
			builder.RootComponents.Add<App>("app");

			await builder.Build().RunAsync();
		}
	}
}
