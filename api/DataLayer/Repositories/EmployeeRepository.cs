using AuthReadyAPI.DataLayer.DTOs.Employees;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models.Employees;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private AuthDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeRepository(AuthDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<EmployeeDTO> GetSingleEmployee(int EmployeeId)
        {
            return await _context.Employees.Where(x => x.Id == EmployeeId).ProjectTo<EmployeeDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }


        public async Task<List<EmployeeDTO>> GetAllEmployees()
        {
            return await _context.Employees.ProjectTo<EmployeeDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<bool> UpdateEmployee(EmployeeDTO DTO)
        {
            EmployeeClass Obj = _mapper.Map<EmployeeClass>(DTO);
            _context.Employees.Update(Obj);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
