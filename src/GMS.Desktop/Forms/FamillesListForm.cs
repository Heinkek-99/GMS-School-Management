using System.Windows.Forms;
using MediatR;
using GMS.Application.Features.Familles.Queries;
using GMS.Application.Features.Familles.Commands;

namespace GMS.Desktop.Forms;

public partial class FamilleListForm : Form
{
    private readonly IMediator _mediator;
    private DataGridView dgvFamilles;
    private TextBox txtSearch;
    private Button btnSearch;
    private Button btnAdd;
    private Button btnEdit;
    private Button btnDelete;
    private Button btnRefresh;
    private Label lblTotal;

    public FamilleListForm(IMediator mediator)
    {
        _mediator = mediator;
        InitializeComponent();
        LoadFamillesAsync();
        
    }

    private void InitializeComponent()
    {
        this.Text = "Gestion des Familles";
        this.Size = new System.Drawing.Size(1200, 700);
        this.StartPosition = FormStartPosition.CenterScreen;

        // Panel du haut (recherche + boutons)
        var panelTop = new Panel
        {
            Dock = DockStyle.Top,
            Height = 60,
            BackColor = System.Drawing.Color.FromArgb(243, 244, 246)
        };

        // Recherche
        var lblSearch = new Label
        {
            Text = "🔍 Recherche:",
            Location = new System.Drawing.Point(20, 20),
            AutoSize = true
        };

        txtSearch = new TextBox
        {
            Location = new System.Drawing.Point(110, 17),
            Size = new System.Drawing.Size(300, 25)
        };

        btnSearch = new Button
        {
            Text = "Rechercher",
            Location = new System.Drawing.Point(420, 15),
            Size = new System.Drawing.Size(100, 30),
            BackColor = System.Drawing.Color.FromArgb(59, 130, 246),
            ForeColor = System.Drawing.Color.White,
            FlatStyle = FlatStyle.Flat
        };
        btnSearch.Click += async (s, e) => await LoadFamillesAsync();

        // Boutons d'action
        btnAdd = new Button
        {
            Text = "➕ Nouvelle Famille",
            Location = new System.Drawing.Point(750, 15),
            Size = new System.Drawing.Size(150, 30),
            BackColor = System.Drawing.Color.FromArgb(34, 197, 94),
            ForeColor = System.Drawing.Color.White,
            FlatStyle = FlatStyle.Flat
        };
        btnAdd.Click += BtnAdd_Click;

        btnEdit = new Button
        {
            Text = "✏️ Modifier",
            Location = new System.Drawing.Point(910, 15),
            Size = new System.Drawing.Size(100, 30),
            BackColor = System.Drawing.Color.FromArgb(251, 146, 60),
            ForeColor = System.Drawing.Color.White,
            FlatStyle = FlatStyle.Flat
        };
        btnEdit.Click += BtnEdit_Click;

        btnDelete = new Button
        {
            Text = "🗑️ Supprimer",
            Location = new System.Drawing.Point(1020, 15),
            Size = new System.Drawing.Size(100, 30),
            BackColor = System.Drawing.Color.FromArgb(239, 68, 68),
            ForeColor = System.Drawing.Color.White,
            FlatStyle = FlatStyle.Flat
        };
        btnDelete.Click += BtnDelete_Click;

        panelTop.Controls.AddRange(new Control[] { lblSearch, txtSearch, btnSearch, btnAdd, btnEdit, btnDelete });

        // DataGridView
        dgvFamilles = new DataGridView
        {
            Dock = DockStyle.Fill,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            ReadOnly = true,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            BackgroundColor = System.Drawing.Color.White,
            BorderStyle = BorderStyle.None
        };
        dgvFamilles.DoubleClick += BtnEdit_Click;

        // Panel du bas (total)
        var panelBottom = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 40,
            BackColor = System.Drawing.Color.FromArgb(243, 244, 246)
        };

        lblTotal = new Label
        {
            Text = "Total: 0 famille(s)",
            Location = new System.Drawing.Point(20, 10),
            AutoSize = true,
            Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        };

