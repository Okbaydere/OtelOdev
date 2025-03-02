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
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerdal;
        public CustomerManager(ICustomerDal customerDal)
        {
            _customerdal = customerDal;
        }
        void ICustomerService.CustomerDelete(Customer c)
        {
            throw new NotImplementedException();
        }

        public void CustomerInsert(Customer customer)
        {
            _customerdal.Insert(customer);
        }

        List<Customer> ICustomerService.Customerliste()
        {
            throw new NotImplementedException();
        }

        void ICustomerService.CustomerUpdate(Customer c)
        {
            throw new NotImplementedException();
        }

        public Customer GetById(int id)
        {
             return _customerdal.Get(x => x.CustomerID == id);
        }
    }
}
