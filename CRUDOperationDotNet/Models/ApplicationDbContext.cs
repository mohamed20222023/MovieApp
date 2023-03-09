﻿using Microsoft.EntityFrameworkCore;

namespace CRUDOperationDotNet.Models
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movies> Movies { get; set; }

    }
}
