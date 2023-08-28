using WebApplication2.Data.Context;
using WebApplication2.Entity;

namespace WebApplication2.Models
{
    public class TypeOfBookRepository  : Repository<TypeOfBook>, ITypeOfBookRepository
    {
        private IMDbContext _dbContext;
        public TypeOfBookRepository(IMDbContext ımDbContext) : base(ımDbContext)
        {
            _dbContext =  ımDbContext;
        }

       
        public void Update(TypeOfBook typeOfBook)
        {
            _dbContext.Update(typeOfBook);

        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
