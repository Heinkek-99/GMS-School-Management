using GMS.Application.Common;
using MediatR;

public record SearchFamillesQuery(string SearchTerm) : IRequest<Result<List<FamilleDto>>>;