        btnRefresh = new Button
        {
            Text = "🔄 Actualiser",
            Location = new System.Drawing.Point(1020, 5),
            Size = new System.Drawing.Size(100, 30),
            BackColor = System.Drawing.Color.FromArgb(99, 102, 241),
            ForeColor = System.Drawing.Color.White,
            FlatStyle = FlatStyle.Flat
        };
        btnRefresh.Click += async (s, e) => await LoadFamillesAsync();

        panelBottom.Controls.AddRange(new Control[] { lblTotal, btnRefresh });

        // Ajouter les contrôles au formulaire
        this.Controls.Add(dgvFamilles);
        this.Controls.Add(panelTop);
        this.Controls.Add(panelBottom);
    }
    
    private async Task LoadFamillesAsync()
    {
        try
        {
            btnSearch.Enabled = false;
            btnSearch.Text = "Chargement...";
            
            var query = new GetAllFamillesQuery(Guid.Empty)
            {
                SearchTerm = txtSearch.Text,
                PageNumber = 1,
                PageSize = 100

            };

            var result = await _mediator.Send(query);

            if(result.IsSuccess && result.Data != null)
            {
                var familles = result.Data;

                dgvFamilles.DataSource = System.Linq.Enumerable.ToList(
                System.Linq.Enumerable.Select(familles, f => new
                {
                    f.Id,
                    Famille = f.NomFamille,
                    Contact = f.ContactPrincipal,
                    Téléphone = f.TelephonePrincipal,
                    Email = f.Email,
                    Ville = f.Ville,
                    Enfants = f.NombreEnfants,
                    Solde = f.Solde.ToString("N0") + " FCFA",
                    Créé_le = f.CreatedAt.ToString("dd/MM/yyyy")
                })
            );

            // Cacher la colonne Id
            if (dgvFamilles.Columns["Id"] != null)
                dgvFamilles.Columns["Id"].Visible = false;

            // Colorer la colonne Solde en rouge si négatif
            if (dgvFamilles.Columns["Solde"] != null)
            {
                dgvFamilles.Columns["Solde"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            lblTotal.Text = $"Total: {familles.Count} famille(s)";
            }
            else
                {
                    MessageBox.Show(
                    result.ErrorMessage ?? "Erreur lors du chargement des familles", 
                    "Erreur", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error
                );
            }
            
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erreur lors du chargement: {ex.Message}", "Erreur", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            btnSearch.Enabled = true;
            btnSearch.Text = "Rechercher";
        }
    }

    private void BtnAdd_Click(object? sender, EventArgs e)
    {
        var form = new FamilleFormDialog(_mediator, null);
        if (form.ShowDialog() == DialogResult.OK)
        {
            LoadFamillesAsync();
        }
    }

    private void BtnEdit_Click(object? sender, EventArgs e)
    {
        if (dgvFamilles.SelectedRows.Count == 0)
        {
            MessageBox.Show("Veuillez sélectionner une famille", "Information", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var familleId = (Guid)dgvFamilles.SelectedRows[0].Cells["Id"].Value;
        var form = new FamilleFormDialog(_mediator, familleId);
        
        if (form.ShowDialog() == DialogResult.OK)
        {
            LoadFamillesAsync();
        }
    }

    private async void BtnDelete_Click(object? sender, EventArgs e)
    {
        if (dgvFamilles.SelectedRows.Count == 0)
        {
            MessageBox.Show("Veuillez sélectionner une famille", "Information", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var result = MessageBox.Show(
            "Êtes-vous sûr de vouloir supprimer cette famille ?",
            "Confirmation",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning
        );

        if (result == DialogResult.Yes)
        {
            try
            {
                var familleId = (Guid)dgvFamilles.SelectedRows[0].Cells["Id"].Value;
                var command = new DeleteFamilleCommand(familleId);
                await _mediator.Send(command);

                MessageBox.Show("Famille supprimée avec succès", "Succès", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                await LoadFamillesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}", "Erreur", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}