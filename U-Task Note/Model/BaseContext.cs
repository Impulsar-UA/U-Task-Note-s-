using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace U_Task_Note.Model;

public class BaseContext : DbContext
{
    public BaseContext()
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Note> Notes { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite("DataSource=Task-NoteDB.db");
}
