using Microsoft.Data.SqlClient;
using PayrollManagementModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PayrollDataServices
{
    public class PayrollDBData : IEmployeeDataService
    {
        private string connectionString
            = "Data Source = localhost\\SQLEXPRESS; Initial Catalog = DBEmployeesPayroll; Integrated Security = True; TrustServerCertificate=True;";

        private SqlConnection sqlConnection;

        public PayrollDBData()
        {
            sqlConnection = new SqlConnection(connectionString);
            AddSeeds();
        }

        private void AddSeeds()
        {
            var existing = GetEmployee();

            if (existing.Count == 0)
            {
                Employee employee = new Employee
                {
                    EmployeeId = Guid.NewGuid(),
                    Name = "Thea",
                    Position = "Manager",
                    DaysWorked = 1,
                    OvertimeHours = 1,
                    DailyRate = 600,
                    OvertimeRate = 95
                };
                Add(employee);

            }
        }

        public void Add(Employee employees)
        {
            var insertStatement = "INSERT INTO TBLEmployee VALUES(@EmployeeId,@Name,@Position,@DaysWorked,@OvertimeHours,@DailyRate,@OvertimeRate)";
            SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);

            insertCommand.Parameters.AddWithValue("@EmployeeId", employees.EmployeeId);
            insertCommand.Parameters.AddWithValue("@Name", employees.Name);
            insertCommand.Parameters.AddWithValue("@Position", employees.Position);
            insertCommand.Parameters.AddWithValue("@DaysWorked", employees.DaysWorked);
            insertCommand.Parameters.AddWithValue("@OvertimeHours", employees.OvertimeHours);
            insertCommand.Parameters.AddWithValue("@DailyRate", employees.DailyRate);
            insertCommand.Parameters.AddWithValue("@OvertimeRate", employees.OvertimeRate);
            sqlConnection.Open();
            insertCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public List<Employee> GetEmployee()
        {
            string selectStatement = "SELECT EmployeeId, Name, Position, DaysWorked, OvertimeHours, DailyRate, OvertimeRate FROM TBLEmployee";

            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);

            sqlConnection.Open();

            SqlDataReader reader = selectCommand.ExecuteReader();

            var employ = new List<Employee>();

            while (reader.Read())
            {
                //deserialize

                Employee emp = new Employee();
                emp.EmployeeId = Guid.Parse(reader["EmployeeId"].ToString());
                emp.Name = reader["Name"].ToString();
                emp.Position = reader["Position"].ToString();
                emp.DaysWorked = Convert.ToInt32(reader["DaysWorked"].ToString());
                emp.OvertimeHours = Convert.ToInt32(reader["OvertimeHours"].ToString());
                emp.DailyRate = Convert.ToInt32(reader["DailyRate"].ToString());
                emp.OvertimeRate = Convert.ToInt32(reader["OvertimeRate"].ToString());

                employ.Add(emp);
            }

            sqlConnection.Close();
            return employ;

        }
        public Employee? GetByEmployee(string employeeName)
        {
            var selectStatement = "SELECT Name FROM DBEmployeesPayroll";
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            selectCommand.Parameters.AddWithValue("@Name", employeeName);
            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();

            var emp = new Employee();

            while (reader.Read())
            {
                emp.EmployeeId = Guid.Parse(reader["EmployeeId"].ToString());
                emp.Name = reader["Name"].ToString();
            }

            sqlConnection.Close();
            return emp;
        }
        public void UpdateEmployee(Employee employee)
        {
            sqlConnection.Open();

            var updateStatement = $"UPDATE TBLEmployee SET EmployeeId = @EmployeeId, Name = @Name, Position = @Position, DaysWorked = @DaysWorked, OvertimeHours = @OvertimeHours WHERE EmployeeId = @EmployeeId";

            SqlCommand updateCommand = new SqlCommand(updateStatement, sqlConnection);
            updateCommand.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
            updateCommand.Parameters.AddWithValue("@Name", employee.Name);
            updateCommand.Parameters.AddWithValue("@Position", employee.Position);
            updateCommand.Parameters.AddWithValue("@DaysWorked", employee.DaysWorked);
            updateCommand.Parameters.AddWithValue("@OvertimeHours", employee.OvertimeHours);
            updateCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }
        public Employee? SearchEmployee(Guid Id)
        {
            var selectStatement = "SELECT EmployeeId, Name, Position, DaysWorked, OvertimeHours, DailyRate, OvertimeRate FROM TBLEmployee WHERE EmployeeId = @EmployeeId";
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            selectCommand.Parameters.AddWithValue("@EmployeeId", Id);
            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();

            var emp = new Employee();

            while (reader.Read())
            {
                emp.EmployeeId = Guid.Parse(reader["EmployeeId"].ToString());
                emp.Name = reader["Name"].ToString();
                emp.Position = reader["Position"].ToString();
                emp.DaysWorked = Convert.ToInt32(reader["DaysWorked"].ToString());
                emp.OvertimeHours = Convert.ToInt32(reader["OvertimeHours"].ToString());
                emp.DailyRate = Convert.ToInt32(reader["DailyRate"].ToString());
                emp.OvertimeRate = Convert.ToInt32(reader["OvertimeRate"].ToString());
            }

            sqlConnection.Close();
            return emp;

        }
    }

}