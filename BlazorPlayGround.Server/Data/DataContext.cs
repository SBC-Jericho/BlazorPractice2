global using Microsoft.EntityFrameworkCore;
using BlazorPlayGround.Shared.Models;


namespace BlazorPlayGround.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Difficulty> Difficulties{ get; set; }

    }
}
