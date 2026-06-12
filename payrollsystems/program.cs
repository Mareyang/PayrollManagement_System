using PayrollManagement;
using PayrollManagementModels;
using System.Data.Common;

namespace payrollsystem
{
    internal class Program
    {
        static payrollservices payrollService = new payrollservices();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("------ PAYROLL MANAGEMENT SYSTEM ------");

                Console.WriteLine("[1] ADMIN");
                Console.WriteLine("[2] EMPLOYEE");
                Console.WriteLine("[3] EXIT");
                Console.Write("Choose Option: ");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        AdminLogin();
                        break;
                    case "2":
                        EmployeePayslip();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid.");
                        break;
                }


            }

            static void AdminLogin()
            {
                Console.Write("Username: ");
                string username = Console.ReadLine();

                Console.Write("Password: ");
                string password = Console.ReadLine();

                if (payrollService.AdminLogin(username, password))
                {
                    AdminMenu();
                }
                else
                {
                    Console.WriteLine("Invalid login.");
                }
            }

            static void AdminMenu()
            {
                string option = "0";

                while (option != "4")
                {
                    Console.WriteLine("\nADMIN MENU");
                    Console.WriteLine("[1] Add Employee");
                    Console.WriteLine("[2] View Employees");
                    Console.WriteLine("[3] Search Employee");
                    Console.WriteLine("[4] Update Employee");
                    Console.WriteLine("[5] Exit Admin Menu");

                    Console.Write("Choose: ");
                    option = Console.ReadLine();

                    if (option == "1")
                        AddEmployee();

                    else if (option == "2")
                        ViewEmployees();

                    else if (option == "3")
                        SearchEmployee();

                    else if (option == "4")
                        UpdateEmployee();

                    else if (option == "5")
                        Environment.Exit(0);

                }
            }

            static void AddEmployee()
            {
                Employee emp = new Employee();

                Console.Write("Name: ");
                emp.Name = Console.ReadLine();

                Console.Write("Position: ");
                emp.Position = Console.ReadLine();

                Console.Write("Days Worked: ");
                emp.DaysWorked = Convert.ToInt32(Console.ReadLine());

                Console.Write("Overtime Hours: ");
                emp.OvertimeHours = Convert.ToInt32(Console.ReadLine());

                emp.DailyRate = 600;
                emp.OvertimeRate = 92;

                payrollService.AddEmployee(emp);

                Console.WriteLine("Employee added successfully.");
            }

            static void ViewEmployees()
            {
                var employees = payrollService.GetEmployees();
                var emp = employees.FirstOrDefault();

                for (int i = 0; i < employees.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {employees[i].Name}");

                }

            }

            static void UpdateEmployee()
            {
                var emp = payrollService.GetEmployees();
                Console.Write("Enter Employee ID to Edit: ");
                int index = Convert.ToInt32(Console.ReadLine()) - 1;

                if (index < 0 || index >= emp.Count)
                {
                    Console.Write("Invalid");
                    return;
                }
                Guid selectedI = emp[index].EmployeeId;


                if (selectedI != null)
                {
                    Console.Write("New Name: ");
                    string name = Console.ReadLine();

                    Console.Write("New Position: ");
                    string position = Console.ReadLine();

                    Console.Write("Days Worked: ");
                    int days = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Overtime Hours: ");
                    int oh = Convert.ToInt32(Console.ReadLine());

                    payrollService.UpdateEmployee(selectedI, name, position, days, oh);

                    Console.WriteLine("Employee updated successfully.");
                }
                else
                {
                    Console.WriteLine("Employee not found.");
                }
            }

            static void SearchEmployee()
            {
                var emp = payrollService.GetEmployees();
                Console.Write("Enter Employee ID to Search: ");
                int index = Convert.ToInt32(Console.ReadLine()) - 1;

                if (index < 0 || index >= emp.Count)
                {
                    Console.Write("Invalid");
                    return;
                }
                Guid selectedI = emp[index].EmployeeId;

                var e = payrollService.SearchEmployee(selectedI);

                if (selectedI != null)

                {
                    Console.WriteLine($"{e.EmployeeId} - {e.Name}");

                    double earnings = payrollService.TotalEarnings(e);
                    double deduction = payrollService.TotalDeduction(e);
                    double SSSdeduct = payrollService.PerSSS(e);
                    double Phildeduct = payrollService.PerPhil(e);
                    double PagIbigdeduct = payrollService.PerPagIbig(e);
                    double net = payrollService.NetPay(e);

                    Console.WriteLine("\n------ PAYSLIP ------");
                    Console.WriteLine("Name: " + e.Name);
                    Console.WriteLine("Position: " + e.Position);
                    Console.WriteLine("Total Earnings: " + earnings);

                    Console.WriteLine("\n------ DEDUCTION ------");
                    Console.WriteLine("SSS: " + SSSdeduct.ToString("0.00"));
                    Console.WriteLine("PhilHealth: " + Phildeduct.ToString("0.00"));
                    Console.WriteLine("Pagibig: " + PagIbigdeduct.ToString("0.00"));
                    Console.WriteLine("TOTAL DEDUCTION: " + deduction.ToString("0.00"));

                    Console.WriteLine("\n------ NET PAY ------");
                    Console.WriteLine("Net Pay: " + net);
                }

                else
                    Console.WriteLine("Employee not found.");
            }
            static void EmployeePayslip()
            {
                var e = payrollService.GetEmployees();

                Console.Write("Enter Employee ID: ");
                int index = Convert.ToInt32(Console.ReadLine()) - 1;

                Guid selectedI = e[index].EmployeeId;
                var emp = payrollService.SearchEmployee(selectedI);

                if (emp == null)
                {
                    Console.WriteLine("Employee not found.");
                    return;
                }

                double earnings = payrollService.TotalEarnings(emp);
                double deduction = payrollService.TotalDeduction(emp);
                double SSSdeduct = payrollService.PerSSS(emp);
                double Phildeduct = payrollService.PerPhil(emp);
                double PagIbigdeduct = payrollService.PerPagIbig(emp);
                double net = payrollService.NetPay(emp);

                Console.WriteLine("\n------ PAYSLIP ------");
                Console.WriteLine("Name: " + emp.Name);
                Console.WriteLine("Position: " + emp.Position);
                Console.WriteLine("Total Earnings: " + earnings);

                Console.WriteLine("\n------ DEDUCTION ------");
                Console.WriteLine("SSS: " + SSSdeduct);
                Console.WriteLine("PhilHealth: " + Phildeduct);
                Console.WriteLine("Pagibig: " + PagIbigdeduct);
                Console.WriteLine("TOTAL DEDUCTION: " + deduction);

                Console.WriteLine("\n------ NET PAY ------");
                Console.WriteLine("Net Pay: " + net);
            }

        }
    }
}






