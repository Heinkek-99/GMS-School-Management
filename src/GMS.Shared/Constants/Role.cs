namespace GMS.Shared.Constants;

public static class Roles
{
    public const string Admin = "Admin";
    public const string Directeur = "Directeur";
    public const string Secretaire = "Secretaire";
    public const string Comptable = "Comptable";

    public static readonly string[] All = { Admin, Directeur, Secretaire, Comptable };

    public static bool IsValid(string role)
    {
        return All.Contains(role);
    }

}