using BusinessLogicTests;

namespace TestProject1;
[TestClass]
public class UserTests
{

    #region Password
    [TestMethod]

    public void ShouldReturnTrue_GivenPassword()
    {

        User myUser = new User();
        myUser.Password = "PasswordIsCorrect";

        Assert.AreEqual(true, User.ValidatePassword(myUser.Password));
    }







    #endregion










}