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
        throw new NotImplementedException();
    }
}