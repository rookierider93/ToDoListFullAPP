using System;
using Microsoft.EntityFrameworkCore;
using TodoList.Api.Data;
using TodoList.Api.Model;

namespace TodoList.Api.Endpoints;

public static class StatusEndpoints
{
    const string GetStatusEndpointName = "status";
    public static RouteGroupBuilder MapStatusEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("status").WithParameterValidation();

        //GET /status
        group.MapGet("/", async (ToDoListContext dbContext) => await dbContext.StatusMasters.ToListAsync());

        //Get /status/{id}
        group.MapGet("/{id}", async (int id, ToDoListContext dbContext) =>
        {
            StatusMaster statusMaster = await dbContext.StatusMasters.FindAsync(id);
            return statusMaster is not null ? Results.Ok(statusMaster) : Results.NotFound();
        }).WithName(GetStatusEndpointName);

        return group;
    }
}
