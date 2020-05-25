using Microsoft.EntityFrameworkCore;
using Sora.Database.Models;

namespace Sora.Database
{
    public sealed class SoraDbContext : DbContext
    {
        private readonly IMySqlConfig _config;

        public SoraDbContext(DbContextOptions<SoraDbContext> options)
            : base(options)
        {
        }

        public DbSet<DbUser> Users { get; set; }
        public DbSet<DbFriend> Friends { get; set; }
        public DbSet<DbAchievement> Achievements { get; set; }
        public DbSet<DbScore> Scores { get; set; }
        public DbSet<DbLeaderboard> Leaderboard { get; set; }
        public DbSet<DboAuthClient> OAuthClients { get; set; }
        public DbSet<DbBeatmap> Beatmaps { get; set; }

        public void Migrate()
        {
            Database.Migrate();
        }

        private class MySqlConfig : IMySqlConfig
        {
            public CMySql MySql { get; set; }
        }
    }
}