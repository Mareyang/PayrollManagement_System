using System.Collections.Generic;
using System.Linq;
using PayrollManagementModels;

namespace PayrollDataServices
{
    public class Data
    {
        private List<Employee> employees = new List<Employee>();

        public Data()
        {
            employees.Add(new Employee
            {
                EmployeeId = Guid.NewGuid(),
                Name = "Employee A",
                Position = "Crew",
                DaysWorked = 12,
                OvertimeHours = 2,
                DailyRate = 600,
                OvertimeRate = 92
            });

            employees.Add(new Employee
            {
                EmployeeId = Guid.NewGuid(),
                Name = "Employee B",
                Position = "Crew",
                DaysWorked = 10,
                OvertimeHours = 3,
                DailyRate = 600,
                OvertimeRate = 92
            });
        }

        public List<Employee> GetEmployees()
        {
            return employees;
        }

        public Employee GetEmployeeById(Guid id)
        {
            return employees.FirstOrDefault(e => e.EmployeeId == id);
        }

        public void AddEmployee(Employee emp)
        {
            employees.Add(emp);
        }
    }
}