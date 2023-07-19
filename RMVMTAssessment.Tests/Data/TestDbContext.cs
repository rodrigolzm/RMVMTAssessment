using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RMVMTAssessment.Data;

namespace RMVMTAssessment.Tests.Data
{
    public abstract class TestDbContext : IDisposable
    {
        private const string InMemoryConnectionString = "DataSource=:memory:";
        
        private readonly SqliteConnection _connection;

        protected readonly ApplicationDbContext _dbContext;

        protected TestDbContext()
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlite(_connection)
                    .Options;
            _dbContext = new ApplicationDbContext(options);
            _dbContext.Database.EnsureCreated();
            DbInitializer.Initialize(_dbContext);
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
