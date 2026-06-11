using PayrollDataServices;
using PayrollManagementModels;
using System.Security.Principal;

namespace PayrollManagement
{
    public class payrollservices
    {

        //EmpJSONData employeeJson = new EmpJSONData();
        PayrollDataService payrolldataservice = new PayrollDataService(new PayrollDBData());

        double sss = 0.045;
        double philhealth = 0.025;
        double pagibig = 0.02;

        string adUser = "adminUser";
        string adPass = "admin123";

        public bool AdminLogin(string username, string password)
        {
            return username == adUser && password == adPass;
        }

        public void AddEmployee(Employee emp)
        {
            payrolldataservice.Add(emp);
            //employeeJson.Add(emp);
        }

        public List<Employee> GetEmployees()
        {
            return payrolldataservice.GetEmployee();
            // return employeeJson.GetEmployee();
        }
        public void UpdateEmployee(Guid id, string name, string position, int days, int oh)
        {

            var employee = payrolldataservice.GetEmployee();
            var emp = employee.FirstOrDefault(x => x.EmployeeId == id);

            if (emp != null)
            {
                emp.Name = name;
                emp.Position = position;
                emp.DaysWorked = days;
                emp.OvertimeHours = oh;
                payrolldataservice.UpdateEmployee(emp);
            }

        }
        public Employee? SearchEmployee(Guid empId)
        {
            return payrolldataservice.SearchByEmployee(empId);
            // return employeeJson.SearchEmployee(empId);
        }

        public double BasicPay(Employee e)
        {
            return e.DaysWorked * e.DailyRate;
        }

        public double OvertimePay(Employee e)
        {
            return e.OvertimeHours * e.OvertimeRate;
        }

        public double TotalEarnings(Employee e)
        {
            return BasicPay(e) + OvertimePay(e);
        }

        public double PerSSS(Employee e)
        {
            double earnings = TotalEarnings(e);
            return (earnings * sss);
        }
        public double PerPhil(Employee e)
        {
            double earnings = TotalEarnings(e);
            return (earnings * philhealth);
        }
        public double PerPagIbig(Employee e)
        {
            double earnings = TotalEarnings(e);
            return (earnings * pagibig);
        }

        public double TotalDeduction(Employee e)
        {
            double earnings = TotalEarnings(e);
            return (earnings * sss) + (earnings * philhealth) + (earnings * pagibig);
        }


        public double NetPay(Employee e)
        {
            return TotalEarnings(e) - TotalDeduction(e);
        }
    }
}