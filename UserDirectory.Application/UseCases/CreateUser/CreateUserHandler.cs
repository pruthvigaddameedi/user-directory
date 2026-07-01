using UserDirectory.Application.DTOs;
using UserDirectory.Application.Interfaces;
using UserDirectory.Domain.Entities;

namespace UserDirectory.Application.UseCases.CreateUser;

public class CreateUserHandler
{
    private readonly IUserRepository _repo;
    public CreateUserHandler(IUserRepository repo) => _repo = repo;

    public async Task<User> Handle(UserCreateDto dto, CancellationToken ct = default)
    {
        var user = new User(dto.Name, dto.Age, dto.City, dto.State, dto.Pincode);
        return await _repo.AddAsync(user, ct);
    }
}
