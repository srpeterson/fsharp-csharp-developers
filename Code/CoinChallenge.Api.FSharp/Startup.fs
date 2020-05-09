namespace CoinChallenge.Api.FSharp

open System
open System.IO;
open System.Reflection;
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.OpenApi.Models

type Startup private () =
    new (configuration: IConfiguration) as me = //prefer 'me' to 'this' to distance from C# 'this'
        Startup() then
        me.Configuration <- configuration

    // This method gets called by the runtime. Use this method to add services to the container.
    member _.ConfigureServices (services: IServiceCollection) =

        // Add framework services.
        services.AddControllers() 
            // prevents 'Microsoft.AspNetCore.Mvc.ProblemDetails' schema from generating for 400 responses
            // when type of is not set ex: [<ProducesResponseType(StatusCodes.StatusCodes.Status400BadRequest)>]
            // instead of [<ProducesResponseType(typeof<StandardResponse>, StatusCodes.Status400BadRequest)>]
            .ConfigureApiBehaviorOptions (fun options -> options.SuppressMapClientErrors <- true) |> ignore

        //create contact
        let contact = new OpenApiContact (
                        Name= "Steve Peterson", 
                        Email = "srpeterson@outlook.com", 
                        Url = new Uri("https://www.linkedin.com/in/stephenrpeterson/") )
           
        //create license
        let license = new OpenApiLicense (
                        Name = "Steve Peterson", 
                        Url = new Uri("https://www.linkedin.com/in/stephenrpeterson/"))

        //set path
        let xmlFile = sprintf "%s.xml" (Assembly.GetExecutingAssembly().GetName().Name) 
        let xmlPath = Path.Combine (AppContext.BaseDirectory, xmlFile);

        services.AddSwaggerGen(fun config ->
             config.SwaggerDoc (
                "v1",  
                new OpenApiInfo(Version="v1", 
                    Title = "F# Coin Challenge Web API", 
                    Description = "The F# Swagger Coin Challenge Web API", 
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = contact,
                    License = license)); 
                config.IncludeXmlComments (xmlPath); 
                config.CustomSchemaIds (fun x -> x.FullName)) |> ignore


    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member _.Configure(app: IApplicationBuilder, env: IWebHostEnvironment) =
        if (env.IsDevelopment()) then
            app.UseDeveloperExceptionPage() |> ignore

        app.UseHttpsRedirection() |> ignore
        app.UseRouting() |> ignore

        //swagger
        app.UseSwagger() |> ignore
        app.UseSwaggerUI (fun config -> config.SwaggerEndpoint("/swagger/v1/swagger.json", "F# Coin Challenge V1")) |> ignore

        app.UseAuthorization() |> ignore

        app.UseEndpoints(fun endpoints ->
            endpoints.MapControllers() |> ignore
            ) |> ignore

    member val Configuration : IConfiguration = null with get, set
