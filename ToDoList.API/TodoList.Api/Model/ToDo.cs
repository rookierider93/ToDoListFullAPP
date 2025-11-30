using System;

namespace TodoList.Api.Model;

public class ToDo
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public int StatusId { get; set; }
    public int UserId { get; set; }
}
