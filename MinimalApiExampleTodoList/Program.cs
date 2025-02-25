using Microsoft.AspNetCore.Http.HttpResults;
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

app.MapPut("/todoitems/{id}" , async (int id , TodoItem inputTodo , AppDbContext db) =>
{
    var todo = await db.Todos.FindAsync(id);
    if (todo == null) return Results.NotFound();
    todo.Name = inputTodo.Name;
    todo.IsComplete = inputTodo.IsComplete;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/todoitems/{id}" , async (int id , AppDbContext db) =>
{
    if ( await db.Todos.FindAsync(id) is TodoItem todoItem)
    {
        db.Todos.Remove(todoItem);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
});

// Middleware Pipeline

app.Run();
