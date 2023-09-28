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

          
            return Ok(_customerService.Get());
        }
        [HttpPost]
        [Route("/test")]
        public async Task<ActionResult> CreateCustomer(Customer model,CancellationToken cancellationToken)
        {
            await _customerService.InsertAsync(model, cancellationToken);

            return Ok();
        }
    }
    public interface ICustomerService
    {
        int Get();
        Task InsertAsync(Customer model, CancellationToken cancellationToken);
    }
    public class CustomerService : ICustomerService
    {
        IRepository<Customer> _customerRepository;
        public CustomerService(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public int Get()
        {
            _customerRepository.TableNoTracking().ToList();
            return 0;
        }
        public async Task InsertAsync(Customer model,CancellationToken cancellationToken)
        {
            _customerRepository.Insert(model);
            await _customerRepository.SaveChangesAsync(cancellationToken);
        
        }
    }
}
