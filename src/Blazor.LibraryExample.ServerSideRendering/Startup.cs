using Blazor.LibraryExample.ServerSideRendering.Agents;
using Blazor.LibraryExample.ServerSideRendering.Contracts.Agents;
using Blazor.LibraryExample.ServerSideRendering.Contracts.Repositories;
using Blazor.LibraryExample.ServerSideRendering.Contracts.Services;
using Blazor.LibraryExample.ServerSideRendering.Data;
using Blazor.LibraryExample.ServerSideRendering.Repositories;
using Blazor.LibraryExample.ServerSideRendering.Services;
using Blazor.LibraryExample.Shared.Entities;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Newtonsoft.Json;

using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Blazor.LibraryExample.ServerSideRendering
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
			services.AddRazorPages();
			services.AddServerSideBlazor();
			services.AddDbContext<LibraryContext>(options =>
			{
				options.UseSqlite(Configuration.GetValue<string>("Data:ConnectionString"));
			});
			services.AddHttpClient<IBookAgent, BookAgent>();
			services.AddHttpClient();
			services.AddScoped<IBooksRepository, BooksRepository>();
			services.AddSingleton<IBookService, BookService>();
			// services.AddTransient<IBookAgent, BookAgent>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, LibraryContext libraryContext)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapDefaultControllerRoute();
				endpoints.MapBlazorHub();
				endpoints.MapFallbackToPage("/_Host");
			});
			// Fill and create the database if does not exists.
			CreateAndSeedDatabase(libraryContext);
		}
		/// <summary>
		/// Creates and initializes the database with test records the first time the application starts  at first time.
		/// </summary>
		/// <param name="libraryContext">The library context.</param>
		private void CreateAndSeedDatabase(LibraryContext libraryContext)
		{
			if (!(libraryContext.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
			{
				libraryContext.Database.EnsureCreated();
				SeedDatabase(libraryContext);
			}
		}

		/// <summary>
		/// Initializes the database with test records the first time the application starts  at first time.
		/// </summary>
		/// <param name="libraryContext">The library context.</param>
		private void SeedDatabase(LibraryContext libraryContext)
		{
			string jsonString = File.ReadAllText(Path.Combine("JsonFiles", "books.json"), Encoding.UTF8);
			var books = JsonConvert.DeserializeObject<IEnumerable<Book>>(jsonString);
			foreach (var book in books)
			{
				libraryContext.Books.Add(book);
			}

			libraryContext.SaveChanges();
		}

	}
}
