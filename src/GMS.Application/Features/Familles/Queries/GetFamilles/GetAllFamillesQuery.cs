using GMS.Application.Common;
using MediatR;

public record GetAllFamillesQuery : IRequest<Result<List<FamilleDto>>>
{
    public Guid EcoleId {get; init;}
    public string? SearchTerm{get; init;}
    public int PageNumber{get; init;} = 1;
    public int PageSize{get; init;} = 50;


    public GetAllFamillesQuery(Guid EcoleId)
    {
        EcoleId = EcoleId;
    }
}
