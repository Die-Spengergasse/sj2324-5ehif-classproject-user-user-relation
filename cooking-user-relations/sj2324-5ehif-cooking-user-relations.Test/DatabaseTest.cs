using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using sj2324_5ehif_cooking_user_relations.Application.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sj2324_5ehif_cooking_user_relations.Test
{
    public class DatabaseTest : IDisposable
    {

        protected readonly SqliteConnection _connection;

        protected readonly DbContextOptions<UserRelationsContext> _options;

        protected readonly UserRelationsContext _context;


        public DatabaseTest()
        {

            var options = new DbContextOptionsBuilder<UserRelationsContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            _context = new UserRelationsContext(options);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
