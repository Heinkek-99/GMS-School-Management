namespace GMS.Shared.Constants;

public static class Permissions
{
    // Permissions par module
    public static class Eleves
    {
        public const string View = "eleves.view";
        public const string Create = "eleves.create";
        public const string Update = "eleves.update";
        public const string Delete = "eleves.delete";
    }

    public static class Finances
    {
        public const string View = "finances.view";
        public const string Create = "finances.create";
        public const string Update = "finances.update";
        public const string Delete = "finances.delete";
    }

    public static class Notes
    {
        public const string View = "notes.view";
        public const string Create = "notes.create";
        public const string Update = "notes.update";
        public const string Delete = "notes.delete";
    }

    public static class Utilisateurs
    {
        public const string View = "users.view";
        public const string Create = "users.create";
        public const string Update = "users.update";
        public const string Delete = "users.delete";
    }

    // Matrice rôle -> permissions
    public static Dictionary<string, List<string>> RolePermissions = new()
    {
        [Roles.Admin] = new List<string>
        {
            Eleves.View, Eleves.Create, Eleves.Update, Eleves.Delete,
            Finances.View, Finances.Create, Finances.Update, Finances.Delete,
            Notes.View, Notes.Create, Notes.Update, Notes.Delete,
            Utilisateurs.View, Utilisateurs.Create, Utilisateurs.Update, Utilisateurs.Delete
        },
        [Roles.Directeur] = new List<string>
        {
            Eleves.View, Finances.View, Notes.View, Utilisateurs.View
        },
        [Roles.Secretaire] = new List<string>
        {
            Eleves.View, Eleves.Create, Eleves.Update,
            Finances.View,
            Notes.View, Notes.Create, Notes.Update
        },
        [Roles.Comptable] = new List<string>
        {
            Finances.View, Finances.Create, Finances.Update,
            Eleves.View
        }
    };
}