using Blog.Common.Contracts.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using UrlShortener.Service.Url;
using UrlShortener.Service.Url.Interfaces;

namespace UrlShortener.Api
{
    public class Startup
    {
        private ProjectDto _projectMeta;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _projectMeta = new ProjectDto();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Load project's definition
            Configuration.GetSection("Project").Bind(_projectMeta);
            Configuration.GetSection("Project:Contact").Bind(_projectMeta.Contact);
            Configuration.GetSection("Project:License").Bind(_projectMeta.License);

            // Populate swagger UI
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(_projectMeta.ApiVersion, new Info
                {
                    Version = _projectMeta.Version,
                    Title = _projectMeta.Title,
                    Description = _projectMeta.Description,
                    TermsOfService = _projectMeta.TermsOfService,
                    Contact = new Contact
                    {
                        Name = _projectMeta.Contact.Name,
                        Email = _projectMeta.Contact.Email,
                        Url = _projectMeta.Contact.Url
                    },
                    License = new License
                    {
                        Name = _projectMeta.License.Name,
                        Url = _projectMeta.License.Url
                    }
                });
                c.EnableAnnotations();
            });

            // Dependency injection
            services.AddScoped<IUrlService, UrlService> ();
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", _projectMeta.Title);
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
