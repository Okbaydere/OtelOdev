using Business.Abstract;
using Data.Abstract;
using Data.EntityFramwork;
using Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concreate
{
    public class EmployeeManager : IEmployeesService
    {
        IEmployeesDal _employeeDal;
         public EmployeeManager(IEmployeesDal employeeDal) { 
            _employeeDal = employeeDal;
         }

        public void EmployeesDelete(Employees e)
        {
            // Fiziksel silme yerine IsActive = false yaparak deaktif ediyoruz
            e.IsActive = false;
            _employeeDal.Update(e);
        }

        public void EmployeesInsert(Employees e)
        {
            // Yeni eklenen çalışan varsayılan olarak aktif
            e.IsActive = true;
            _employeeDal.Insert(e);
        }

        public List<Employees> Employeesliste()
        {
            return _employeeDal.liste();
        }

        public void EmployeesUpdate(Employees e)
        {
            _employeeDal.Update(e);
        }

        public Employees GetById(int id)
        {
            return _employeeDal.Get(x => x.EmployeeId == id);
        }
        
        // Sadece aktif çalışanları listeleyen yeni metot
        public List<Employees> GetActiveEmployees()
        {
            return _employeeDal.liste(x => x.IsActive == true);
        }
    }
}
