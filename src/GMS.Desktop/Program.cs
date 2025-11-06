using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using GMS.Application;
using GMS.Infrastructure;
using GMS.Infrastructure.Data;
using GMS.Application.Common.Services; // ✅ CORRIGER ICI
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
        
        try
        {
            // Seed data au démarrage
            SeedData.InitializeAsync(host.Services).GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            WinForms.MessageBox.Show(
                $"Erreur d'initialisation de la base de données:\n{ex.Message}",
                "Erreur",
                WinForms.MessageBoxButtons.OK,
                WinForms.MessageBoxIcon.Error
            );
            return;
        }

        // Récupérer les services nécessaires
        var mediator = host.Services.GetRequiredService<MediatR.IMediator>();
        var currentUserService = host.Services.GetRequiredService<ICurrentUserService>(); // ✅ CORRIGER ICI

        // Afficher le formulaire de login
        var loginForm = new GMS.Desktop.Forms.LoginForm(mediator, currentUserService);

        if (loginForm.ShowDialog() == WinForms.DialogResult.OK)
        {
            // L'utilisateur est authentifié
            var mainForm = new WinForms.Form();
            mainForm.Text = $"GMS - Bienvenue {currentUserService.NomComplet} ({currentUserService.Role})";
            mainForm.Size = new System.Drawing.Size(1200, 700);
            mainForm.WindowState = WinForms.FormWindowState.Maximized;
            WinForms.Application.Run(mainForm);
        }
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