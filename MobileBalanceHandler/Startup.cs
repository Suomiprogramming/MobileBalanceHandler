using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MobileBalanceHandler.Extensions;
using MobileBalanceHandler.Models;
using MobileBalanceHandler.Models.Data;

namespace MobileBalanceHandler
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "MobileBalanceHandler", Version = "v1"});
            });
            string connection = Configuration.GetConnectionString("Connection");
            services.AddDbContext<MobileBalanceContext>(o => o.UseSqlite(connection));
            services.AddOptions<List<Carrier>>().Bind(Configuration.GetSection(nameof(Carrier)))
                .ValidateDataAnnotations();
            services.AddHttpClient();
            services.AddServices();
            services.AddLocalization();
            services.AddRequestLocalization(o =>
            {
                o.DefaultRequestCulture = new RequestCulture("ru-RU");
                o.ApplyCurrentCultureToResponseHeaders = true;
                o.SupportedCultures = new List<CultureInfo>()
                {
                    new CultureInfo("ru-RU"),
                    new CultureInfo("kk-KZ")
                };
                o.SupportedUICultures = new List<CultureInfo>()
                {
                    new CultureInfo("ru-RU"),
                    new CultureInfo("kk-KZ")
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MobileBalanceHandler v1"));
            }

            app.UseRequestLocalization();
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}