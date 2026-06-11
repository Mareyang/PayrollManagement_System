namespace PayrollManagementModels
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }

        public int DaysWorked { get; set; }
        public int OvertimeHours { get; set; }

        public double DailyRate { get; set; }
        public double OvertimeRate { get; set; }
    }
}