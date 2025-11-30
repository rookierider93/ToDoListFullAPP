using Microsoft.AspNetCore.Http.HttpResults;
using TodoList.Api.Data;
using TodoList.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
var connectionString = builder.Configuration.GetConnectionString("tododb");
builder.Services.AddSqlite<ToDoListContext>(connectionString);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

// Apply CORS early so preflight and redirect responses include CORS headers
app.UseCors("AllowAll");

// app.UseHttpsRedirection();

app.MapStatusEndpoints();
app.MapUserEndpoints();
app.mapTodoEndpoints();

app.Run();

