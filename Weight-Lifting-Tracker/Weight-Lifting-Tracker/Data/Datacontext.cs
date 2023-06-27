using Microsoft.EntityFrameworkCore;
using Weight_Lifting_Tracker.Models;


namespace Weight_Lifting_Tracker.Data
{
    public class Datacontext : DbContext
    {
        public Datacontext(DbContextOptions<Datacontext> options) : base(options)
        {
        }

        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Lift> Lifts { get; set; }
        public DbSet<Set> Sets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Workout>().HasKey(x => x.Id);
            modelBuilder.Entity<Lift>().HasKey(x => x.Id);
            modelBuilder.Entity<Set>().HasKey(x => x.Id);
        }
    }
}
