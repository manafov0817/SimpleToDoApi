using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Data.Entities;

namespace ToDoApp.Data.Data
{
    public class ToDoAppDbContext : DbContext
    {
        public ToDoAppDbContext() { }

        public ToDoAppDbContext(DbContextOptions<ToDoAppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ToDoItem> ToDoItems { get; set; }

        public DbSet<ToDoList> ToDoLists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost; Port=5432; Database=ToDoDBContext; username=postgres; password=manafov165");
        }
    }
}
