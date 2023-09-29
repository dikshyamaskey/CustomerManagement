using CustomerManagement.Application.User.Commands.CreateUser;
using CustomerManagement.Core.Common.Model;
using CustomerManagement.Application.Interface;
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
        IList<Customer> Get();
        Task InsertAsync(Customer model, CancellationToken cancellationToken);
    }
    public class CustomerService : ICustomerService
    {
      private readonly  IRepository<Customer> _customerRepository;
      private readonly  IRepository<User> _userRepository;
       private readonly IUnitofWorkFactory _unitOfWork;

        public CustomerService(IRepository<Customer> customerRepository, IRepository<User> userRepository,IUnitofWorkFactory unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }
        public IList<Customer> Get()
        {
          return  _customerRepository.TableNoTracking().ToList();
          
        }
        public async Task InsertAsync(Customer model,CancellationToken cancellationToken)
        {

            using(var uow = _unitOfWork.CreateUnitOfWork())
            {
                _customerRepository.Insert(model);
                var user = new User();
              //  await _customerRepository.SaveChangesAsync(cancellationToken);
              //  _customerRepository.Insert(model);
               // await _userRepository.SaveChangesAsync(cancellationToken);
                await uow.CommitAsync();
            }
         
        }
    }
}
