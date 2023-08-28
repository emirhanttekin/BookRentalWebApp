using WebApplication2.Data.Context;
using WebApplication2.Entity;

namespace WebApplication2.Models
{
    public class HireRepository : Repository<Hire>, IHireBookRepository
    {
        private IMDbContext _dbContext;
        public HireRepository(IMDbContext ımDbContext) : base(ımDbContext)
        {
            _dbContext = ımDbContext;
        }

        public void Update(Hire hire)
        {
            _dbContext.Update(hire);
           
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
