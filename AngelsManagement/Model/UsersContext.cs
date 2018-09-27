using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static AngelsManagement.Globals;

namespace AngelsManagement.Model
{
    public class UsersContext : DbContext
    {
        public DbSet<UserCredentials> UserCredentials { get; set; }

        public UsersContext() : base() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString);
        }
    }
}
