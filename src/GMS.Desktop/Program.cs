using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GMS.Application.Common.Services;
using GMS.Application;
using GMS.Infrastructure;
using GMS.Infrastructure.Data;
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
            // Initialisation de la base de données au démarrage
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<GmsDbContext>();
                var logger = services.GetRequiredService<ILogger<GmsDbContext>>();


                logger.LogInformation("Initialisation de la base de données...");

                context.Database.EnsureCreated();

                logger.LogInformation("Base de données initialisée avec succès.");
            }
        }

        catch (Exception ex)
        {
            WinForms.MessageBox.Show(
                $"Erreur d'initialisation de la base de données:\n{ex.Message}\n\n{ex.InnerException?.Message}",
                "Erreur",
                WinForms.MessageBoxButtons.OK,
                WinForms.MessageBoxIcon.Error
            );
            return;
        }

        try
        {
            host.Start();
            // Récupérer les services nécessaires
            var mediator = host.Services.GetRequiredService<MediatR.IMediator>();
            var currentUserService = host.Services.GetRequiredService<ICurrentUserService>();

            // Afficher le formulaire de login
            var loginForm = new GMS.Desktop.Forms.LoginForm(mediator, currentUserService);

            if (loginForm.ShowDialog() == WinForms.DialogResult.OK)
            {
                // L'utilisateur est authentifié - Rediriger selon le rôle
                Form dashboardForm = currentUserService.Role switch
                {
                    "Admin" => new GMS.Desktop.Forms.Dashboards.AdminDashboard(mediator, currentUserService),
                    "Directeur" => new GMS.Desktop.Forms.Dashboards.DirecteurDashboard(mediator, currentUserService),
                    "Secretaire" => new GMS.Desktop.Forms.Dashboards.SecretaireDashboard(mediator, currentUserService),
                    "Comptable" => new GMS.Desktop.Forms.Dashboards.ComptableDashboard(mediator, currentUserService),
                    _ => new WinForms.Form() // Fallback
                };

                // L'utilisateur est authentifié
                // var mainForm = new WinForms.Form();
                // mainForm.Text = $"GMS - Bienvenue {currentUserService.NomComplet} ({currentUserService.Role})";
                // mainForm.Size = new System.Drawing.Size(1200, 700);
                // mainForm.WindowState = WinForms.FormWindowState.Maximized;

                // // Ajouter un label de bienvenue
                // var welcomeLabel = new WinForms.Label
                // {
                //     Text = $"✅ Connecté en tant que: {currentUserService.NomComplet}\n" +
                //            $"Rôle: {currentUserService.Role}",
                //     AutoSize = true,
                //     Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold),
                //     Location = new System.Drawing.Point(20, 20)
                // };
                // mainForm.Controls.Add(welcomeLabel);

                // WinForms.Application.Run(mainForm);
                WinForms.Application.Run(dashboardForm);
            }

        }
        catch (Exception ex)
        {
            WinForms.MessageBox.Show(
                $"Erreur au démarrage de l'application:\n{ex.Message}\n\n{ex.InnerException?.Message}",
                "Erreur",
                WinForms.MessageBoxButtons.OK,
                WinForms.MessageBoxIcon.Error
            );
        } 
    }

    static IHostBuilder CreateHostBuilder() =>
        Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(AppContext.BaseDirectory);
                config.AddJsonFile("appsettings.json", optional: 
                false, reloadOnChange: true);
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
                    builder.SetMinimumLevel(LogLevel.Information);
                });
                
                // Enregistrer les services
                services.AddApplication();
                services.AddInfrastructure(context.Configuration);
            });
}