using System.Windows.Forms;
using MediatR;
using GMS.Application.Features.Familles.Queries;
using GMS.Application.Features.Familles.Commands;

namespace GMS.Desktop.Forms;

public partial class FamilleFormDialog : Form
{
    private readonly IMediator _mediator;
    private readonly Guid? _familleId;
    
    private TextBox txtNomFamille;
    private TextBox txtNomPere, txtPrenomPere, txtTelPere, txtEmailPere;
    private TextBox txtNomMere, txtPrenomMere, txtTelMere, txtEmailMere;
    private TextBox txtAdresse, txtVille, txtCodePostal;
    private Button btnSave, btnCancel;

    public FamilleFormDialog(IMediator mediator, Guid? familleId)
    {
        _mediator = mediator;
        _familleId = familleId;
        InitializeComponent();
        
        if (_familleId.HasValue)
        {
            LoadFamilleAsync();
        }
    }

    private void InitializeComponent()
    {
        this.Text = _familleId.HasValue ? "Modifier la Famille" : "Nouvelle Famille";
        this.Size = new System.Drawing.Size(700, 650);
        this.StartPosition = FormStartPosition.CenterParent;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;

        var y = 20;

        // Nom de famille
        AddLabel("Nom de famille:", 20, y);
        txtNomFamille = AddTextBox(200, y, 450);
        y += 40;

        // INFORMATIONS PÈRE
        AddGroupLabel("👨 Informations du Père", 20, y);
        y += 30;

        AddLabel("Nom*:", 20, y);
        txtNomPere = AddTextBox(200, y, 220);
        AddLabel("Prénom:", 440, y);
        txtPrenomPere = AddTextBox(520, y, 150);
        y += 40;

        AddLabel("Téléphone:", 20, y);
        txtTelPere = AddTextBox(200, y, 220);
        AddLabel("Email:", 440, y);
        txtEmailPere = AddTextBox(520, y, 150);
        y += 40;

        // INFORMATIONS MÈRE
        AddGroupLabel("👩 Informations de la Mère", 20, y);
        y += 30;

        AddLabel("Nom*:", 20, y);
        txtNomMere = AddTextBox(200, y, 220);
        AddLabel("Prénom:", 440, y);
        txtPrenomMere = AddTextBox(520, y, 150);
        y += 40;

        AddLabel("Téléphone:", 20, y);
        txtTelMere = AddTextBox(200, y, 220);
        AddLabel("Email:", 440, y);
        txtEmailMere = AddTextBox(520, y, 150);
        y += 40;

        // ADRESSE
        AddGroupLabel("📍 Adresse", 20, y);
        y += 30;

        AddLabel("Adresse*:", 20, y);
        txtAdresse = AddTextBox(200, y, 470);
        y += 40;

        AddLabel("Ville:", 20, y);
        txtVille = AddTextBox(200, y, 220);
        AddLabel("Code Postal:", 440, y);
        txtCodePostal = AddTextBox(550, y, 120);
        y += 60;

        // Boutons
        btnCancel = new Button
        {
            Text = "Annuler",
            Location = new System.Drawing.Point(450, y),
            Size = new System.Drawing.Size(100, 35),
            DialogResult = DialogResult.Cancel
        };

        btnSave = new Button
        {
            Text = _familleId.HasValue ? "💾 Modifier" : "💾 Enregistrer",
            Location = new System.Drawing.Point(560, y),
            Size = new System.Drawing.Size(110, 35),
            BackColor = System.Drawing.Color.FromArgb(34, 197, 94),
            ForeColor = System.Drawing.Color.White,
            FlatStyle = FlatStyle.Flat
        };
        btnSave.Click += BtnSave_Click;

        this.Controls.Add(btnSave);
        this.Controls.Add(btnCancel);
        this.AcceptButton = btnSave;
        this.CancelButton = btnCancel;
    }

    private Label AddLabel(string text, int x, int y)
    {
        var label = new Label
        {
            Text = text,
            Location = new System.Drawing.Point(x, y + 3),
            AutoSize = true
        };
        this.Controls.Add(label);
        return label;
    }

