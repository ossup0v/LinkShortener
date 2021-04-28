using LinkShortener.Configs;
using LinkShortener.DataAccessLayer;
using LinkShortener.Interfaces;
using LinkShortener.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace LinkShortener
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<DatabaseSettings>(Configuration.GetSection(nameof(DatabaseSettings)));
			services.AddSingleton<IDatabaseSettings>(x => x.GetRequiredService<IOptions<DatabaseSettings>>().Value);
			services.AddTransient<ILinkShortenerService, LinkShortenerService>();
			services.AddTransient<ILinkShortenerStorage, LinkShortenerStorage>();
			services.AddControllers();
			services.AddMvcCore(options =>
			{
				options.EnableEndpointRouting = false;
			});
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "LinkShortener", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LinkShortener v1"));
			}

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthorization();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
						name: "default",
						template: "{controller=LinkShortener}/{action=GetFullLink}/{token?}");
			});
		}
	}
}
