using System.Threading.Tasks;
using EmployeesInformationManager.Repositories;
using EmployeesInformationManager.Data;
using EmployeesInformationManager.Proxies;
using Newtonsoft.Json;

namespace EmployeesInformationManager.Services
{
    public class EmployeeServices:EmployeeRepository
    {

        public EmployeeServices(EmployeesInformationManagerContext context)
        :base(context){}
        public EmployeeModelView GetAsModelView(int id)
        {
            var serializedParent = JsonConvert.SerializeObject(
                this.GetById(id)); 
            return JsonConvert.DeserializeObject
            <EmployeeModelView>(serializedParent);
        }

        public async Task DeleteAsync(int id)
        {
            this.Delete(id);
            await this.SaveAsync();
        }
    }
}
