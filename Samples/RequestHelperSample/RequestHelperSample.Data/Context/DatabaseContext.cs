using Microsoft.EntityFrameworkCore;
using RequestHelperSample.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RequestHelperSample.Data.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
