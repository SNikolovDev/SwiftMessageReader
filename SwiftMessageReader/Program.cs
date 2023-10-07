using NLog.Web;
using NLog;
using System.Data.Entity;
using SwiftMessageReader.Data;

namespace SwiftMessageReader
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Setting up logger
            //var logger = LogManager.Setup()
            //    .LoadConfigurationFromAppSettings()
            //    .GetCurrentClassLogger();

            //logger.Debug("init main");

            var builder = WebApplication
                .CreateBuilder(args);

            // Add services to the container.


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

            //var databaseInitializer = new DataBaseCreater(builder.Configuration);
            //databaseInitializer.Create();

            app.Run();
        }
    }
}