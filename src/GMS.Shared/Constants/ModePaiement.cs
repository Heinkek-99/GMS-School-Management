namespace GMS.Shared.Constants;

public static class ModePaiement
{
    public const string Especes = "Espèces";
    public const string Cheque = "Chèque";
    public const string Virement = "Virement";
    public const string MobileMoney = "Mobile Money";
    public const string CarteBancaire = "Carte Bancaire";

    public static readonly string[] All = { Especes, Cheque, Virement, MobileMoney, CarteBancaire };
}