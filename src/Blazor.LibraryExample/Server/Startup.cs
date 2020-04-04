using Blazor.LibraryExample.Server.Contracts.Repositories;
using Blazor.LibraryExample.Server.Data;
using Blazor.LibraryExample.Server.Repositories;
using Blazor.LibraryExample.Shared.Entities;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Newtonsoft.Json;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Blazor.LibraryExample.Server
{
	/// <summary>
	/// The startup class.
	/// </summary>
	public class Startup
	{
		private readonly IConfiguration configuration;

		/// <summary>
		/// Initializes a new instance of the <see cref="Startup"/> class.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		public Startup(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		/// <summary>
		/// This method gets called by the runtime. Use this method to add services to the container.
		/// </summary>
		/// <param name="services">The services.</param>
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
			services.AddDbContext<LibraryContext>(options =>
			{
				options.UseSqlite(configuration.GetValue<string>("Data:ConnectionString"));
			});
			services.AddScoped<IBooksRepository, BooksRepository>();
		}

		/// <summary>
		/// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		/// </summary>
		/// <param name="app">The application.</param>
		/// <param name="env">The env.</param>
		/// <param name="libraryContext">The library context.</param>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, LibraryContext libraryContext)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseWebAssemblyDebugging();
			}

			app.UseStaticFiles();
			app.UseBlazorFrameworkFiles();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapDefaultControllerRoute();
				endpoints.MapFallbackToFile("index.html");
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
