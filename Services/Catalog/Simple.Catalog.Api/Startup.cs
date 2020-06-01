namespace Simple.Catalog.Api
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Simple.Catalog.Api.Domain.Context;
    using Simple.Core;
    using MediatR;
    using AutoMapper;
    using System.IdentityModel.Tokens.Jwt;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;
    using System;
    using Serilog;
    using Simple.Catalog.Api.Infrastructure.Filters;
    using System.IO;

    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Logger(log => log.Filter.ByIncludingOnly("@Level = 'Information'")
                    .WriteTo.RollingFile(
                        pathFormat: "Logs//Informations//log-information-{Date}.txt",
                        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
                    ))
                .WriteTo.Logger(log => log.Filter.ByIncludingOnly("@Level = 'Warning'")
                    .WriteTo.RollingFile(
                        pathFormat: "Logs//Warnings//log-warning-{Date}.txt",
                        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
                    ))
                .WriteTo.Logger(log => log.Filter.ByIncludingOnly("@Level = 'Error'")
                    .WriteTo.RollingFile(
                        pathFormat: "Logs//Errors//log-error-{Date}.txt",
                        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
                    ))
                .CreateLogger();
        }

        public static IConfigurationRoot Configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(
                options =>
                {
                    options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                })
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            services.AddSingleton(Configuration);

            // Entity framework
            string msSqlConnectionString = Configuration.GetValue<string>("database:msSql:connectionString");
            services.AddDbContext<AppCatalogDbContext>(opt =>
                opt.UseSqlServer(
                    msSqlConnectionString,
                    options =>
                    {
                        options.EnableRetryOnFailure();
                    }));

            services.AddMediatR(ReflectionUtilities.GetAssemblies());
            services.AddAutoMapper(ReflectionUtilities.GetAssemblies());

            // ===== Add Jwt Authentication ========
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["JwtIssuer"],
                        ValidAudience = Configuration["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            // Logging
            services.AddSingleton(Log.Logger);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // auto migration
            this.RunMigration(app);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        private void RunMigration(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<AppCatalogDbContext>().Database.Migrate();
            }
        }
    }
}
