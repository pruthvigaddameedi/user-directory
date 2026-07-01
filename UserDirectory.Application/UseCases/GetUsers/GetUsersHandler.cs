using UserDirectory.Application.Interfaces;

namespace UserDirectory.Application.UseCases.GetUsers;

public class GetUsersHandler
{
    private readonly IUserRepository _repo;
    public GetUsersHandler(IUserRepository repo) => _repo = repo;
    public Task<List<UserDirectory.Domain.Entities.User>> Handle(CancellationToken ct = default) =>
        _repo.GetAllAsync(ct);
}

