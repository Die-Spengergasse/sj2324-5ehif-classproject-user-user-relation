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

            _connection = new SqliteConnection("DataSource=:memory:");

            _connection.Open();

            _options = new DbContextOptionsBuilder<UserRelationsContext>().UseSqlite(_connection).Options;

            _context = new UserRelationsContext(_options);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

        }


        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
