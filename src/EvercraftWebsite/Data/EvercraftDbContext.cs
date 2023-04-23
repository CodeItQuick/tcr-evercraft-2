using Microsoft.EntityFrameworkCore;

namespace EvercraftWebsite.Data;

public class EvercraftDbContext : DbContext, IEvercraftDbContext
{
    public EvercraftDbContext(DbContextOptions<EvercraftDbContext> options): base(options)
    {
            
    }
        
    public virtual DbSet<DnDCharacter> DnDCharacters { get; set; }
}

public interface IEvercraftDbContext
{
    public abstract DbSet<DnDCharacter> DnDCharacters { get; set; }
}