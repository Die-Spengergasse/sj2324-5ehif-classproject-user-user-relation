using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using sj2324_5ehif_cooking_user.Application.Infrastructure;
using sj2324_5ehif_cooking_user.Application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sj2324_5ehif_cooking_user.Test
{
    public class DatabaseTest : IDisposable
    {

        protected readonly SqliteConnection _connection;

        protected readonly DbContextOptions<UserContext> _options;

        protected readonly UserContext _context;

        public DatabaseTest()
        {
            var options = new DbContextOptionsBuilder<UserContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            _context = new UserContext(options);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
