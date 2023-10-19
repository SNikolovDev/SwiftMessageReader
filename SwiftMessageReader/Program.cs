using NLog.Web;

using SwiftMessageReader.Data;
using SwiftMessageReader.Data.Interfaces;
using SwiftMessageReader.Services;
using SwiftMessageReader.Services.Interfaces;

namespace SwiftMessageReader
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication
            .CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<ISwiftService, SwiftService>();
            builder.Services.AddScoped<ISwiftRepository, SwiftRepository>();

            // NLog: Setup NLog for Dependency injection
            builder.Logging.ClearProviders();
            builder.Host.UseNLog();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();

            var databaseCreator = new DatabaseCreator(builder.Configuration);
            databaseCreator.Create();

            app.Run();
        }
    }
}