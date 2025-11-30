using System;
using Microsoft.EntityFrameworkCore;
using TodoList.Api.Data;
using TodoList.Api.Model;

namespace TodoList.Api.Endpoints;

public static class UserEndpoints
{
    const string GetUserEndpointName = "users";
    public static RouteGroupBuilder MapUserEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("users").WithParameterValidation();

        //GET /users
        group.MapGet("/", async (ToDoListContext dbContext) => await dbContext.Users.ToListAsync());

        //GET /users/{id}
        group.MapGet("/{id}", async (int id, ToDoListContext dbContext) =>
        {
            var user = await dbContext.Users.FindAsync(id);
            return user is not null ? Results.Ok(user) : Results.NotFound();
        }).WithName(GetUserEndpointName);

        //POST /users
        group.MapPost("/", async (User user, ToDoListContext dbContext) =>
        {
            User existUser = await dbContext.Users.FindAsync(user.Id);
            if (existUser is not null)
            {
                return Results.Conflict($"User with Id {user.Id} already exists.");
            }
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            return Results.CreatedAtRoute(GetUserEndpointName, new { id = user.Id }, user);
        }); // removed WithName to avoid duplicate

        //PUT /users/{id}
        group.MapPut("/{id}", async (int id, User user, ToDoListContext dbContext) =>
        {
            var existing = await dbContext.Users.FindAsync(id);
            if (existing is null)
            {
                return Results.NotFound();
            }
            existing.UserName = user.UserName;
            existing.Email = user.Email;
            await dbContext.SaveChangesAsync();
            return Results.Ok("User updated successfully.");
        }); // removed WithName

        //Delete /users/{id}
        group.MapDelete("/{id}", async (int id, ToDoListContext dbContext) =>
        {
            var existing = await dbContext.Users.FindAsync(id);
            if (existing is null)
            {
                return Results.NotFound();
            }
            dbContext.Users.Remove(existing);
            await dbContext.SaveChangesAsync();
            return Results.Ok("User deleted successfully.");
        }); // removed WithName

        return group;
    }
}
