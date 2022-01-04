using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleChat.API.Hubs;
using SimpleChat.API.Mapper;
using SimpleChat.BusinessLogic;
using System.Collections.Generic;

namespace SimpleChat.API
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
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddSignalR();
            services.AddCors(options =>
            {
                options.AddPolicy(name: "ClientApp", builder =>
                {
                    builder.WithOrigins("https://localhost:44393")
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("ClientApp");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<RoomsHub>("/hubs/rooms");
                endpoints.MapHub<MessagesHub>("/hubs/messages");
            });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });            
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<BusinessLogicModule>();
            builder.Register(ctx => new MapperConfiguration(configuration =>
            {
                configuration.AddProfile<ModelMapper>();

                var profiles = ctx.Resolve<IEnumerable<Profile>>();

                foreach (var profile in profiles)
                {
                    configuration.AddProfile(profile);
                }

            })).AsSelf().SingleInstance();
        }
    }
}
