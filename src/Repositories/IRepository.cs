using System.Threading.Tasks;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace EmployeesInformationManager.Repositories
{
    /// <remarks> Mediates between the domain and data mapping layers (like Entity Framework). 
    /// It allows you to pull a record or number of records out of datasets,
    /// and then have those records to work on acting like an in-memory domain object collection,
    /// and you can also update or delete records within those data set,
    /// and the mapping code encapsulated by the Repository will carry out the appropriate operations behind the scenes.</remarks>
    public interface IRepository<T,PrimaryKey> 
    {                
        ///<returns> Returns One row from the database using primary key</returns>
        T GetById(PrimaryKey id); 
        
        ///<returns> Returns rows from the database,
        /// where filter is passed to the where clause</returns>
        IQueryable<T> GetList(Expression<Func<T,Boolean>> filter);
        
        ///<summary> Adds one row to the database</summary>
        void Add(T t);
        
        ///<summary> Removes one row from the database</summary>
        void Delete(PrimaryKey id); 
        
        ///<summary> Deletes rows from the database,
        /// where filter is passed to the where clause</returns>
        void DeleteRange(Expression<Func<T,Boolean>> filter);
        
        ///<summary> Updates columns value of one row existing in the database</summary>
        void Update(T t);   

        ///<summary> Saves All changes that happened at the inMemory database
        /// to the real database asynchronously</summary>  
        Task SaveAsync();    
    }
}