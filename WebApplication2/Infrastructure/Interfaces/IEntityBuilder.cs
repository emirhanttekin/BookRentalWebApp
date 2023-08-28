using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Infrastructure.Interfaces
{
    public interface IEntityBuilder
    {
        void Builder(ModelBuilder builder);
    }
}
