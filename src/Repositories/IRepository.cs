using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeesInformationManager.Repositories
{
    public interface IRepository<T> 
    {        
        List<T> GetAll();        
        T GetByID(int? id);        
        void Add(T t);       
        void Delete(int? id);        
        void Update(T t);     
        Task SaveAsync();    
    }
}