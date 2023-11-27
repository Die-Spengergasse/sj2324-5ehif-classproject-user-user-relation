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

        protected readonly UserRepository _userRepository;

        public DatabaseTest()
        {

            _connection = new SqliteConnection("DataSource=:memory:");

            _connection.Open();

            _options = new DbContextOptionsBuilder<UserContext>().UseSqlite(_connection).Options;

            _context = new UserContext(_options);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();


            _userRepository = new UserRepository(_context);
        }


        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
