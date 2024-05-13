using AuthReadyAPI.DataLayer.DTOs.Employees;
using AuthReadyAPI.DataLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthReadyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpPost]
        [Route("update")]
        [Authorize(Roles = "Accountant")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeDTO DTO)
        {
            return Ok(await _employeeRepository.UpdateEmployee(DTO));
        }

        [HttpGet]
        [Route("all")]
        [Authorize(Roles = "Accountant")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployeeList()
        {
            return Ok(await _employeeRepository.GetAllEmployees());
        }

        [HttpGet]
        [Route("one/{EmployeeId}")]
        [Authorize(Roles = "Accountant")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployee([FromRoute] int EmployeeId)
        {
            return Ok(await _employeeRepository.GetSingleEmployee(EmployeeId));
        }
    }
}
