namespace Blazor.LibraryExample.Server
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Net.Mime;
	using System.Text;
	using Blazor.LibraryExample.Server.Contracts.Repositories;
	using Blazor.LibraryExample.Server.Data;
	using Blazor.LibraryExample.Server.Repositories;
	using Blazor.LibraryExample.Shared.Entities;
	using Microsoft.AspNetCore.Blazor.Server;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.ResponseCompression;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Infrastructure;
	using Microsoft.EntityFrameworkCore.Storage;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Serialization;

	/// <summary>
	/// The Startup class
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
		/// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		/// </summary>
		/// <param name="services">The services.</param>
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();

			services.AddResponseCompression(options =>
			{
				options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
				{
					MediaTypeNames.Application.Octet,
					WasmMediaTypeNames.Application.Wasm,
				});
			});
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
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, LibraryContext libraryContext)
		{
			app.UseResponseCompression();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseMvc(routes =>
			{
				routes.MapRoute(name: "default", template: "{controller}/{action}/{id?}");
			});

			CreateAndSeedDatabase(libraryContext);

			app.UseBlazor<Client.Program>();
		}

		/// <summary>
		/// Creates and initializes the database with test records the first time the application starts  at first time
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
		/// Initializes the database with test records the first time the application starts  at first time
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