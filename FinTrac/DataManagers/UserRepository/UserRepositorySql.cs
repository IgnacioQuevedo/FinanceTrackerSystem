using BusinessLogic.User_Components;
using DataManagers;

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
        foreach (var someUser in _database.Users)
        {
            if (someUser.Email.Equals(UserEmail.ToLower()))
            {
                throw new ExceptionUserRepository("Email already registered, impossible to create another account.");
            }
        }
    }

    public bool userRegistered(User userToBeLogged)
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
        var existingUser = _database.Users.Find(updatedUser.UserId);
        
        _database.Entry(existingUser).CurrentValues.SetValues(updatedUser);
        _database.SaveChanges();
        
    }
}