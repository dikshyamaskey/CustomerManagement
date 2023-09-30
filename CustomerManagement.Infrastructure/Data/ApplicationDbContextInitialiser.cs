using CustomerManagement.Application.Interface;
using CustomerManagement.Core.Entities;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Infrastructure.Data;

public class ApplicationDbContextInitialiser
{
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Employee> _employeeRepository;
    private readonly IRepository<Membership> _memberShipRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    public ApplicationDbContextInitialiser(IRepository<User> userRepository, IRepository<Employee> employeeRepository,IUnitOfWork unitOfWork,ILogger<ApplicationDbContextInitialiser> logger,
        IRepository<Membership> memberShipRepository)
    {
        _userRepository = userRepository;
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _memberShipRepository = memberShipRepository;
    }

    public async Task SeedAsync()
    {
        var cancellationToken = new CancellationToken();
        try
        {
            if (!_userRepository.Table().Any())
            {
                await TrySeedUserAsync(cancellationToken);
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
        }
    }
    
    private async Task TrySeedUserAsync(CancellationToken cancellationToken)
    {
        Guid[] ids = { new Guid("2AC00C5D-BF52-4160-864D-59A925BE7E3F"), new Guid("E6F57BBE-4CCB-45E1-9AEC-0BE7EABEFF2B")};
        var user = _userRepository.TableNoTracking().Any(x => ids.Contains(x.Id));
        if (!user)
        {
            var userRepo = new List<User>()
            {
                new User()
                {
                    Id = new Guid("2AC00C5D-BF52-4160-864D-59A925BE7E3F"),
                    FirstName = "Diksya",
                    LastName = "Maskey",
                    Address = "Maitidevi,Kathmandu",
                    EmailAddress = "dikshya.maksey@gmail.com",
                    UserName = "dikshya",
                    Password = "1234"
                },
                new User()
                {
                    Id = new Guid("E6F57BBE-4CCB-45E1-9AEC-0BE7EABEFF2B"),
                    FirstName = "Rozeen",
                    LastName = "Nakarmi",
                    Address = "Bhotahity,Kathmandu",
                    EmailAddress = "rozeen.nakarmi@gmail.com",
                    UserName = "rozeen",
                    Password = "1234"

                }
            };
            await _userRepository.AddRangeAsync(userRepo);
            await _unitOfWork.CommitAsync(cancellationToken);
            var emmployeeRepo = new List<Employee>()
            {
                new Employee()
                {
                    UserId = new Guid("2AC00C5D-BF52-4160-864D-59A925BE7E3F"),
                    JobTitle = "Senior Sales Person",
                    HireDate = DateTime.UtcNow
                },
                new Employee()
                {
                    UserId = new Guid("E6F57BBE-4CCB-45E1-9AEC-0BE7EABEFF2B"),
                    JobTitle = "CEO",
                    HireDate = DateTime.UtcNow
                }
            };
            await _employeeRepository.AddRangeAsync(emmployeeRepo);
            _memberShipRepository.InsertAsync(new Membership()
            {
                MembershipName = "Normal", 
                Description = "Normal Member",
            }, cancellationToken);
            
            await _unitOfWork.CommitAsync(cancellationToken);
        }
        
    }
}