﻿using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.Json;
using TarkovTracker.Data.Entities;
using TarkovTracker.Models;

namespace TarkovTracker.Data;

public class AppDbContext : DbContext
{
    public DbSet<UserDataItem> UserItems { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions)
        : base(dbContextOptions)
    {
        Database.EnsureCreated();
    }
}
