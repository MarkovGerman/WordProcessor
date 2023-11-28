using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using WordProcessorApp.Parsers;
using WordProcessorApp.Repositories;
using WordProcessorApp.Services;

namespace WordProcessorApp
{
    public static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            ApplicationConfiguration.Initialize();
            var host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;
            var form = ServiceProvider.GetRequiredService<Form1>();
            Application.Run(form);
        }
        public static IServiceProvider ServiceProvider { get; private set; }
        /// <summary>
        /// Create a host builder to build the service provider
        /// </summary>
        /// <returns></returns>
        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddScoped<IRepository, Repository>();
                    services.AddScoped<IParser, Parser>();
                    services.AddScoped<IMessageService, MessageService>();
                    services.AddScoped<IDictionaryService, DictionaryService>();
                    services.AddSingleton<Form1>();
                    var path = Directory.GetCurrentDirectory();
                    var serilogLogger = new LoggerConfiguration()
                                    .WriteTo.File($"{path}\\Logger\\logging.txt")
                                    .CreateLogger();
                    services.AddLogging(x =>
                    {
                        x.SetMinimumLevel(LogLevel.Information);
                        x.AddSerilog(serilogLogger, dispose: true);
                    });

                });
        }
    }
}