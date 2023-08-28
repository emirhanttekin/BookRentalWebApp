using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebApplication2.Entity;
using WebApplication2.Infrastructure.Interfaces;

namespace WebApplication2.Data.Context
{
    public class IMDbContext : IdentityDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Buraya SQL SERVER BİLGİLERİNİ YAZICAKSINIZ");
            base.OnConfiguring(optionsBuilder);
         
        }

        public IMDbContext(DbContextOptions<IMDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            RegisterMapping(modelBuilder);
            
        }

        private void RegisterMapping(ModelBuilder modelBuilder)
        {
            Type builderInterfaceType = typeof(IEntityBuilder);
            List<Type> types = new();

            Assembly currentExecutingAssembly = Assembly.GetExecutingAssembly();
            types.AddRange(currentExecutingAssembly.DefinedTypes.Select(typeInfo => typeInfo.AsType()));

            IEnumerable<Type> builderTypes = types.Where(type =>
                builderInterfaceType.IsAssignableFrom(type) &&
                !type.IsInterface &&
                !type.IsAbstract &&
                type != builderInterfaceType &&
                type != null);

            foreach (Type builderType in builderTypes)
            {
                IEntityBuilder entityBuilder = (IEntityBuilder)Activator.CreateInstance(builderType);
                entityBuilder.Builder(modelBuilder);
            }
        }

        public DbSet<TypeOfBook> TypeOfBooks { get; set; }
        public DbSet<Book>Books { get; set; } 
        public DbSet<Hire>Hires { get; set; }
        
        public DbSet<ApplicationUser>ApplicationUsers { get; set; }
    }
}
