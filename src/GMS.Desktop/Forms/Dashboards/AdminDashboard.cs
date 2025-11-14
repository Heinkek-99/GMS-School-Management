using System.Windows.Forms;
using MediatR;
using GMS.Application.Common.Services;

namespace GMS.Desktop.Forms.Dashboards;

public class AdminDashboard : Form
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public AdminDashboard(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.Text = $"GMS - Dashboard Administrateur - {_currentUserService.NomComplet}";
        this.Size = new System.Drawing.Size(1400, 800);
        this.WindowState = FormWindowState.Maximized;
        this.StartPosition = FormStartPosition.CenterScreen;

        // Panel du haut avec infos utilisateur
        var panelTop = new Panel
        {
            Dock = DockStyle.Top,
            Height = 80,
            BackColor = System.Drawing.Color.FromArgb(37, 99, 235)
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
            Text = $"🔑 Rôle: Administrateur",
            Location = new System.Drawing.Point(30, 50),
            AutoSize = true,
            Font = new System.Drawing.Font("Segoe UI", 12),
            ForeColor = System.Drawing.Color.FromArgb(220, 220, 220)
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

        // Panel principal avec cartes de statistiques
        var panelMain = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = System.Drawing.Color.FromArgb(249, 250, 251),
            Padding = new Padding(30)
        };

        var lblTitle = new Label
        {
            Text = "📊 Vue d'ensemble - Tableau de bord Administrateur",
            Location = new System.Drawing.Point(30, 20),
            AutoSize = true,
            Font = new System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold),
            ForeColor = System.Drawing.Color.FromArgb(31, 41, 55)
        };

        // Cartes de statistiques
        var cardUtilisateurs = CreateCard("👥 Utilisateurs", "4", "Comptes actifs", 30, 80, System.Drawing.Color.FromArgb(59, 130, 246));
        var cardFamilles = CreateCard("👨‍👩‍👧‍👦 Familles", "0", "Familles inscrites", 330, 80, System.Drawing.Color.FromArgb(16, 185, 129));
        var cardEleves = CreateCard("🎓 Élèves", "0", "Élèves actifs", 630, 80, System.Drawing.Color.FromArgb(245, 158, 11));
        var cardClasses = CreateCard("📚 Classes", "12", "Classes disponibles", 930, 80, System.Drawing.Color.FromArgb(139, 92, 246));

        // Boutons d'action rapide
        var lblActions = new Label
        {
            Text = "⚡ Actions rapides",
            Location = new System.Drawing.Point(30, 280),
            AutoSize = true,
            Font = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold),
            ForeColor = System.Drawing.Color.FromArgb(31, 41, 55)
        };

        var btnGestionUsers = CreateActionButton("👥 Gestion Utilisateurs", 30, 320);
        btnGestionUsers.Click += (s, e) => MessageBox.Show("Fonctionnalité à venir", "Information");

        var btnGestionFamilles = CreateActionButton("👨‍👩‍👧‍👦 Gestion Familles", 330, 320);
        btnGestionFamilles.Click += (s, e) => ShowFamillesList();

        var btnGestionEleves = CreateActionButton("🎓 Gestion Élèves", 630, 320);
        btnGestionEleves.Click += (s, e) => MessageBox.Show("Fonctionnalité à venir", "Information");

        var btnParametres = CreateActionButton("⚙️ Paramètres", 930, 320);
        btnParametres.Click += (s, e) => MessageBox.Show("Fonctionnalité à venir", "Information");

        panelMain.Controls.AddRange(new Control[] {
            lblTitle,
            cardUtilisateurs, cardFamilles, cardEleves, cardClasses,
            lblActions,
            btnGestionUsers, btnGestionFamilles, btnGestionEleves, btnParametres
        });

        this.Controls.Add(panelMain);
        this.Controls.Add(panelTop);
    }

    private Panel CreateCard(string title, string value, string subtitle, int x, int y, System.Drawing.Color color)
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
            ForeColor = color
        };

        var lblValue = new Label
        {
            Text = value,
            Location = new System.Drawing.Point(20, 55),
            AutoSize = true,
            Font = new System.Drawing.Font("Segoe UI", 32, System.Drawing.FontStyle.Bold),
            ForeColor = System.Drawing.Color.FromArgb(31, 41, 55)
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
            ForeColor = System.Drawing.Color.FromArgb(37, 99, 235),
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