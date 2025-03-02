using Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IEmployeesService
    {
        List<Employees> Employeesliste();
        void EmployeesInsert(Employees e);
        void EmployeesUpdate(Employees e);
        void EmployeesDelete(Employees e);
        Employees GetById(int id);
        List<Employees> GetActiveEmployees();
    }
}
