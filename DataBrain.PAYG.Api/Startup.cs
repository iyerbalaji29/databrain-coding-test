using DataBrain.PAYG.Api.Filters;
using DataBrain.PAYG.Api.Middleware;
using DataBrain.PAYG.Service.Services;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using System.Text.Json.Serialization;

namespace DataBrain.PAYG.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console()
                .CreateLogger();

            // Add Serilog to the logging pipeline
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog();
            });

            services.AddControllers().AddJsonOptions(options =>
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PAYG API", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
                    $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
                c.SchemaFilter<EnumSchemaFilter>();
            });

            // Call a separate method to register services
            RegisterServices(services);
        }

        private void RegisterServices(IServiceCollection services)
        {
            // Register your services here
            services.AddScoped<IPAYGService, PAYGService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure logging
            app.UseSerilogRequestLogging();

            // Use the custom middleware for exception handling
            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the Swagger documentation API
            if (_env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PAYG API v1");
                    c.DocExpansion(DocExpansion.List);
                });
            }

            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();
            app.UseRouting();

            //Added CORS to allow every origin, method and header. This will bypass any CORS from client side
            //This needs to be in between UseRouting() and UseEndpoint() middleware for CORS to work successfully
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
