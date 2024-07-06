using Route.C41.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.BLL.Interface
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        //IGenericRepository<T> repository<T>() where T : ModelBase;
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }
        Task<int> Complete();

    }
}
