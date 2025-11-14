using FluentValidation;
using GMS.Application.Features.Eleves.Commands;

namespace GMS.Application.Features.Eleves.Requests
{
    public class CreateEleveResponse
    {
        public Guid Id { get; set; }
        public string Matricule { get; set; } = string.Empty;
        public string NomComplet { get; set; } = string.Empty;
        public string Classe { get; set; } = string.Empty;
        public int Age { get; set; }
        public decimal SoldeFinancier { get; set; }
        public int NombreFraisGeneres { get; set; }
        public DateTime DateInscription { get; set; }
    }
 
}