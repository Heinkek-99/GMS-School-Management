using GMS.Application.Common;
using MediatR;

namespace GMS.Application.Features.Paiements.Commands;

public record EnregistrerPaiementCommand : IRequest<Result<Guid>>
{
    public Guid FamilleId { get; init; }
    public decimal Montant { get; init; }
    public DateTime DatePaiement { get; init; }
    public string ModePaiement { get; init; } = string.Empty;
    public string? NumeroReference { get; init; }
    public string? Observations { get; init; }
    public List<VentilationDto> Ventilations { get; init; }
}