using GreenPipes;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using PaymentConsumer.Models;
using PaymentConsumer.Services;
using PaymentConsumer.Consumers;
using Shared.Model;
using System;
using Microsoft.EntityFrameworkCore;
using PaymentConsumer.Context;

namespace PaymentConsumer
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
            services.AddMassTransit(x =>
            {
                x.AddConsumer< OrderPaymentConsumer>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.UseHealthCheck(provider);
                    cfg.Host(new Uri("rabbitmq://localhost"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ReceiveEndpoint("PaymentTicketQueue", ep =>
                    {
                        ep.PrefetchCount = 10;
                        ep.UseMessageRetry(r => r.Interval(2, 100));
                        ep.ConfigureConsumer<OrderPaymentConsumer>(provider);
                    });
                }));
            });
            services.Configure<PaymentsDatabaseSettings>(Configuration.GetSection(nameof(PaymentsDatabaseSettings)));

            services.AddSingleton<IPaymentsDatabaseSettings>(sp => sp.GetRequiredService<IOptions<PaymentsDatabaseSettings>>().Value);

            services.AddSingleton<PaymentsService>();
            services.AddMassTransitHostedService();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PaymentConsumer", Version = "v1" });
            });
            var connection = @"Server=(localdb)\mssqllocaldb;Database=PaymentDB;Trusted_Connection=True;";
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PaymentConsumer v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
