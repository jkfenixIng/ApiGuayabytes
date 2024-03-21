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
        public DbSet<Users> Users { get; set; }
        public DbSet<ConfigGlobal> ConfigGlobal { get; set; }
        public DbSet<Configuration> Configuration { get; set; }
        public DbSet<Games> Games { get; set; }
        public DbSet<GameTypes> GameTypes { get; set; }
        public DbSet<HistoryGames> HistoryGames { get; set; }
        public DbSet<Items> Items { get; set; }
        public DbSet<ItemsCategories> ItemsCategories { get; set; }
        public DbSet<UserInventory> UserInventory { get; set; }
    }
}