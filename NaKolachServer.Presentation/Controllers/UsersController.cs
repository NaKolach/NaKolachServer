using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using NaKolachServer.Application.Users;
using NaKolachServer.Domain.Users;
using NaKolachServer.Presentation.Controllers.Dtos;

namespace NaKolachServer.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(GetUserById getUserById, InsertUser insertUser) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<User?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await getUserById.Execute(id, cancellationToken);
    }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] InsertUserDto dto, CancellationToken cancellationToken)
    {
        var id = await insertUser.Execute(dto.Name, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, dto);
    }
}
