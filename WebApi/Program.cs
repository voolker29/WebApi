
using WebApi.EndPoints;
using WebApi.Models;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

             // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            
            builder.Services.AddSingleton<List<Good>>();
            builder.Services.AddSingleton<List<Order>>();

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            new Seed().Fill(app.Services);

            app.UseHttpsRedirection();
            app.AddGoodEndPoints();
            app.AddOrderEndPoints();

            app.Run();
        }
    }
}
