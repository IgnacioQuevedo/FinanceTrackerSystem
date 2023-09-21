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
        string password = "pass";
        User.ValidatePassword(password);
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateUser))]
    public void GivenPasswordMoreThanMaxLength_ShouldThrowException()
    {
        string password = "12345123451234512345123451234512345";
        User.ValidatePassword(password);
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateUser))]

    public void GivenPasswordWithoutUpperCaseLetter_ShouldThrowException()
    {

        string password = "passwordexample";
        User.ValidatePassword(password);
    }

    #endregion










}