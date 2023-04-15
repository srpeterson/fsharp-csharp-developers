using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Api.OptimalCoins.Sharp.Services;

namespace Api.OptimalCoins.Sharp; 

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
            // prevents 'Microsoft.AspNetCore.Mvc.ProblemDetails' schema from generating for 400 responses
            // when type of is not set ex: [ProducesResponseType(StatusCodes.Status400BadRequest)]
            // instead of [ProducesResponseType(typeof(StandardResponse), StatusCodes.Status400BadRequest)]
            .ConfigureApiBehaviorOptions(options => options.SuppressMapClientErrors = true);

        //di
        services.AddScoped<ICoinService, CoinService>();
       
        // Register the Swagger
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "C# Coin Challenge Web API",
                Description = "The C# Swagger Coin Challenge Web API",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Steve Peterson",
                    Email = "srpeterson@outlook.com",
                    Url = new Uri("https://www.linkedin.com/in/stephenrpeterson/")
                },
                License = new OpenApiLicense
                {
                    Name = "License Terms",
                    Url = new Uri("https://example.com/license")
                }
            });

            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        // Enable middleware to serve generated Swagger as a JSON endpoint.
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "C# Coin Challenge V1");
        });

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
