using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using TodoList.Api.Data;
using TodoList.Api.Model;

namespace TodoList.Api.Endpoints;

public static class TodoEndpoints
{
    const string GetTodoEndpointName = "todos";
    public static RouteGroupBuilder mapTodoEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("todos").WithParameterValidation();

        //GET /todos
        group.MapGet("/", async (ToDoListContext dbContext) =>{
           var todos= await dbContext.ToDos.ToListAsync();
           return todos is not null? Results.ok(todos): Results.NotFound();
        } );

        //GET /todos/{id}
        group.MapGet("/{id}", async (int id, ToDoListContext dbContext) =>
        {
            var todo = await dbContext.ToDos.FindAsync(id);
            return todo is not null ? Results.Ok(todo) : Results.NotFound();
        }).WithName(GetTodoEndpointName);

        //POST /todos
        group.MapPost("/", async (ToDo todo, ToDoListContext dbContext) =>
        {
            todo.Description ??= string.Empty;
            if (todo.CreatedDate == default) todo.CreatedDate = DateTime.UtcNow;
            dbContext.ToDos.Add(todo);
            await dbContext.SaveChangesAsync();
            return Results.CreatedAtRoute(GetTodoEndpointName, new { id = todo.Id }, todo);
        });

        //PUT /todos/{id}
        group.MapPut("/{id}", async (int id, ToDo todo, ToDoListContext dbContext) =>
        {
            var existing = await dbContext.ToDos.FindAsync(id);
            if (existing is null)
            {
                return Results.NotFound();
            }
            existing.Title = todo.Title;
            existing.Description = todo.Description ?? string.Empty;
            existing.StatusId = todo.StatusId;
            existing.UserId = todo.UserId;
            await dbContext.SaveChangesAsync();
            return Results.Ok("Todo updated successfully.");
        });

        //Delete /todos/{id}
        group.MapDelete("/{id}", async (int id, ToDoListContext dbContext) =>
        {
            var existing = await dbContext.ToDos.FindAsync(id);
            if (existing is null)
            {
                return Results.NotFound();
            }
            dbContext.ToDos.Remove(existing);
            await dbContext.SaveChangesAsync();
            return Results.Ok("Todo deleted successfully.");
        });

        return group;
    }
}
