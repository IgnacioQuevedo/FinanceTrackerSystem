using BusinessLogic.Exceptions;
using BusinessLogic.User_Components;


namespace DataManagers;

public class UserRepositorySql
{
    private SqlContext _database;

    public UserRepositorySql(SqlContext database)
    {
        _database = database;
    }


    public void Create(User userToAdd)
    {
        _database.Users.Add(userToAdd);
        _database.SaveChanges();
    }

    public void EmailUsed(string UserEmail)
    {
        if (FindUserInDb(UserEmail) != null)
        {
            throw new ExceptionUserRepository("Email already registered, impossible to create another account.");
        }
    }
    
public bool UserRegistered(User userToBeLogged)
{
    foreach (var account in _database.Users)
    {
        if (account.Email.Equals(userToBeLogged.Email))

        {
            if (account.Password.Equals(account.Password))
            {
                return true;
            }
        }
    }
    return false;
}
public void Update(User updatedUser)
{
    var existingUser = FindUserInDb(updatedUser.Email);
    
        updatedUser.UserId = existingUser.UserId;
        _database.Entry(existingUser).CurrentValues.SetValues(updatedUser);
        _database.SaveChanges();
}

public User FindUserInDb(string emailAK)
{
    return _database.Users.FirstOrDefault(u => u.Email == emailAK);
}

}