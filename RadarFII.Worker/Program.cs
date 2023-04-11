﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Coravel.Queuing.Interfaces;
using Coravel;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using RadarFIIWorker;


public class Program
{
    public static void Main(string[] args)
    {
        // Changed to return the IHost
        // builder before running it.
        IHost host = CreateHostBuilder(args).Build();
        //////host.Services.UseScheduler(scheduler => {
        //////    // Easy peasy 👇
        //////    scheduler
        //////        .Schedule<ColetaProventosFIIWorker>()
        //////        .EveryFiveSeconds()
        //////        .Weekday();
        //////});
        host.Services.UseScheduler(scheduler =>
            scheduler
                .Schedule<ColetaProventosFIIJob>()
                .EveryMinute()
    );
        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                //var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                //Console.WriteLine($"AMBIENTE USADO (ASPNETCORE_Environment) : {environmentName}");

                config
                    .AddJsonFile("Configs/appsettings.json", optional: false, reloadOnChange: true);
                    //.AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                    //.AddEnvironmentVariables("ASPNETCORE_");
            })
            .ConfigureServices(services =>
            {
                services.AddScheduler();
                // Add this 👇
                services.AddSingleton<ColetaProventosFIIJob>();
                //services.AddSingleton<PublicaProventosInstagramJob>();
            });
};

