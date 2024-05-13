namespace AuthReadyAPI.DataLayer.DTOs.Employees
{
    public class EmployeeDTO : BaseDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double GrossAnnualSalary { get; set; }
        public string Currency { get; set; }
    }
}
