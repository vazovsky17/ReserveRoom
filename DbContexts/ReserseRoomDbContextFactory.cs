using Microsoft.EntityFrameworkCore;

namespace ReserveRoom.DbContexts
{
    public class ReserseRoomDbContextFactory
    {
        private readonly string _connectionString;

        public ReserseRoomDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ReserveRoomDbContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;

            return new ReserveRoomDbContext(options);
        }
    }
}
