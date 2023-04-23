using Microsoft.EntityFrameworkCore;

namespace EvercraftWebsite.Data;

public class EvercraftDbContext : DbContext
{
    public EvercraftDbContext(DbContextOptions<EvercraftDbContext> options): base(options)
    {
            
    }
        
    public virtual DbSet<DnDCharacter> DnDCharacters { get; set; }
}