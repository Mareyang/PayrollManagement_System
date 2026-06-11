using PayrollManagementModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollDataServices
{
    public interface IEmployeeDataService
    {
        void Add(Employee employees);
        void UpdateEmployee(Employee employee);
        Employee? GetByEmployee(string employeeName);
        Employee? SearchEmployee(Guid Id);
        List<Employee> GetEmployee();
    }
}
