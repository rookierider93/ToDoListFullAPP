using System;
using Microsoft.EntityFrameworkCore;
using TodoList.Api.Model;

namespace TodoList.Api.Data;

public class ToDoListContext(DbContextOptions<ToDoListContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<ToDo> ToDos => Set<ToDo>();
    public DbSet<StatusMaster> StatusMasters => Set<StatusMaster>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StatusMaster>().HasData(
            new StatusMaster { Id = 1, Name = "Pending" },
            new StatusMaster { Id = 2, Name = "In Progress" },
            new StatusMaster { Id = 3, Name = "Completed" }
        );
    }
}

