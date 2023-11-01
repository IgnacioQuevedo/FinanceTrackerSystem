using BusinessLogic.User_Components;

namespace BusinessLogic.Dto_Components;

public class UserDTO
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; }
    public string Address { get; set; }
    public UserDTO()
    {

    }

    public UserDTO(string firstName, string lastName, string email,string password, string address)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Address = address;
        Password = password;
    }

}