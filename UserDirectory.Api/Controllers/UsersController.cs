using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserDirectory.Application.DTOs;
using UserDirectory.Application.UseCases.GetUsers;
using UserDirectory.Application.UseCases.CreateUser;

namespace UserDirectory.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly GetUsersHandler _getHandler;
    private readonly CreateUserHandler _createHandler;

    public UsersController(GetUsersHandler getHandler, CreateUserHandler createHandler)
    {
        _getHandler = getHandler;
        _createHandler = createHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _getHandler.Handle();
        return Ok(users.Select(u => new {
            id = u.Id, name = u.Name, age = u.Age, city = u.City, state = u.State, pincode = u.Pincode
        }));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var user = (await _getHandler.Handle()).FirstOrDefault(u => u.Id == id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPost]
    [Authorize] // require valid JWT
    public async Task<IActionResult> Create([FromBody] UserCreateDto dto)
    {
        // FluentValidation will validate dto automatically
        var created = await _createHandler.Handle(dto);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, new {
            id = created.Id, name = created.Name, age = created.Age, city = created.City, state = created.State, pincode = created.Pincode
        });
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Update(int id, [FromBody] UserCreateDto dto)
    {
        var users = await _getHandler.Handle();
        var existing = users.FirstOrDefault(u => u.Id == id);
        if (existing == null) return NotFound();

        existing.Update(dto.Name, dto.Age, dto.City, dto.State, dto.Pincode);
        // repository update via DI
        // For brevity, resolve repository from DI
        var repo = HttpContext.RequestServices.GetRequiredService<UserDirectory.Application.Interfaces.IUserRepository>();
        await repo.UpdateAsync(existing);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var repo = HttpContext.RequestServices.GetRequiredService<UserDirectory.Application.Interfaces.IUserRepository>();
        await repo.DeleteAsync(id);
        return NoContent();
    }
}
