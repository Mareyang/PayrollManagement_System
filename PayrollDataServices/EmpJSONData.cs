using System;
using PayrollManagementModels;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PayrollDataServices
{
    public class EmpJSONData : IEmployeeDataService
    {
        private List<Employee> employs = new List<Employee>();

        private string _jsonFileName;

        public EmpJSONData()
        {
            _jsonFileName = $"{AppDomain.CurrentDomain.BaseDirectory}/JSONEmployeePayroll.json";
            PopulateJsonFile();
        }

        private void PopulateJsonFile()
        {
            RetrieveDataFromJsonFile();

            if (employs.Count == 0)
            {
                employs.Add(new Employee { EmployeeId = Guid.NewGuid(), Name = "Thea", Position = "Manager", DaysWorked = 1, OvertimeHours = 1, DailyRate = 600, OvertimeRate = 95 });
            }
            ;
            SaveDataToJsonFile();
        }
        private void SaveDataToJsonFile()
        {
            using (var outputStream = File.OpenWrite(_jsonFileName))
            {
                JsonSerializer.Serialize<List<Employee>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    { SkipValidation = true, Indented = true })
                    , employs);
            }
        }

        private void RetrieveDataFromJsonFile()
        {
            using (var jsonFileReader = File.OpenText(this._jsonFileName))
            {
                this.employs = JsonSerializer.Deserialize<List<Employee>>
                    (jsonFileReader.ReadToEnd(), new JsonSerializerOptions
                    { PropertyNameCaseInsensitive = true })
                    .ToList();
            }
        }

        public void Add(Employee emp)
        {
            employs.Add(emp);
            SaveDataToJsonFile();
        }

        public List<Employee> GetEmployee()
        {
            RetrieveDataFromJsonFile();
            return employs;
        }

        public Employee? GetByEmployee(string employeeName)
        {
            RetrieveDataFromJsonFile();
            return employs.Where(x => x.Name == employeeName).FirstOrDefault();
        }

        public void UpdateEmployee(Employee employee)
        {
            RetrieveDataFromJsonFile();

            var existingEmployee = employs.FirstOrDefault(x => x.EmployeeId == employee.EmployeeId);
            if (existingEmployee != null)
            {
                existingEmployee.Name = employee.Name;
                existingEmployee.Position = employee.Position;
                existingEmployee.DaysWorked = employee.DaysWorked;
                existingEmployee.OvertimeHours = employee.OvertimeHours;
            }
            SaveDataToJsonFile();
        }

        public Employee? SearchEmployee(Guid id)
        {
            RetrieveDataFromJsonFile();
            return employs.Where(x => x.EmployeeId == id).FirstOrDefault();
        }
    }
}

