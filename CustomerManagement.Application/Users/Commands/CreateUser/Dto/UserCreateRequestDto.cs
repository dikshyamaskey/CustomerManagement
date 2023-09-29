namespace CustomerManagement.Application.Users.Commands.CreateUser.Dto;

public class UserCreateRequestDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string EmailAddress { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}