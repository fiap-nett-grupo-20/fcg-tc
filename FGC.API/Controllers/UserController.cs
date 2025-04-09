using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using FGC.API.Data;
using FGC.API.Models;
namespace FGC.API.Controllers
{
    public class UserController
    {
    }


public static class UsersEndpoints
{
	public static void MapUsersEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Users").WithTags(nameof(Users));

        group.MapGet("/", async (FGCAPIContext db) =>
        {
            return await db.Users.ToListAsync();
        })
        .WithName("GetAllUsers")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Users>, NotFound>> (int id, FGCAPIContext db) =>
        {
            return await db.Users.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Users model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetUsersById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Users users, FGCAPIContext db) =>
        {
            var affected = await db.Users
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, users.Id)
                  .SetProperty(m => m.Name, users.Name)
                  .SetProperty(m => m.Email, users.Email)
                  .SetProperty(m => m.Password, users.Password)
                  .SetProperty(m => m.Role, users.Role)
                  .SetProperty(m => m.IsActive, users.IsActive)
                  .SetProperty(m => m.CreatedAt, users.CreatedAt)
                  .SetProperty(m => m.UpdatedAt, users.UpdatedAt)
                  .SetProperty(m => m.PasswordResetToken, users.PasswordResetToken)
                  .SetProperty(m => m.PasswordResetTokenExpiration, users.PasswordResetTokenExpiration)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateUsers")
        .WithOpenApi();

        group.MapPost("/", async (Users users, FGCAPIContext db) =>
        {
            db.Users.Add(users);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Users/{users.Id}",users);
        })
        .WithName("CreateUsers")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, FGCAPIContext db) =>
        {
            var affected = await db.Users
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteUsers")
        .WithOpenApi();
    }
}
}