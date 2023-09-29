using CustomerManagement.Application.User.Commands.CreateUser;
using CustomerManagement.Core.Common.Model;
using CustomerManagement.Core.Entities;
using CustomerManagement.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
                _customerService = customerService;
        }

        [HttpGet]
        [Route("/test")]
        public ActionResult Custmer()
        {
            _customerService.Get();
            return Ok();
        }
    }

    public class RozeenController : CustomerBaseController
    {
        [HttpPost]
        public async Task<ActionResult<Result<string>>> CreateUser([FromBody] CreateUserCommand command)
        {
            return await Mediator.Send(command);
        }

    }
    public interface ICustomerService
    {
        int Get();
        
    }
    public class CustomerService : ICustomerService
    {
       // IRepository<Customer> _customerRepository;
        public CustomerService(IRepository<Customer> customerRepository)
        {
           // _customerRepository = customerRepository;
        }
        public int Get()
        {
           // _customerRepository.TableNoTracking().ToList();
            return 0;
        }
    }
}
