using Microsoft.EntityFrameworkCore;
using MinimalApiExampleTodoList;

var builder = WebApplication.CreateBuilder(args);

// Add Services To DI

var app = builder.Build();

app.MapGet("/todoitems", async (AppDbContext db) => await db.Todos.ToListAsync());

app.MapGet("/todoitems/{id}", async (int id, AppDbContext db) => await db.Todos.FindAsync(id));

app.MapPost("/todoitems", async (TodoItem todo, AppDbContext db) =>
{
    await db.Todos.AddAsync(todo);
    await db.SaveChangesAsync();
    return Results.Created($"/todolist/{todo.Id}", todo);
});

// Middleware Pipeline

app.Run();
