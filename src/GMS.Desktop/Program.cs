<<<<<<< Updated upstream
=======
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using GMS.Application;
using GMS.Infrastructure;
using GMS.Application.Common.Services; 
using WinForms = System.Windows.Forms;
using Microsoft.Extensions.Logging;
using GMS.Infrastructure.Data;


>>>>>>> Stashed changes
namespace GMS.Desktop;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
<<<<<<< Updated upstream
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());
=======
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
        var currentUserService = host.Services.GetRequiredService<ICurrentUserService>();

        // Afficher le formulaire de login
        var loginForm = new GMS.Desktop.Forms.LoginForm(mediator, currentUserService);

        if (loginForm.ShowDialog() == WinForms.DialogResult.OK)
        {
            // L'utilisateur est authentifié, continuer avec l'application principale
            var mainForm = new WinForms.Form();
            mainForm.Text = $"GMS - Bienvenue {currentUserService.NomComplet} ({currentUserService.Role})";
            mainForm.Size = new System.Drawing.Size(1200, 700);
            mainForm.WindowState = WinForms.FormWindowState.Maximized;
            WinForms.Application.Run(mainForm);
        }
        else
        {
            // L'utilisateur a annulé la connexion, fermer l'application
            return;
        }
        
        // // Récupérer le service provider
        // var serviceProvider = host.Services;

        // Lancer le formulaire principal
        // var mainForm = new WinForms.Form(); // Remplacer par votre formulaire principal
        // mainForm.Text = "GMS - Gestion Scolaire";
        // mainForm.Size = new System.Drawing.Size(800, 600);
        // WinForms.Application.Run(mainForm);
>>>>>>> Stashed changes
    }
}