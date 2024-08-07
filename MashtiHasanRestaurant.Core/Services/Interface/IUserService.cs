using MashtiHasanRestaurant.Core.DTOs;
using MashtiHasanRestaurant.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MashtiHasanRestaurant.Core.Services.Interface
{
    public interface IUserService
    {
        bool IsExistUserName(string userName);
    
       
         #region Customer
        public int AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        #endregion
        #region Employee
        public int AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);

        #endregion

       



    }
}
