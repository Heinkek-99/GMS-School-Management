using System.Windows.Forms;
using MediatR;
using GMS.Application.Common.Services;

namespace GMS.Desktop.Forms;

public class MainForm : Form
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;
    private MenuStrip menuStrip;
    private StatusStrip statusStrip;
    private ToolStripStatusLabel lblUser;
    private Panel panelContent;

    public MainForm(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.Text = "GMS - Gestion Scolaire";
        this.Size = new System.Drawing.Size(1400, 800);
        this.WindowState = FormWindowState.Maximized;
        this.StartPosition = FormStartPosition.CenterScreen;

        // Menu principal
        menuStrip = new MenuStrip
        {
            BackColor = System.Drawing.Color.FromArgb(37, 99, 235),
            ForeColor = System.Drawing.Color.White,
            Font = new System.Drawing.Font("Segoe UI", 10)
        };

        // Menu Accueil
        var menuAccueil = new ToolStripMenuItem("🏠 Accueil");
        menuAccueil.Click += (s, e) => ShowDashboard();

        // Menu Familles
        var menuFamilles = new ToolStripMenuItem("👨‍👩‍👧‍👦 Familles");
        menuFamilles.DropDownItems.Add("📋 Liste des familles", null, (s, e) => ShowFamilles());
        menuFamilles.DropDownItems.Add("➕ Nouvelle famille", null, (s, e) => AddFamille());

        // Menu Élèves
        var menuEleves = new ToolStripMenuItem("🎓 Élèves");
        menuEleves.DropDownItems.Add("📋 Liste des élèves", null, (s, e) => ShowEleves());
        menuEleves.DropDownItems.Add("➕ Nouvel élève", null, (s, e) => AddEleve());

        // Menu Finances
        var menuFinances = new ToolStripMenuItem("💰 Finances");
        menuFinances.DropDownItems.Add("💳 Paiements", null, (s, e) => ShowPaiements());
        menuFinances.DropDownItems.Add("📊 Rapports financiers", null, (s, e) => ShowRapports());

        // Menu Administration
        var menuAdmin = new ToolStripMenuItem("⚙️ Administration");
        menuAdmin.DropDownItems.Add("👥 Utilisateurs", null, (s, e) => ShowUtilisateurs());
        menuAdmin.DropDownItems.Add("📚 Classes", null, (s, e) => ShowClasses());
        menuAdmin.DropDownItems.Add("📝 Types de frais", null, (s, e) => ShowTypesFrais());

        // Menu Aide
        var menuAide = new ToolStripMenuItem("❓ Aide");
        menuAide.DropDownItems.Add("📖 Documentation", null, (s, e) => ShowDoc());
        menuAide.DropDownItems.Add("ℹ️ À propos", null, (s, e) => ShowAbout());

        // Déconnexion (à droite)
        var menuDeconnexion = new ToolStripMenuItem("🚪 Déconnexion");
        menuDeconnexion.Alignment = ToolStripItemAlignment.Right;
        menuDeconnexion.Click += (s, e) => Deconnexion();

        menuStrip.Items.AddRange(new ToolStripItem[] 
        { 
            menuAccueil, 
            menuFamilles, 
            menuEleves, 
            menuFinances, 
            menuAdmin, 
            menuAide,
            menuDeconnexion 
        });

        // Status bar
        statusStrip = new StatusStrip();
        lblUser = new ToolStripStatusLabel
        {
            Text = $"Connecté : {_currentUserService.NomComplet} ({_currentUserService.Role})",
            Spring = true,
            TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        };
        
        var lblDate = new ToolStripStatusLabel
        {
            Text = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm")
        };

        statusStrip.Items.AddRange(new ToolStripItem[] { lblUser, lblDate });

        // Panel de contenu
        panelContent = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = System.Drawing.Color.FromArgb(249, 250, 251)
        };

        // Ajouter les contrôles
        this.Controls.Add(panelContent);
        this.Controls.Add(statusStrip);
        this.Controls.Add(menuStrip);
        this.MainMenuStrip = menuStrip;

        // Afficher le dashboard par défaut
        ShowDashboard();
    }

    private void ShowDashboard()
    {
        panelContent.Controls.Clear();
        
        var lblWelcome = new Label
        {
            Text = $"Bienvenue {_currentUserService.NomComplet} !",
            Font = new System.Drawing.Font("Segoe UI", 24, System.Drawing.FontStyle.Bold),
            Location = new System.Drawing.Point(50, 50),
            AutoSize = true
        };

        panelContent.Controls.Add(lblWelcome);
    }

    private void ShowFamilles()
    {
        panelContent.Controls.Clear();
        
        var familleForm = new FamilleListForm(_mediator)
        {
            TopLevel = false,
            FormBorderStyle = FormBorderStyle.None,
            Dock = DockStyle.Fill
        };
        
        panelContent.Controls.Add(familleForm);
        familleForm.Show();
    }

    private void AddFamille()
    {
        var form = new FamilleFormDialog(_mediator, null);
        form.ShowDialog();
    }

    private void ShowEleves()
    {
        MessageBox.Show("Fonctionnalité à implémenter", "Information");
    }

    private void AddEleve()
    {
        MessageBox.Show("Fonctionnalité à implémenter", "Information");
    }

    private void ShowPaiements()
    {
        MessageBox.Show("Fonctionnalité à implémenter", "Information");
    }

    private void ShowRapports()
    {
        MessageBox.Show("Fonctionnalité à implémenter", "Information");
    }

    private void ShowUtilisateurs()
    {
        MessageBox.Show("Fonctionnalité à implémenter", "Information");
    }

    private void ShowClasses()
    {
        MessageBox.Show("Fonctionnalité à implémenter", "Information");
    }

    private void ShowTypesFrais()
    {
        MessageBox.Show("Fonctionnalité à implémenter", "Information");
    }

    private void ShowDoc()
    {
        MessageBox.Show("Documentation GMS v1.0", "Documentation");
    }

    private void ShowAbout()
    {
        MessageBox.Show(
            "GMS - Gestion Scolaire\nVersion 1.0.0\n\n" +
            "Système de gestion pour établissements scolaires\n" +
            "© 2024 - Tous droits réservés",
            "À propos",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information
        );
    }

    private void Deconnexion()
    {
        var result = MessageBox.Show(
            "Voulez-vous vraiment vous déconnecter ?",
            "Déconnexion",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );

        if (result == DialogResult.Yes)
        {
            _currentUserService.ClearUser();
            this.Close();
        }
    }
}