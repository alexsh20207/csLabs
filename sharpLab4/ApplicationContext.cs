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
            optionsBuilder.UseNpgsql(Consts.DB_INFO);
        }

        else if (dbName == DbName.LocalSQL)
        {
            optionsBuilder.UseInMemoryDatabase(Consts.DB_NAME);
        }
    }
}