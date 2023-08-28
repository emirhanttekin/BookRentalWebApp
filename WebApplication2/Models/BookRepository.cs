using WebApplication2.Data.Context;
using WebApplication2.Entity;

namespace WebApplication2.Models
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private IMDbContext _dbContext;
        public BookRepository(IMDbContext ımDbContext) : base(ımDbContext)
        {
            _dbContext = ımDbContext;
        }

        public void Update(Book book)
        {
            _dbContext.Update(book);
           
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
