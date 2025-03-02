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

        public void CustomerDelete(Customer c)
        {
            _customerdal.Delete(c);
        }

        public void CustomerInsert(Customer customer)
        {
            _customerdal.Insert(customer);
        }

        public List<Customer> Customerliste()
        {
            return _customerdal.liste();
        }

        public void CustomerUpdate(Customer c)
        {
            _customerdal.Update(c);
        }

        public Customer GetById(int id)
        {
            return _customerdal.Get(x => x.CustomerID == id);
        }

        public Customer GetByTc(string tc)
        {
            return _customerdal.Get(x => x.CustomerTc == tc);
        }
    }
}
