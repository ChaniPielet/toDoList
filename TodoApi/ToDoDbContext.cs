using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Mvc;
namespace TodoApi;

public partial class ToDoDbContext : DbContext
{
    public ToDoDbContext()
    {
    }

    public ToDoDbContext(DbContextOptions<ToDoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Item> Items { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("name=ToDoDB", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.35-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("items");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsComplete).HasColumnName("isComplete");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  

  
 

// var builder = WebApplication.CreateBuilder(args);

// // Added as service
// builder.Services.AddSingleton<ToDoDbContext>();

// var app = builder.Build();


// app.MapGet("/{id}", ([FromRoute] int id,
//                      [FromQuery(Name = "p")] int page,
//                      [FromServices] Service service,
//                      [FromHeader(Name = "Content-Type")] string contentType) 
//                      => {});

// class Service { }


}
