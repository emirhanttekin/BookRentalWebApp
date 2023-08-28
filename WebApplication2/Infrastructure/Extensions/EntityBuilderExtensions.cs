using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication2.Entity;

namespace WebApplication2.Infrastructure.Extensions
{
    public static class EntityBuilderExtensions
    {
        public static void HasExtended<TEntity>(this EntityTypeBuilder<TEntity> entity) where TEntity : BaseEntity
        {
            entity.HasKey(x => x.Id);
                
           
        }
    }
}
