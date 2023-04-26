using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OrderServiceTest
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
                    });;
            
            
            builder.Services.AddScoped<OrderService>();
            builder.Services.AddScoped<OrderContext>();
            
            var app = builder.Build();

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();

          
        }
        
    }
    
    
}