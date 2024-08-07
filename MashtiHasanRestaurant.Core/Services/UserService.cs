using MashtiHasanRestaurant.Core.DTOs;
using MashtiHasanRestaurant.Core.Services.Interface;
using MashtiHasanRestaurant.DataLayer.Context;
using MashtiHasanRestaurant.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MashtiHasanRestaurant.Core.Services
{
    public class UserService : IUserService
    {
        private readonly ResturantMashtiHasanContext _context;
        public UserService(ResturantMashtiHasanContext context)
        {
            _context = context;
        }
        public int AddEmployee(Employee employee)
        {
            _context.Employee.Add(employee);
            _context.SaveChanges();
            return employee.EmployeeId;
        }
        public int AddCustomer(Customer customer)
        {
            _context.Customer.Add(customer);
            _context.SaveChanges();
            return customer.CustomerId;
        }
        public bool IsExistUserName(string mobile)
        {
            return _context.Customer.Any(u => u.Mobile == mobile);
        }

        public void UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public void UpdateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
