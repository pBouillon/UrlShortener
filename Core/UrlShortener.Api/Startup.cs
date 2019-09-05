using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using UrlShortener.Common.Contracts.Configuration;
using UrlShortener.Service.Url;
using UrlShortener.Service.Url.Interfaces;

namespace UrlShortener.Api
{
    public class Startup
    {
        public readonly ProjectDto ProjectMeta;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ProjectMeta = new ProjectDto();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Load project's definition
            Configuration.GetSection("Project").Bind(ProjectMeta);
            Configuration.GetSection("Project:Contact").Bind(ProjectMeta.Contact);
            Configuration.GetSection("Project:License").Bind(ProjectMeta.License);

            // Populate swagger UI
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(ProjectMeta.ApiVersion, new Info
                {
                    Version = ProjectMeta.Version,
                    Title = ProjectMeta.Title,
                    Description = ProjectMeta.Description,
                    TermsOfService = ProjectMeta.TermsOfService,
                    Contact = new Contact
                    {
                        Name = ProjectMeta.Contact.Name,
                        Email = ProjectMeta.Contact.Email,
                        Url = ProjectMeta.Contact.Url
                    },
                    License = new License
                    {
                        Name = ProjectMeta.License.Name,
                        Url = ProjectMeta.License.Url
                    }
                });
                c.EnableAnnotations();
            });

            // Dependency injection
            services.AddScoped<IUrlService, UrlService> ();
            services.AddScoped<IDal, Dal>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            // Enable swagger middleware
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", ProjectMeta.Title);
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
