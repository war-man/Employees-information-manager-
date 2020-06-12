using System.Threading.Tasks;
using EmployeesInformationManager.Repositories;
using EmployeesInformationManager.Data;
using EmployeesInformationManager.Proxies;
using Newtonsoft.Json;

namespace EmployeesInformationManager.Services
{
    ///<remarks> Provides both low and high level queries
    /// to create, read, update or delete rows from Employee database table</remarks>
    public class EmployeeServices:EmployeeRepository
    {

        public EmployeeServices(EmployeesInformationManagerContext context)
        :base(context){}

        ///<returns>Returns employee as EmployeeModelView using it's id</returns>
        public EmployeeModelView GetAsModelView(int id)
        {
            var serializedParent = JsonConvert.SerializeObject(
                this.GetById(id)); 
            return JsonConvert.DeserializeObject
            <EmployeeModelView>(serializedParent);
        }

        ///<summary>Deletes employee from database using it's id asynchronously</summary>
        public async Task DeleteAsync(int id)
        {
            this.Delete(id);
            await this.SaveAsync();
        }
    }
}
