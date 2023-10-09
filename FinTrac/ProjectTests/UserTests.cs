using BusinessLogic;
using BusinessLogic.User_Components;
using NuGet.Frameworks;
using System.Runtime.ExceptionServices;

namespace TestProject1;
[TestClass]
public class UserTests
{
    #region Generic User
    private User genericUser;

    [TestInitialize]

    public void TestInitialize()
    {
        genericUser = new User();
    }
    #endregion

    #region Firstname
    [TestMethod]
    public void GivenCorrectUserName_ShouldReturnTrue()
    {
        User myUser = new User();
        myUser.FirstName = "Diego";
        Assert.AreEqual(true, myUser.ValidateFirstName(myUser.FirstName));
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateUser))]

    public void GivenEmptyUserName_ShouldThrowException()
    {
        string firstName = "";
        genericUser.ValidateFirstName(firstName);
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateUser))]
    public void GivenUserNameWithSpecialCaracters_ShouldThrowException()
    {
        string firstName = "Die!!@go";
        genericUser.ValidateFirstName(firstName);
    }



    #endregion

    #region LastName

    [TestMethod]
    public void GivenCorrectUserLastName_ShouldReturnTrue()
    {

        User myUser = new User();
        myUser.LastName = "Hernandez";

        Assert.AreEqual(true, myUser.ValidateLastName(myUser.LastName));
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateUser))]

    public void GivenEmptyOrNullUserLastName_ShouldThrowException()
    {
        string lastName = "";
        genericUser.ValidateLastName(lastName);
    }


    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateUser))]
    public void GivenUserLastNameWithSpecialCaracters_ShouldThrowException()
    {
        string lastName = "Her!!nande@z";
        genericUser.LastName = lastName;
        genericUser.ValidateLastName(genericUser.LastName);
    }

    #endregion

    #region Email

    [TestMethod]
    public void GivenCorrectEmail_ShouldReturnTrue()
    {
        string email = "diegohernandez@gmail.com";
        genericUser.Email = email;
        Assert.AreEqual(true, genericUser.ValidateEmail(genericUser.Email));
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateUser))]
    public void GivenUnformattedEmail_ShouldReturnException()
    {
        string email = "diego..Hernandez@gmail.com";
        genericUser.Email = email;
        genericUser.ValidateEmail(genericUser.Email);
    }
    #endregion

    #region Password
    [TestMethod]

    public void GivenCorrectPassword_ShouldReturnTrue()
    {

        genericUser.Password = "PasswordIsCorrect";

        Assert.AreEqual(true, genericUser.ValidatePassword(genericUser.Password));
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateUser))]
    public void GivenPasswordLessThanMinorLength_ShouldThrowException()
    {
        string password = "pass";
        genericUser.Password = password;
        genericUser.ValidatePassword(genericUser.Password);
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateUser))]
    public void GivenPasswordMoreThanMaxLength_ShouldThrowException()
    {
        string password = "12345123451234512345123451234512345";
        genericUser.Password = password;
        genericUser.ValidatePassword(genericUser.Password);
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateUser))]

    public void GivenPasswordWithoutUpperCaseLetter_ShouldThrowException()
    {

        string password = "passwordexample";
        genericUser.Password = password;
        genericUser.ValidatePassword(genericUser.Password);
    }

    #endregion

    #region Address

    [TestMethod]
    public void GivenCorrectAdress_ShouldReturnAdress()
    {
        User myUser = new User();
        myUser.Address = "97th Ave";
        Assert.AreEqual("97th Ave", myUser.Address);
    }
    #endregion

    #region GenericMethods

    [TestMethod]
    public void GivenStringStartingOrHavingAtTheEndSpaces_ShouldReturnStringWhithoutThem()
    {
        string stringExample = "    stringExample";
        Assert.AreEqual("stringExample", genericUser.RemoveAllUnsenseSpaces(stringExample));

        stringExample = "stringExample     ";
        Assert.AreEqual("stringExample", genericUser.RemoveAllUnsenseSpaces(stringExample));
    }

    #endregion

    #region UserCreation

    [TestMethod]
    public void CreatingUserWithValues_PropertiesValuesShouldBeEquals()
    {
        string firstName = "Austin";
        string lastName = "Ford";
        string email = "austinFord@gmail.com";
        string password = "AustinF2003";
        string address = "NW 2nd Ave";

        User myUser = new User(firstName, lastName, email, password, address);

        Assert.AreEqual(firstName, myUser.FirstName);
        Assert.AreEqual(lastName, myUser.LastName);
        Assert.AreEqual(email, myUser.Email);
        Assert.AreEqual(password, myUser.Password);
        Assert.AreEqual(address, myUser.Address);

    }
    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateUser))]
    public void GivenWrongValues_ShouldThrowException()
    {
        string firstName = "";
        string lastName = "Ford";
        string email = "austinFord@gmail.com";
        string password = "Austin1980";
        string address = "East 25 Av";
        User myUser = new User(firstName, lastName, email, password, address);
    }


    #endregion




}