using TodoApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);
//הזרקה
builder.Services.AddDbContext<ToDoDbContext>();
//הוספת קונטרולר
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//swagger
builder.Services.AddSwaggerGen();
//cors
builder.Services.AddCors(option=>option.AddPolicy("AllowAll",builder=>{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));
var app = builder.Build();
 app.UseCors("AllowAll");
 //swagger
if (app.Environment.IsDevelopment())
 {
     app.UseSwagger();
    app.UseSwaggerUI();
}
//שליפת כל הנתונים מהמסד
 app.MapGet("/", async (ToDoDbContext db) =>{
    var data=await db.Items.ToListAsync();
    return Results.Ok(data);

 });
 //הוספה
app.MapPost("/Items/{name}", async (string name,ToDoDbContext db) =>
{  
    Item todo=new Item();
    todo.Name=name;
    db.Items.Add(todo);
    await db.SaveChangesAsync();
    return Results.Ok(db.Items);
 });
//עדכון
app.MapPut("/Items/{id}/{IsComplete}", async ( int id,bool IsComplete,ToDoDbContext db) =>
{
    var todo = await db.Items.FindAsync(id);
    if (todo is null)
     return Results.NotFound();
    todo.IsComplete = IsComplete;
    await db.SaveChangesAsync();
    return Results.Ok(db.Items);
});
//מחיקה
app.MapDelete("/Items/{id}", async (int id, ToDoDbContext db) =>
{
    if (await db.Items.FindAsync(id) is Item todo)
    {
        db.Items.Remove(todo);
        await db.SaveChangesAsync();
            return Results.Ok(db.Items);
        //return Results.NoContent();
    }

    return Results.NotFound();
});
app.Run();