using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Archangel.Tests.WebEditor.Common;
using Archangel.Tests.WebEditor.Common.Services.Logger;
using Archangel.Tests.WebEditor.Data;
using Archangel.Tests.WebEditor.Extensions;
using Archangel.Tests.WebEditor.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Archangel.Tests.WebEditor
{
    public class Startup
    {
        private IConfiguration Configuration;
        private readonly IHostingEnvironment _env;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{_env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            var config = new Configuration();
            Configuration.Bind(config);
            services.AddSingleton(x => config);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddScoped<WebEditorDbContext>(_ => new FakeDbContext(Configuration).GetFakeDbContext());
            //services.AddDbContext<WebEditorDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("WebEditorContext")));

            IoCContainerFactory.MapInterfaces(services, Configuration);
            services.AddCors();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Archangel.Tests.API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            var instrumentationKey = config.InstrumentationKey;
            services.AddApplicationInsightsTelemetry(x => { x.InstrumentationKey = instrumentationKey; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerService loggerService)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //ToDo Uncomment if need to use real db
                //try
                //{
                //    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                //        .CreateScope())
                //    {
                //        serviceScope.ServiceProvider.GetService<WebEditorDbContext>().Database.Migrate();
                //    }
                //}
                //catch (Exception exc)
                //{
                //    loggerService.LogException(exc);
                //}
            }

            app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Archangel.Tests.API V1");
            });
            app.UseAuthentication();
            app.ConfigureCustomMiddleware(loggerService);
            app.UseMvc();
        }
    }
}