// GMS.Desktop/Forms/Dashboards/ComptableDashboard.cs
using System.Windows.Forms;
using MediatR;
using GMS.Application.Common.Services;

namespace GMS.Desktop.Forms.Dashboards;

public class ComptableDashboard : Form
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public ComptableDashboard(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.Text = $"GMS - Dashboard Comptabilité - {_currentUserService.NomComplet}";
        this.Size = new System.Drawing.Size(1400, 800);
        this.WindowState = FormWindowState.Maximized;
        this.StartPosition = FormStartPosition.CenterScreen;

        var panelTop = new Panel
        {
            Dock = DockStyle.Top,
            Height = 80,
            BackColor = System.Drawing.Color.FromArgb(139, 92, 246)
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
            Text = $"💰 Rôle: Comptable",
            Location = new System.Drawing.Point(30, 50),
            AutoSize = true,
            Font = new System.Drawing.Font("Segoe UI", 12),
            ForeColor = System.Drawing.Color.FromArgb(230, 220, 255)
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
            Text = "💰 Gestion Financière et Comptabilité",
            Location = new System.Drawing.Point(30, 20),
            AutoSize = true,
            Font = new System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold)
        };

        var cardEncaissements = CreateCard("💵 Encaissements", "0 FCFA", "Ce mois", 30, 80);
        var cardImpayés = CreateCard("⚠️ Impayés", "0 FCFA", "À recouvrer", 330, 80);
        var cardPaiements = CreateCard("📊 Paiements", "0", "Transactions ce mois", 630, 80);
        var cardSoldes = CreateCard("📈 Soldes", "0 FCFA", "Solde global", 930, 80);

        var lblActions = new Label
        {
            Text = "⚡ Actions rapides",
            Location = new System.Drawing.Point(30, 280),
            AutoSize = true,
            Font = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold)
        };

        var btnEnregistrerPaiement = CreateActionButton("💳 Enregistrer Paiement", 30, 320);
        btnEnregistrerPaiement.Click += (s, e) => MessageBox.Show("Fonctionnalité à venir", "Information");

        var btnHistoriquePaiements = CreateActionButton("📋 Historique Paiements", 330, 320);
        btnHistoriquePaiements.Click += (s, e) => MessageBox.Show("Fonctionnalité à venir", "Information");

        var btnRapportsFinanciers = CreateActionButton("📊 Rapports Financiers", 630, 320);
        btnRapportsFinanciers.Click += (s, e) => MessageBox.Show("Fonctionnalité à venir", "Information");

        var btnFamilles = CreateActionButton("👨‍👩‍👧‍👦 Voir Familles", 930, 320);
        btnFamilles.Click += (s, e) => ShowFamillesList();

        panelMain.Controls.AddRange(new Control[] {
            lblTitle, cardEncaissements, cardImpayés, cardPaiements, cardSoldes,
            lblActions, btnEnregistrerPaiement, btnHistoriquePaiements, btnRapportsFinanciers, btnFamilles
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
            ForeColor = System.Drawing.Color.FromArgb(139, 92, 246)
        };

        var lblValue = new Label
        {
            Text = value,
            Location = new System.Drawing.Point(20, 55),
            AutoSize = true,
            Font = new System.Drawing.Font("Segoe UI", 20, System.Drawing.FontStyle.Bold)
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
            ForeColor = System.Drawing.Color.FromArgb(139, 92, 246),
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand,
            TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
            Padding = new Padding(20, 0, 0, 0)
        };
        btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(200, 200, 200);
        return btn;
    }

    private void ShowFamillesList()
    {
        var form = new FamilleListForm(_mediator);
        form.ShowDialog();
    }
}