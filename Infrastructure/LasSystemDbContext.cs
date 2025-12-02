using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class LasSystemDbContext : DbContext
    {

        public virtual DbSet<Kund> Kunder { get; set; }
        public virtual DbSet<Person> Personer { get; set; }
        public virtual DbSet<Anstallning> Anstallningar { get; set; }
        public virtual DbSet<WinLasData> WinLasData { get; set; }

        public LasSystemDbContext(DbContextOptions<LasSystemDbContext> options)
        : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=TestDB;Trusted_Connection=True;TrustServerCertificate=True;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
