using BusinessLogic.Dtos_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.User_Components;

namespace Controller.IControllers
{
    public interface IUserController
    {
        public void RegisterUser(UserDTO userDtoToCreate);
        public bool LoginUser(UserLoginDTO userToLogin);
        public void UpdateUser(UserDTO userDto);
        public UserDTO FindUser(int userId);
        public void PasswordMatch(string password, string passwordRepeated);

        public void DesactiveUserConnected(int? userIdToDesactivate);
    }
}