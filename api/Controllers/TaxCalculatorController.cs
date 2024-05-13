using AuthReadyAPI.DataLayer.DTOs.TaxCalculator;
using AuthReadyAPI.DataLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthReadyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxCalculatorController : ControllerBase
    {
        private readonly ITaxCalculatorRepository _taxCalculatorRepository;

        public TaxCalculatorController(ITaxCalculatorRepository taxCalculatorRepository)
        {
            _taxCalculatorRepository = taxCalculatorRepository;
        }

        [HttpPost]
        [Route("calculate")]
        [Authorize(Roles = "Accountant")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CalculatePaidTaxes([FromBody] CalculatedTaxDTO DTO)
        {
            return Ok(await _taxCalculatorRepository.Calculate(DTO));
        }

        [HttpGet]
        [Route("currencies")]
        [Authorize(Roles = "Accountant")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCurrencies()
        {
            return Ok(await _taxCalculatorRepository.GetAllAvailableCurrencies());
        }
    }
}
