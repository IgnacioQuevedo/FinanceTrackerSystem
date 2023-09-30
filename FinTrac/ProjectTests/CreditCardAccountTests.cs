using BusinessLogic;
using BusinessLogic.User;
using NuGet.Frameworks;
using System.Runtime.ExceptionServices;
using BusinessLogic.Account;
using System.Security.Principal;

namespace TestProject1;

[TestClass]
public class CreditCardAccountTests
{
    private Account myCreditCard;
    private CreditCardAccount myCreditCardAccount;

    #region Init
    [TestInitialize]
    public void TestInitialized()
    {
        //myCreditCard is a polimorfed object, we make this so we can validate that polimorfic object are also passing the necessary tests.
        myCreditCard = new CreditCardAccount();

        //myCreditCardAccount is 100% a sublclass object.
        myCreditCardAccount = new CreditCardAccount();

        //Initiale of myCreditCardAccount
        myCreditCardAccount.Name = "GenericName";
        myCreditCardAccount.IssuingBank = "GenericBank";
        myCreditCardAccount.Currency = CurrencyEnum.UY;
    }
    #endregion

    #region Name
    [TestMethod]
    public void GivenCorrectName_ShouldBeSetted()
    {
        string name = "Itau volar";
        myCreditCard.Name = name;
        Assert.AreEqual(name, myCreditCard.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateAccount))]
    public void GivenEmptyName_ShouldThrowException()
    {
        string name = "";
        myCreditCard.Name = name;

        myCreditCard.ValidateAccount();
    }
    #endregion

    #region CreationDate
    [TestMethod]

    public void DateofCreditCard_ShouldBeActualDate()
    {
        string actualDate = DateTime.Now.ToString("dd/MM/yyyy");
        Assert.AreEqual(actualDate, myCreditCard.CreationDate);
    }
    #endregion

    #region Currency
    [TestMethod]
    public void GivenCurrency_ShouldBelongToCurrencyEnum()
    {
        myCreditCard.Currency = CurrencyEnum.UY;
        bool belongToEnum = Enum.IsDefined(typeof(CurrencyEnum), myCreditCard.Currency);

        Assert.IsTrue(belongToEnum);

    }
    #endregion

    #region IssuingBank
    [TestMethod]
    public void GivenIssuingBank_ShouldBeAssignedToCreditCard()
    {
        string issuingBank = "Brou";

        myCreditCardAccount.IssuingBank = issuingBank;
        Assert.AreEqual(myCreditCardAccount.IssuingBank, issuingBank);
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateAccount))]

    public void GivenEmptyIssuingBank_ShouldThrowException()
    {
        string issuingBank = string.Empty;
        myCreditCardAccount.IssuingBank = issuingBank;

        myCreditCardAccount.ValidateCreditCardAccount();
    }
    #endregion


    #region Last4Digits
    [TestMethod]

    public void GivenLast4Digits_ShouldBeAssigned()
    {
        string last4Digits = "2354";
        myCreditCardAccount.Last4Digits = last4Digits;

        Assert.AreEqual(myCreditCardAccount.Last4Digits, last4Digits);

    }


    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateAccount))]

    public void GivenEmpty4LastDigits_ShouldThrowException()
    {
        myCreditCardAccount.Last4Digits = "";
        
        myCreditCardAccount.ValidateCreditCardAccount();
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateAccount))]

    public void GivenLessThan4Digits_ShouldThrowException()
    {
        myCreditCardAccount.Last4Digits = "123";
        myCreditCardAccount.ValidateCreditCardAccount();

    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateAccount))]

    public void GivenIncorrectFormatOfLast4Digits_ShouldThrowException()
    {

        myCreditCardAccount.Last4Digits= "123A";
        myCreditCardAccount.ValidateCreditCardAccount();

    }

    #endregion

}