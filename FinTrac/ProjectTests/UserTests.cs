using BusinessLogic;
using BusinessLogic.User;

namespace TestProject1;
[TestClass]
public class UserTests
{

    #region Firstname
    [TestMethod]
    public void GivenCorrectName_ShouldReturnTrue()
    {
        User myUser = new User();
        myUser.FirstName = "Diego";
        Assert.AreEqual(true, User.ValidateFirstName(myUser.FirstName));
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateUser))]

    public void GivenEmptyName_ShouldThrowException()
    {
        string firstName = "";
        User.ValidateFirstName(firstName);
    }

    [TestMethod]
    public void GivenNameStartingOrHavingAtTheEndWithSpaces_ShouldReturnNameWhithoutThem()
    {
        string firstName = "    Diego";
        Assert.AreEqual("Diego", User.CorrectFirstName(firstName));

        firstName = "Diego     ";
        Assert.AreEqual("Diego", User.CorrectFirstName(firstName));
    }


    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateUser))]
    public void GivenNameWithSpecialCaracters_ShouldThrowException()
    {
        string firstName = "Die!!@go";
        User.ValidateFirstName(firstName);
    }



    #endregion

    #region LastName

    [TestMethod]
    public void GivenCorrectLastName_ShouldReturnTrue()
    {

        User myUser = new User();
        myUser.LastName = "Hernandez";

        Assert.AreEqual(true, User.ValidateLastName(myUser.LastName));

    }
    #endregion

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