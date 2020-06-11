using System.Threading.Tasks;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace EmployeesInformationManager.Repositories
{
    public interface IRepository<T,PrimaryKey> 
    {                
        T GetById(PrimaryKey id); 
        IQueryable<T> GetList(Expression<Func<T,Boolean>> filter);
        void Add(T t);
        void Delete(PrimaryKey id); 
        void DeleteRange(Expression<Func<T,Boolean>> filter); 
        void Update(T t);     
        Task SaveAsync();    
    }
}