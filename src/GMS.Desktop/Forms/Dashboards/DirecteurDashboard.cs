// GMS.Desktop/Forms/Dashboards/DirecteurDashboard.cs
using System.Windows.Forms;
using MediatR;
using GMS.Application.Common.Services;

namespace GMS.Desktop.Forms.Dashboards;

public class DirecteurDashboard : Form
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public DirecteurDashboard(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.Text = $"GMS - Dashboard Directeur - {_currentUserService.NomComplet}";
        this.Size = new System.Drawing.Size(1400, 800);
        this.WindowState = FormWindowState.Maximized;
        this.StartPosition = FormStartPosition.CenterScreen;

        var panelTop = new Panel
        {
            Dock = DockStyle.Top,
            Height = 80,
            BackColor = System.Drawing.Color.FromArgb(16, 185, 129)
        };

        var lblWelcome = new Label
        {
            Text = $"👋 Bienvenue, {_currentUserService.NomComplet}",
            Location = new System.Drawing.Point(30, 20),
            AutoSize = true,
            Font = new System.Drawing.Font("Segoe UI", 18, System.Drawing.FontStyle.Bold),
            ForeColor = System.Drawing.Color.White
        };

        var lblRole = new Label
        {
            Text = $"🎯 Rôle: Directeur",
            Location = new System.Drawing.Point(30, 50),
            AutoSize = true,
            Font = new System.Drawing.Font("Segoe UI", 12),
            ForeColor = System.Drawing.Color.FromArgb(220, 255, 220)
        };

        var btnLogout = new Button
        {
            Text = "🚪 Déconnexion",
            Location = new System.Drawing.Point(1200, 20),
            Size = new System.Drawing.Size(150, 40),
            BackColor = System.Drawing.Color.FromArgb(239, 68, 68),
            ForeColor = System.Drawing.Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand
        };
        btnLogout.FlatAppearance.BorderSize = 0;
        btnLogout.Click += (s, e) => {
            if (MessageBox.Show("Voulez-vous vraiment vous déconnecter ?", "Confirmation", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _currentUserService.ClearUser();
                this.Close();
            }
        };

        panelTop.Controls.AddRange(new Control[] { lblWelcome, lblRole, btnLogout });

        var panelMain = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = System.Drawing.Color.FromArgb(249, 250, 251),
            Padding = new Padding(30)
        };

        var lblTitle = new Label
        {
            Text = "📊 Tableau de bord - Vue Direction",
            Location = new System.Drawing.Point(30, 20),
            AutoSize = true,
            Font = new System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold)
        };

        var cardEleves = CreateCard("🎓 Élèves", "0", "Élèves inscrits", 30, 80);
        var cardProfesseurs = CreateCard("👨‍🏫 Enseignants", "0", "Personnel enseignant", 330, 80);
        var cardClasses = CreateCard("📚 Classes", "12", "Classes actives", 630, 80);
        var cardFinances = CreateCard("💰 Finances", "0 FCFA", "Encaissements du mois", 930, 80);

        var lblActions = new Label
        {
            Text = "⚡ Actions rapides",
            Location = new System.Drawing.Point(30, 280),
            AutoSize = true,
            Font = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold)
        };

        var btnRapports = CreateActionButton("📊 Rapports", 30, 320);
        var btnGestionEleves = CreateActionButton("🎓 Gestion Élèves", 330, 320);
        var btnGestionClasses = CreateActionButton("📚 Gestion Classes", 630, 320);
        var btnStatistiques = CreateActionButton("📈 Statistiques", 930, 320);

        panelMain.Controls.AddRange(new Control[] {
            lblTitle, cardEleves, cardProfesseurs, cardClasses, cardFinances,
            lblActions, btnRapports, btnGestionEleves, btnGestionClasses, btnStatistiques
        });

        this.Controls.Add(panelMain);
        this.Controls.Add(panelTop);
    }

    private Panel CreateCard(string title, string value, string subtitle, int x, int y)
    {
        var card = new Panel
        {
            Location = new System.Drawing.Point(x, y),
            Size = new System.Drawing.Size(280, 150),
            BackColor = System.Drawing.Color.White,
            BorderStyle = BorderStyle.FixedSingle
        };

        var lblTitle = new Label
        {
            Text = title,
            Location = new System.Drawing.Point(20, 20),
            AutoSize = true,
            Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold),
            ForeColor = System.Drawing.Color.FromArgb(16, 185, 129)
        };

        var lblValue = new Label
        {
            Text = value,
            Location = new System.Drawing.Point(20, 55),
            AutoSize = true,
            Font = new System.Drawing.Font("Segoe UI", 24, System.Drawing.FontStyle.Bold)
        };

        var lblSubtitle = new Label
        {
            Text = subtitle,
            Location = new System.Drawing.Point(20, 110),
            AutoSize = true,
            Font = new System.Drawing.Font("Segoe UI", 10),
            ForeColor = System.Drawing.Color.Gray
        };

        card.Controls.AddRange(new Control[] { lblTitle, lblValue, lblSubtitle });
        return card;
    }

    private Button CreateActionButton(string text, int x, int y)
    {
        var btn = new Button
        {
            Text = text,
            Location = new System.Drawing.Point(x, y),
            Size = new System.Drawing.Size(280, 60),
            Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold),
            BackColor = System.Drawing.Color.White,
            ForeColor = System.Drawing.Color.FromArgb(16, 185, 129),
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand,
            TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
            Padding = new Padding(20, 0, 0, 0)
        };
        btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(200, 200, 200);
        btn.Click += (s, e) => MessageBox.Show($"Fonctionnalité '{text}' à venir", "Information");
        return btn;
    }
}