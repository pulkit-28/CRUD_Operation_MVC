using CRUD_Operation_MVC.Models.Domian;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Operation_MVC.Data
{
    public class MVCDemoDBContext : DbContext
    {
        public MVCDemoDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }

}
