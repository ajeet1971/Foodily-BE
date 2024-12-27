using Foodily.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Foodily.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public virtual DbSet<UserVM> UserMaster { get; set; }

        public virtual DbSet<Recipe> RecipeMaster { get; set; }

        public virtual DbSet<Category> CategoryMaster { get; set; }

    }
}
