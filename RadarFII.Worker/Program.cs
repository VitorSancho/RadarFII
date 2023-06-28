using System;
using Microsoft.Extensions.DependencyInjection;
using Coravel.Queuing.Interfaces;
using Coravel;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using RadarFIIWorker;
using RadarFII.Business;
using RadarFII.Service;
using RadarFII.Data.Repository;
using RadarFII.Business.Interfaces.Business;
using RadarFII.Business.Interfaces.Service;
using RadarFII.Business.Interfaces.Data;
using RadarFII.Service.RabbitMQ;

public class Program
{
    public static void Main(string[] args)
    {
        IHost host = CreateHostBuilder(args).Build();
        host.Services.UseScheduler(scheduler => {
            scheduler
                .Schedule<ColetaProventosFIIJob>()
                .Hourly().Weekday()
                .RunOnceAtStart();
        })
            .OnError(e => Console.WriteLine(e));
        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddScheduler();

                services.AddTransient<ColetaProventosFIIJob>();
                services.AddScoped<IColetaEventosFIIService, ColetaEventosFIIService>();
                services.AddScoped<IProventoFIIRepository, ProventoFIIRepository>();
                services.AddScoped<IColetaProventosFIIBusiness, ColetaProventosFIIBusiness>();
                services.AddScoped<IDBRepository, DBRepository>();
                services.AddScoped<IRabbitMQService, RabbitMQService>();
                
            })
            .ConfigureAppConfiguration((context, config) =>
            {
                //var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                //Console.WriteLine($"AMBIENTE USADO (ASPNETCORE_Environment) : {environmentName}");

                config
                    //.AddJsonFile("C:\\Users\\vitor\\OneDrive\\Documentos\\Desafios e Projetos\\RadarFII\\RadarFII\\RadarFII.Worker\\appsettings.json", optional: false, reloadOnChange: true);
                .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables("ASPNETCORE_");
            });
};
