using BusinessLogic;
using BusinessLogic.User;

namespace TestProject1;
[TestClass]
public class UserTests
{

    #region Password
    [TestMethod]

    public void GivenCorrectPassword_ShouldReturnTrue()
    {

        User myUser = new User();
        myUser.Password = "PasswordIsCorrect";

        Assert.AreEqual(true, User.ValidatePassword(myUser.Password));
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateUser))]
    public void GivenPasswordLessThanMinorLength_ShouldThrowException()
    {
  
        User.ValidatePassword("pass");
       

    }





    #endregion










}