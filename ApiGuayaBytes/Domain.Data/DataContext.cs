using System.Collections.Generic;
using Domain.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Domain.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Users>? Users { get; set; }
    }
}