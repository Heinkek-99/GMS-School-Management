using MediatR;
using GMS.Application.Common;

// public record DeleteFamilleCommand(Guid FamilleId) : IRequest<Result<bool>>;
public record DeleteFamilleCommand : IRequest<Result<bool>>
{
    public Guid Id { get; set; }

    public DeleteFamilleCommand(Guid id)
    {
        Id = id;
    }
}
