using WebApplication2.Entity;

namespace WebApplication2.Models
{
    public interface IHireBookRepository : IRepository<Hire>
    {
        void Update(Hire hire);
        void Save();

    }
}
