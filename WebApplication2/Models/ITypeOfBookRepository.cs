using WebApplication2.Entity;

namespace WebApplication2.Models
{
    public interface ITypeOfBookRepository : IRepository<TypeOfBook>
    {
        void Update(TypeOfBook typeOfBook);
        void Save();

    }
}
