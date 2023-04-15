﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Coravel.Queuing.Interfaces;
using Coravel;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using RadarFIIWorker;
using RadarFII.Service;
using RadarFII.Data.Interfaces;
using RadarFII.Data.Repository;
using RadarFII.Business.Intefaces;
using RadarFII.Business;

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
        host.Services.UseScheduler(scheduler => {
            scheduler
                .Schedule<ColetaProventosFIIJob>()
                .Hourly().Weekday()
                .RunOnceAtStart();
    });
        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                //var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                //Console.WriteLine($"AMBIENTE USADO (ASPNETCORE_Environment) : {environmentName}");

                config
                    .AddJsonFile("C:\\Users\\vitor\\OneDrive\\Documentos\\Desafios e Projetos\\RadarFII\\RadarFII\\RadarFII.Worker\\appsettings.json", optional: false, reloadOnChange: true);
                    //.AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                    //.AddEnvironmentVariables("ASPNETCORE_");
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddScheduler();
                // Add this 👇
                services.AddTransient<ColetaProventosFIIJob>();
                services.AddSingleton<IColetaEventosFIIService,ColetaEventosFIIService > ();
                services.AddSingleton<IProventoFIIRepository, ProventoFIIRepository>();
                services.AddSingleton<IColetaProventosFIIBusiness, ColetaProventosFIIBusiness>();
                //services.AddSingleton<PublicaProventosInstagramJob>();
            });
};

