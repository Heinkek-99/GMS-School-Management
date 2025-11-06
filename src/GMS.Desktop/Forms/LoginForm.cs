using System.Windows.Forms;
using MediatR;
using GMS.Application.Features.Auth.Commands.Login;
using GMS.Application.Common.Services;

namespace GMS.Desktop.Forms;

public partial class LoginForm : Form
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;
    private TextBox txtUsername;
    private TextBox txtPassword;
    private Button btnLogin;
    private Label lblError;
    private CheckBox chkShowPassword;

    public LoginForm(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.Text = "GMS - Connexion";
        this.Size = new System.Drawing.Size(450, 380);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;

        // Logo / Titre
        var lblTitle = new Label
        {
            Text = "GMS - Gestion Scolaire",
            Font = new System.Drawing.Font("Segoe UI", 18, System.Drawing.FontStyle.Bold),
            Location = new System.Drawing.Point(80, 30),
            AutoSize = true,
            ForeColor = System.Drawing.Color.FromArgb(37, 99, 235)
        };

        // Username
        var lblUsername = new Label
        {
            Text = "Nom d'utilisateur:",
            Location = new System.Drawing.Point(50, 100),
            AutoSize = true,
            Font = new System.Drawing.Font("Segoe UI", 10)
        };

        txtUsername = new TextBox
        {
            Location = new System.Drawing.Point(50, 125),
            Size = new System.Drawing.Size(350, 30),
            Font = new System.Drawing.Font("Segoe UI", 11)
        };

        // Password
        var lblPassword = new Label
        {
            Text = "Mot de passe:",
            Location = new System.Drawing.Point(50, 170),
            AutoSize = true,
            Font = new System.Drawing.Font("Segoe UI", 10)
        };

        txtPassword = new TextBox
        {
            Location = new System.Drawing.Point(50, 195),
            Size = new System.Drawing.Size(350, 30),
            Font = new System.Drawing.Font("Segoe UI", 11),
            UseSystemPasswordChar = true
        };
        txtPassword.KeyPress += TxtPassword_KeyPress;

        // Show password
        chkShowPassword = new CheckBox
        {
            Text = "Afficher le mot de passe",
            Location = new System.Drawing.Point(50, 230),
            AutoSize = true
        };
        chkShowPassword.CheckedChanged += (s, e) =>
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        };

        // Error label
        lblError = new Label
        {
            Location = new System.Drawing.Point(50, 260),
            Size = new System.Drawing.Size(350, 30),
            ForeColor = System.Drawing.Color.Red,
            Text = "",
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
            Font = new System.Drawing.Font("Segoe UI", 9)
        };

        // Login button
        btnLogin = new Button
        {
            Text = "Se connecter",
            Location = new System.Drawing.Point(150, 280),
            Size = new System.Drawing.Size(150, 40),
            Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold),
            BackColor = System.Drawing.Color.FromArgb(37, 99, 235),
            ForeColor = System.Drawing.Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand
        };
        btnLogin.FlatAppearance.BorderSize = 0;
        btnLogin.Click += BtnLogin_Click;

        // Accept button (Enter key)
        this.AcceptButton = btnLogin;

        // Add controls
        this.Controls.Add(lblTitle);
        this.Controls.Add(lblUsername);
        this.Controls.Add(txtUsername);
        this.Controls.Add(lblPassword);
        this.Controls.Add(txtPassword);
        this.Controls.Add(chkShowPassword);
        this.Controls.Add(lblError);
        this.Controls.Add(btnLogin);
    }

    private void TxtPassword_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            e.Handled = true;
            BtnLogin_Click(sender, e);
        }
    }

    private async void BtnLogin_Click(object? sender, EventArgs e)
    {
        lblError.Text = "";
        
        if (string.IsNullOrWhiteSpace(txtUsername.Text))
        {
            lblError.Text = "Veuillez saisir votre nom d'utilisateur";
            txtUsername.Focus();
            return;
        }

        if (string.IsNullOrWhiteSpace(txtPassword.Text))
        {
            lblError.Text = "Veuillez saisir votre mot de passe";
            txtPassword.Focus();
            return;
        }

        btnLogin.Enabled = false;
        btnLogin.Text = "Connexion en cours...";
        lblError.Text = "Connexion...";
        lblError.ForeColor = System.Drawing.Color.Blue;

        try
        {
            var command = new LoginCommand
            {
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text
            };

            var result = await _mediator.Send(command);

            if (result.Success && result.Utilisateur != null)
            {
                // Enregistrer l'utilisateur dans la session
                _currentUserService.SetUser(
                    result.Utilisateur.Id,
                    result.Utilisateur.Username,
                    result.Utilisateur.Role,
                    result.Utilisateur.NomComplet
                );

                lblError.ForeColor = System.Drawing.Color.Green;
                lblError.Text = "Connexion réussie !";

                await Task.Delay(500); // Petite pause pour l'UX

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Text = result.ErrorMessage ?? "Échec de la connexion";
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }
        catch (Exception ex)
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = $"Erreur: {ex.Message}";
        }
        finally
        {
            btnLogin.Enabled = true;
            btnLogin.Text = "Se connecter";
        }
    }
}