    private Label AddGroupLabel(string text, int x, int y)
    {
        var label = new Label
        {
            Text = text,
            Location = new System.Drawing.Point(x, y),
            AutoSize = true,
            Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold),
            ForeColor = System.Drawing.Color.FromArgb(37, 99, 235)
        };
        this.Controls.Add(label);
        return label;
    }

    private TextBox AddTextBox(int x, int y, int width)
    {
        var textBox = new TextBox
        {
            Location = new System.Drawing.Point(x, y),
            Size = new System.Drawing.Size(width, 25)
        };
        this.Controls.Add(textBox);
        return textBox;
    }

    private async void LoadFamilleAsync()
    {
        try
        {
            var query = new GetFamilleByIdQuery(_familleId!.Value);
            var result = await _mediator.Send(query);

            if (result.IsSuccess && result.Data != null)
            {
                var famille = result.Data;
                txtNomFamille.Text = famille.NomFamille;
                txtNomPere.Text = famille.NomResponsable;
                txtPrenomPere.Text = famille.PrenomResponsable;
                txtTelPere.Text = famille.TelephonePrincipal;
                txtEmailPere.Text = famille.Email;
                txtNomMere.Text = famille.NomPere;
                txtPrenomMere.Text = famille.PrenomMere;
                txtTelMere.Text = famille.TelephoneMere;
                txtAdresse.Text = famille.Adresse;
                txtVille.Text = famille.Ville;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erreur: {ex.Message}", "Erreur", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void BtnSave_Click(object? sender, EventArgs e)
    {
        // Validation
        if (string.IsNullOrWhiteSpace(txtNomFamille.Text))
        {
            MessageBox.Show("Le nom de famille est requis", "Validation", 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtNomFamille.Focus();
            return;
        }

        if (string.IsNullOrWhiteSpace(txtNomPere.Text))
        {
            MessageBox.Show("Le nom du père est requis", "Validation", 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtNomPere.Focus();
            return;
        }

        if (string.IsNullOrWhiteSpace(txtAdresse.Text))
        {
            MessageBox.Show("L'adresse est requise", "Validation", 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtAdresse.Focus();
            return;
        }

        btnSave.Enabled = false;
        btnSave.Text = "Enregistrement...";

        try
        {
            if (_familleId.HasValue)
            {
                // Modifier
                var command = new UpdateFamilleCommand
                {
                    Id = _familleId.Value,
                    NomFamille = txtNomFamille.Text.Trim(),
                    NomPere = txtNomPere.Text.Trim(),
                    PrenomPere = txtPrenomPere.Text?.Trim(),
                    TelephonePere = txtTelPere.Text?.Trim(),
                    EmailPere = txtEmailPere.Text?.Trim(),
                    NomMere = txtNomMere.Text.Trim(),
                    PrenomMere = txtPrenomMere.Text?.Trim(),
                    TelephoneMere = txtTelMere.Text?.Trim(),
                    EmailMere = txtEmailMere.Text?.Trim(),
                    Adresse = txtAdresse.Text.Trim(),
                    Ville = txtVille.Text?.Trim(),
                    CodePostal = txtCodePostal.Text?.Trim()
                };

                await _mediator.Send(command);
                MessageBox.Show("Famille modifiée avec succès", "Succès", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Créer
                var command = new CreateFamilleCommand
                {
                    NomFamille = txtNomFamille.Text.Trim(),
                    NomPere = txtNomPere.Text.Trim(),
                    PrenomPere = txtPrenomPere.Text?.Trim(),
                    TelephonePere = txtTelPere.Text?.Trim(),
                    EmailPere = txtEmailPere.Text?.Trim(),
                    NomMere = txtNomMere.Text.Trim(),
                    PrenomMere = txtPrenomMere.Text?.Trim(),
                    TelephoneMere = txtTelMere.Text?.Trim(),
                    EmailMere = txtEmailMere.Text?.Trim(),
                    Adresse = txtAdresse.Text.Trim(),
                    Ville = txtVille.Text?.Trim(),
                    CodePostal = txtCodePostal.Text?.Trim(),
                    Pays = "Cameroun"
                };

                await _mediator.Send(command);
                MessageBox.Show("Famille créée avec succès", "Succès", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erreur: {ex.Message}", "Erreur", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            btnSave.Enabled = true;
            btnSave.Text = _familleId.HasValue ? "💾 Modifier" : "💾 Enregistrer";
        }
    }
}