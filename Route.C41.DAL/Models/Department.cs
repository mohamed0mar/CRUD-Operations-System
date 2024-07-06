using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.DAL.Models
{
    public class Department:ModelBase
    {
        //Model =>We dont write here any data annoutation [validation]
        
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime DateOfCreation { get; set; }

        public  ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();

    }
}
