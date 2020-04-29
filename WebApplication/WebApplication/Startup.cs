using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using WebApplication.Filters;
using WebApplication.MiddlewareExtensions;
using WebApplication.Middlewares;
using WebApplication.Validation;

namespace WebApplication
{
    public class Startup
    {
        readonly string CorsPolicy = "WebApplication";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(options => options.AddPolicy(CorsPolicy,
             builder =>
                 builder.WithOrigins()
                     .WithMethods("GET", "POST", "PUT", "OPTIONS")
                     .AllowAnyHeader()
              ));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();

            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
        .AddJwtBearer(options =>
        {
            //options.TokenValidationParameters = CertificateService.GetJwtOptions(projectConfiguration).TokenValidationParameters;
            //options.SaveToken = true;
            //options.RequireHttpsMetadata = false;
        });

            //inject validator
            services.AddSingleton<AbstractValidator<WeatherForecastDto>, WeatherForecastValidator>();
            services.AddSingleton<IValidator<WeatherForecastDto>, WeatherForecastValidator>();

            services.AddMvc(options =>
            {
                options.Filters.Add(new ResponseFilter());
                options.Filters.Add<CustomExceptionFilter>();
            }).AddFluentValidation(fv => 
                    {
                        fv.RegisterValidatorsFromAssemblyContaining<Startup>();
                        //fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                        //fv.ImplicitlyValidateChildProperties = true;
                    });;

            //register the swagger generator,
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //shortc circuit middleware example
            //app.Run(async context => {
            //    await context.Response.WriteAsync("Hello World!");            
            //});

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
           // app.UseStaticFiles();
           
            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthorization();

            app.UseLogRequestsMiddleware();
            app.UseRefreshTokenMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseRequestCultureMiddleware();

            //Short-circuits middlewares

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync(
            //        $"Hello {CultureInfo.CurrentCulture.DisplayName}");
            //});

            if (!env.IsProduction())
            {
                var entryAssembly = Assembly.GetEntryAssembly();
                app.Run(async x =>
                {
                    var text = $"{entryAssembly.GetName().Name} Under Construction!";
                    Console.WriteLine(text);
                    await x.Response.WriteAsync(text);
                });
            }

        }
    }
}
