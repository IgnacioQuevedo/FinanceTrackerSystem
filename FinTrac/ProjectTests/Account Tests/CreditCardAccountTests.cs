using BusinessLogic;
using BusinessLogic.Enums;
using BusinessLogic.User_Components;
using NuGet.Frameworks;
using System.Runtime.ExceptionServices;
using BusinessLogic.Transaction_Components;
using BusinessLogic.Account_Components;
using System.Security.Principal;
using System.Security.Cryptography.X509Certificates;
using BusinessLogic.Category_Components;
using BusinessLogic.Exceptions;

namespace TestProject1;

[TestClass]
public class CreditCardAccountTests
{

    #region Init

    private Account myCreditCard;
    private CreditCardAccount myCreditCardAccount;
    private User genericUser;
    private Category genericCategory;
    private Transaction genericTransaction;
    private CreditCardAccount myCreditAccount;
    private Transaction transactionUpdated;

    [TestInitialize]
    public void TestInitialized()
    {
        genericCategory = new Category("Food", StatusEnum.Enabled, TypeEnum.Outcome);
        genericTransaction = new Transaction("Payment of food", 200, DateTime.Now, CurrencyEnum.UY, TypeEnum.Outcome, genericCategory);
        myCreditAccount = new CreditCardAccount("Brou", CurrencyEnum.UY, DateTime.Now, "Brou", "1234", 1000, new DateTime(2024, 10, 05, 7, 0, 0));

        genericUser = new User("Michael", "Santa", "michSanta@gmail.com", "AustinF2003", "NW 2nd Ave");
        transactionUpdated = new Transaction("Payment of food", 300, DateTime.Now, CurrencyEnum.UY, TypeEnum.Outcome, genericCategory);

        myCreditCard = new CreditCardAccount();
        myCreditCardAccount = new CreditCardAccount();


        myCreditCardAccount.Name = "GenericName";
        myCreditCardAccount.IssuingBank = "GenericBank";
        myCreditCardAccount.Currency = CurrencyEnum.UY;
        myCreditCardAccount.Last4Digits = "5380";
    }
    #endregion

    #region Card Name
    [TestMethod]
    public void GivenCorrectAccountName_ShouldBeSetted()
    {
        string name = "Itau volar";
        myCreditCard.Name = name;
        Assert.AreEqual(name, myCreditCard.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateAccount))]
    public void GivenEmptyAccountName_ShouldThrowException()
    {
        string name = "";
        myCreditCard.Name = name;

        myCreditCard.ValidateAccount();
    }
    #endregion

    #region CreationDate
    [TestMethod]

    public void DateOfCreditCard_ShouldBeActualDate()
    {
        DateTime actualDate = DateTime.Now.Date;
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

        myCreditCardAccount.Last4Digits = "123A";
        myCreditCardAccount.ValidateCreditCardAccount();

    }
    #endregion 

    #region AvailableCredit

    [TestMethod]
    public void GivenCorrectAvailableCredit_ShouldBeSetted()
    {
        int availableCredit = 12000;
        myCreditCardAccount.AvailableCredit = availableCredit;

        Assert.AreEqual(availableCredit, myCreditCardAccount.AvailableCredit);
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateAccount))]
    public void GivenAvailableCreditLessThan0_ShouldReturnException()
    {

        myCreditCardAccount.AvailableCredit = -1;

        myCreditCardAccount.ValidateCreditCardAccount();
    }

    [TestMethod]
    public void GivenTransactionAndCreditAccount_ShouldReturnAmountOfAccountAfterModifyCorrect()
    {
        genericUser.AddCreditAccount(myCreditAccount);

        decimal oldAmount = myCreditAccount.AvailableCredit;

        genericUser.MyAccounts[0].MyTransactions = new List<Transaction>();
        genericUser.MyAccounts[0].AddTransaction(genericTransaction);

        myCreditAccount.UpdateAccountMoneyAfterAdd(genericTransaction);
        myCreditAccount.UpdateAccountAfterModify(transactionUpdated, genericTransaction.Amount);

        Assert.AreEqual(myCreditAccount.AvailableCredit, oldAmount - transactionUpdated.Amount);
    }


    [TestMethod]
    public void GivenTransactionToDelete_ShouldUpdateAmountCorrectly()
    {
        genericUser.AddCreditAccount(myCreditAccount);

        genericUser.MyAccounts[0].MyTransactions = new List<Transaction>();
        genericUser.MyAccounts[0].AddTransaction(genericTransaction);

        myCreditAccount.UpdateAccountMoneyAfterAdd(genericTransaction);
        myCreditAccount.UpdateAccountAfterDelete(genericTransaction);

        Assert.AreEqual(1000, myCreditAccount.AvailableCredit);
    }

    #endregion

    #region ClosingDate

    [TestMethod]
    public void CreatedAnAccount_ClosingDateMustBeSetted()
    {

        //We make it string so it can have an estandar format and is the same format that CreationDate.
        DateTime closingDate = new DateTime(2023, 11, 1);
        myCreditCardAccount.ClosingDate = closingDate;

        Assert.AreEqual(closingDate, myCreditCardAccount.ClosingDate);
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateAccount))]
    public void SelectedClosingDateThatIsBeforeCreationDate_ShouldThrowException()
    {

        myCreditCardAccount.ClosingDate = new DateTime(2023, 7, 1);

        myCreditCardAccount.ValidateCreditCardAccount();


    }

    #endregion

    #region Constructor
    [TestMethod]
    public void CreationOfCreditCardAccount_ShouldBeValidated()
    {

        string nameToBeSetted = "Prex";
        CurrencyEnum currencyToBeSetted = CurrencyEnum.UY;
        string issuingBankToBeSetted = "Santander";
        string last4DigitsToBeSetted = "1234";
        int availableCreditToBeSetted = 20000;
        DateTime closingDateToBeSetted = new DateTime(2024, 9, 30);

        CreditCardAccount CreditCardAccountExample = new CreditCardAccount(nameToBeSetted, currencyToBeSetted, DateTime.Now, issuingBankToBeSetted, last4DigitsToBeSetted, availableCreditToBeSetted, closingDateToBeSetted);

        Assert.AreEqual(nameToBeSetted, CreditCardAccountExample.Name);
        Assert.AreEqual(currencyToBeSetted, CreditCardAccountExample.Currency);
        Assert.AreEqual(issuingBankToBeSetted, CreditCardAccountExample.IssuingBank);
        Assert.AreEqual(last4DigitsToBeSetted, CreditCardAccountExample.Last4Digits);
        Assert.AreEqual(availableCreditToBeSetted, CreditCardAccountExample.AvailableCredit);
        Assert.AreEqual(closingDateToBeSetted, CreditCardAccountExample.ClosingDate);

    }


    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateAccount))]
    public void GivenIncorrectValueWhenCreatingACreditCard_ShouldThrowException()
    {
        string nameToBeSetted = "";
        CurrencyEnum currencyToBeSetted = CurrencyEnum.UY;
        string issuingBankToBeSetted = "Santander";
        string last4DigitsToBeSetted = "1234";
        int availableCreditToBeSetted = 20000;
        DateTime closingDateToBeSetted = new DateTime(2022, 9, 30);

        CreditCardAccount CreditCardAccountExample = new CreditCardAccount(nameToBeSetted, currencyToBeSetted, DateTime.Now, issuingBankToBeSetted, last4DigitsToBeSetted, availableCreditToBeSetted, closingDateToBeSetted);
    }


    #endregion

}