using Microsoft.EntityFrameworkCore;

namespace sharpLab4;
public class ApplicationContext : DbContext
{
    public DbSet<Experiment> Experiments { get; set; }
    private static DbName dbName = DbName.PostgreSQL;
    public ApplicationContext()
    {
        Database.EnsureCreated();
    }

    public static void SetDb(DbName dbName)
    {
        ApplicationContext.dbName = dbName;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (dbName == DbName.PostgreSQL)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=1111;Database=csDB;Username=postgres;Password=postgres");
        }

        else if (dbName == DbName.LocalSQL)
        {
            optionsBuilder.UseInMemoryDatabase("csDB");
        }
    }
}