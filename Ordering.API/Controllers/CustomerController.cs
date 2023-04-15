using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Odering.Core.Entities;
using Ordering.Application.Commands.Customers.Create;
using Ordering.Application.Commands.Customers.Delete;
using Ordering.Application.Commands.Customers.Update;
using Ordering.Application.DTOs;
using Ordering.Application.Queries.Customers;
using System.Data;

namespace Ordering.API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin,Member")]
    //[Authorize]
    //[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Member")]

    // Authorize with a specific scheme
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Member,User")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(IMediator mediator, ILogger<CustomerController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Customer>> Get()
        {
            _logger.LogInformation("Get all cutomers");
            return await _mediator.Send(new GetAllCustomerQuery());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Customer> Get(Guid id)
        {
            _logger.LogInformation($"Get cutomers {id}");
            return await _mediator.Send(new GetCustomerByIdQuery(id));
        }

        [HttpGet("email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Customer> GetByEmail(string email)
        {
            _logger.LogInformation($"Get cutomers {email}");
            return await _mediator.Send(new GetCustomerByEmailQuery(email));
        }

        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CustomerResponse>> CreateCustomer([FromBody] CreateCustomerCommand command)
        {
            _logger.LogInformation("Create new cutomer");
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpPut("Edit/{id}")]
        public async Task<ActionResult> Edit(Guid id, [FromBody] EditCustomerCommand command)
        {
            try
            {
                if (command.Id == id)
                {
                    _logger.LogInformation($"Update cutomers {id}");
                    var result = await _mediator.Send(command);
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return BadRequest(exp.Message);
            }
        }


        [Authorize(Roles = "Admin, Management")]
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteCustomer(Guid id)
        {
            try
            {
                _logger.LogInformation($"Delete cutomer {id}");
                string result = string.Empty;
                result = await _mediator.Send(new DeleteCustomerCommand(id));
                return Ok(result);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return BadRequest(exp.Message);
            }
        }

    }
}
