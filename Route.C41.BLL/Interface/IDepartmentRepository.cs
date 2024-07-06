using Route.C41.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.BLL.Interface
{
    public interface IDepartmentRepository:IGenericRepository<Department>
    {
        IQueryable<Department> SearchByName(string name);
    }
}
