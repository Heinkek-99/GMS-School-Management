namespace GMS.Application.Services;

public interface IDocumentService
{
    Task<byte[]> GenererCarteEleveAsync(Guid eleveId);
    Task<byte[]> GenererRecuPaiementAsync(Guid paiementId);
    Task<byte[]> GenererBulletinFinancierAsync(Guid eleveId);
    Task<byte[]> GenererBulletinNotesAsync(Guid eleveId, Guid periodeId);
    Task<byte[]> GenererCertificatScolariteAsync(Guid eleveId);
}