using AuthReadyAPI.DataLayer.DTOs.Employees;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IEmployeeRepository
    {
        public Task<bool> UpdateEmployee(EmployeeDTO DTO);
        public Task<List<EmployeeDTO>> GetAllEmployees();
        public Task<EmployeeDTO> GetSingleEmployee(int EmployeeId);
    }
}
