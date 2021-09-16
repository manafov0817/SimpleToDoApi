using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDoApp.Data.Entities;
using ToDoApp.Data.Entities.Enums;

namespace ToDoApp.Data.Data
{
    public static class SeedDatabase
    {
        public static async void Seed(DbContext dbContext)
        {
            if (dbContext.Database.GetPendingMigrations().Count() == 0)
            {
                if (dbContext is ToDoAppDbContext)
                {
                    using (var context = new ToDoAppDbContext())
                    {
                        if (context.Categories.Count() == 0)
                        {
                            await context.Categories.AddRangeAsync(new List<Category>()
                            {
                                new Category() { Name = "Exercises in Gym" },
                                new Category() { Name = "Daily Routine" },
                                new Category() { Name = "Work Tasks" }
                            });
                            await context.SaveChangesAsync();
                        }

                        if (context.ToDoItems.Count() == 0)
                        {
                            DateTime now = DateTime.Now;

                            ToDoItem warmUp = new ToDoItem()
                            {
                                Title = "Morning Warm Up",
                                Content = "Warm Up for 30 minutes",
                                CreateDate = DateTime.Now,
                                StartTime = new DateTime(now.Year, now.Month, now.Day, 5, 00, 00),
                                DueTime = new DateTime(now.Year, now.Month, now.Day, 5, 30, 00),
                                CategoryId = context.Categories
                                                           .FirstOrDefault(
                                                                 c => c.Name
                                                                 .ToLower()
                                                                 .Contains("exercise".ToLower())).Id,
                                ItemStatus = ItemStatus.Waiting
                            };

                            ToDoItem toothBrush = new ToDoItem()
                            {
                                Title = "Tooth Brush",
                                Content = "Brush your teeth",
                                CreateDate = DateTime.Now,
                                StartTime = new DateTime(now.Year, now.Month, now.Day, 5, 30, 00),
                                DueTime = new DateTime(now.Year, now.Month, now.Day, 5, 35, 00),
                                CategoryId = context.Categories
                                                       .FirstOrDefault(
                                                             c => c.Name
                                                             .ToLower()
                                                             .Contains("daily".ToLower())).Id,
                                ItemStatus = ItemStatus.Waiting
                            };

                            ToDoItem meditate = new ToDoItem()
                            {
                                Title = "Meditate",
                                Content = "Meditate for 15 minutes",
                                CreateDate = DateTime.Now,
                                StartTime = new DateTime(now.Year, now.Month, now.Day, 5, 40, 00),
                                DueTime = new DateTime(now.Year, now.Month, now.Day, 6, 00, 00),
                                CategoryId = context.Categories
                                                           .FirstOrDefault(
                                                                 c => c.Name
                                                                 .ToLower()
                                                                 .Contains("daily".ToLower())).Id,
                                ItemStatus = ItemStatus.Waiting
                            };

                            ToDoItem pushUp = new ToDoItem()
                            {
                                Title = "Push up 5-25",
                                Content = "You have to do 5 sets of push ups about 25 reps with 1.5 minutes rest",
                                CreateDate = DateTime.Now,
                                StartTime = new DateTime(now.Year, now.Month, now.Day, 16, 00, 00),
                                DueTime = new DateTime(now.Year, now.Month, now.Day, 16, 30, 00),
                                CategoryId = context.Categories
                                                          .FirstOrDefault(
                                                                c => c.Name
                                                                .ToLower()
                                                                .Contains("exercise".ToLower())).Id,
                                ItemStatus = ItemStatus.Waiting
                            };

                            ToDoItem pullUp = new ToDoItem()
                            {
                                Title = "Pull up 5-12",
                                Content = "You have to do 5 sets of pull ups about 12 reps with 1.5 minutes rest",
                                CreateDate = DateTime.Now,
                                StartTime = new DateTime(now.Year, now.Month, now.Day, 16, 00, 00),
                                DueTime = new DateTime(now.Year, now.Month, now.Day, 16, 30, 00),
                                CategoryId = context.Categories
                                                 .FirstOrDefault(
                                                       c => c.Name
                                                       .ToLower()
                                                       .Contains("exercise".ToLower())).Id,
                                ItemStatus = ItemStatus.Waiting
                            };

                            await context.ToDoItems.AddRangeAsync(new List<ToDoItem>() {
                                            warmUp, toothBrush, meditate, pushUp, pullUp
                                        });

                            if (context.ToDoLists.Count() == 0)
                            {
                                await context.ToDoLists.AddRangeAsync(new List<ToDoList>()
                                  {
                                      new ToDoList() {
                                          Title="Morning Routine",
                                          CreateDate=now,
                                          ToDoItems=new List<ToDoItem>(){ warmUp ,toothBrush,meditate},
                                      },
                                      new ToDoList() {
                                          Title="Gym Routine",
                                          CreateDate=now,
                                          ToDoItems=new List<ToDoItem>(){ pushUp, pullUp },
                                      }
                                  });
                                await context.SaveChangesAsync();
                            }
                        }
                    }
                }
            }
        }
    }
}
