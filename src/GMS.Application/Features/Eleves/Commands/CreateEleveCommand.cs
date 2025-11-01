using GMS.Application.Common;
using MediatR;

namespace GMS.Application.Features.Eleves.Commands;

public record CreateEleveCommand : IRequest<Result<Guid>>
{
    public Guid FamilleId { get; init; }
    public string Nom { get; init; }
    public string Prenom { get; init; }
    public DateTime DateNaissance { get; init; }
    public string LieuNaissance { get; init; }
    public string Sexe { get; init; }
    public Guid ClasseId { get; init; }
    public Guid AnneeScolaireId { get; init; }
    public string? PhotoPath { get; init; }
}