using PayrollManagementModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PayrollDataServices
{
    public class PayrollDataService
    {
        IEmployeeDataService dataservice;
        public PayrollDataService(IEmployeeDataService iEmployeeDataService)
        {
            dataservice = iEmployeeDataService;
        }
        public void Add(Employee employ)
        {
            dataservice.Add(employ);
        }

        public List<Employee> GetEmployee()
        {
            return dataservice.GetEmployee();
        }

        public void UpdateEmployee(Employee employee)
        {
            dataservice.UpdateEmployee(employee);
        }
        public Employee? SearchByEmployee(Guid id)
        {
            return dataservice.SearchEmployee(id);
        }


    }
}