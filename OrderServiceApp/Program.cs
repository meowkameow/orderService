using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OrderEfRepository;
using OrdersBase;
using OrderServices;

namespace OrderServiceApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddNewtonsoftJson(
                options =>
                    {
                        options.SerializerSettings.Converters = new List<JsonConverter>
                        {
                            new StringEnumConverter(),
                        };
                    });

            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddDbContext<OrderContext>();
            
            var app = builder.Build();
            app.UseHttpsRedirection();
            app.MapControllers();
            app.Run();
        }
    }
}