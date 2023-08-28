using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data.Context;
using WebApplication2.Entity;

namespace WebApplication2.Models
{
    public class Repository<T> : IRepository<T>where T : class
    
    {
        private readonly IMDbContext _ımDbContext;
        internal DbSet<T> dbSet; // dbset =  _ımDbContext.TypeOfBook
        public Repository(IMDbContext ımDbContext)
        {
            _ımDbContext = ımDbContext;
            this.dbSet = _ımDbContext.Set<T>();
            _ımDbContext.Books.Include(k => k.TypeOfBook);

        }

        public IEnumerable<T> GetAll(string?  includeProps = null)
        {
            IQueryable<T> sorgu = dbSet;
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var  includeProp in includeProps.Split(new char[] {','} ,StringSplitOptions.RemoveEmptyEntries))
                {
                    sorgu = sorgu.Include(includeProp);
                }
            }
            return sorgu.ToList();
        }

        public T Get(Expression<Func<T, bool>> filtre , string? includeProps = null)
        {
            IQueryable<T> sorgu = dbSet;
            sorgu =  sorgu.Where(filtre);
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    sorgu = sorgu.Include(includeProp);
                }
            }
            return sorgu.FirstOrDefault(); //
        }

        public void AddBook(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
