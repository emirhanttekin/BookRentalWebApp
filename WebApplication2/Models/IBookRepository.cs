using WebApplication2.Entity;

namespace WebApplication2.Models
{
    public interface IBookRepository : IRepository<Book>
    {
        void Update(Book book);
        void Save();

    }
}
