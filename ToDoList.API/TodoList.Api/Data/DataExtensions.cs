using System;
using Microsoft.EntityFrameworkCore;

namespace TodoList.Api.Data;

public static class DataExtensions
{
    public static async Task MigrateDBAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ToDoListContext>();
        await dbContext.Database.MigrateAsync();
    }
}
