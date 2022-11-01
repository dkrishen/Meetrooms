using MRA.Bookings.Data;
using MRA.Bookings.Models;
using MRA.Bookings.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using MRA.Bookings.Logic.RabbitMQ.Consumers;
using MRA.Bookings.Logic.SignalR;
using MRA.Bookings.Logic;

namespace MRA.Bookings
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var AuthOptions = Configuration.GetSection("Auth").Get<AuthOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = AuthOptions.Issuer;
                    options.Audience = AuthOptions.Audience;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                    };
                });

            services.AddDbContext<MRABookingsDbContext>(options =>
                options.UseMySql(
                    Configuration["Data:Database:ConnectionString"],
                    new MySqlServerVersion(new Version(8, 0, 30))
                    ));
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<IBookingLogic, BookingLogic>();
            services.AddSingleton<ISignalRClient, SignalRClient>();
            services.AddHostedService<RabbitMqAddListener>();
            services.AddHostedService<RabbitMqDeleteListener>();
            services.AddHostedService<RabbitMqUpdateListener>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookingApi", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookingApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
