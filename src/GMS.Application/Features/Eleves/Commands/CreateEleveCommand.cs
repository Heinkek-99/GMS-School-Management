using GMS.Application.Common;
using MediatR;

namespace GMS.Application.Features.Eleves.Commands;

public record CreateEleveCommand : IRequest<Result<Guid>>
{
 // Relation école
        
        // Informations personnelles
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public DateTime DateNaissance { get; set; }
        public string LieuNaissance { get; set; } = string.Empty;
        public string Sexe { get; set; }
        public string? Nationalite { get; set; }
        
        // Relations
        public Guid FamilleId { get; set; }
        public Guid ClasseId { get; set; }
        public Guid AnneeScolaireId { get; set; }
                
        // Photo (optionnel à la création, peut être ajouté plus tard)
        public string PhotoPath { get; set; }
        public string? PhotoFileName { get; set; }
}