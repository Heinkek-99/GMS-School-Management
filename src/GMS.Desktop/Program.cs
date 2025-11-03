using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using GMS.Application;
using GMS.Infrastructure;
using WinForms = System.Windows.Forms;
using Microsoft.Extensions.Logging;

namespace GMS.Desktop;

static class Program
{
    /// <summary>
    /// Point d'entrée principal de l'application
    /// </summary>
    [STAThread]
    static void Main()
    {
        WinForms.Application.EnableVisualStyles();
        WinForms.Application.SetCompatibleTextRenderingDefault(false);

        // Configuration du Host
        var host = CreateHostBuilder().Build();

        // Récupérer le service provider
        var serviceProvider = host.Services;

        // Lancer le formulaire principal
        var mainForm = new WinForms.Form(); // Remplacer par votre formulaire principal
        mainForm.Text = "GMS - Gestion Scolaire";
        mainForm.Size = new System.Drawing.Size(800, 600);
        WinForms.Application.Run(mainForm);
    }

    static IHostBuilder CreateHostBuilder() =>
        Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(AppContext.BaseDirectory);
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                config.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", 
                    optional: true, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                // Logging
                services.AddLogging(builder =>
                {
                    builder.AddConsole();
                    builder.AddDebug();
                });
                
                // Enregistrer les services
                services.AddApplication();
                services.AddInfrastructure(context.Configuration);
            });
}