using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ReserveRoom.DbContexts
{
    public class ReserveRoomDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ReserveRoomDbContext>
    {
        public ReserveRoomDbContext CreateDbContext(string[] args)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite("Data Source=reserveroom.db").Options;

            return new ReserveRoomDbContext(options);
        }
    }
}
