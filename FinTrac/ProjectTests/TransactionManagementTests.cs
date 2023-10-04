using BusinessLogic;
using NuGet.Frameworks;
using System.Runtime.ExceptionServices;
using BusinessLogic.Transaction_Components;
using BusinessLogic.Category_Components;
using BusinessLogic.Account_Components;
using BusinessLogic.User_Components;
using System.Security.Principal;
using System.Collections.Generic;

namespace TestProject1;
[TestClass]
public class TransactionManagementTests
{
    #region initializingAspects

    private User genericUser;
    private Category genericCategoryOutcome1;
    private Category genericCategoryOutcome2;
    private MonetaryAccount genericMonetaryAccount;
    private CreditCardAccount genericCreditAccount;
    private Transaction genericTransactionOutcome1;
    private Transaction genericTransactionOutcome2;

    [TestInitialize]
    public void TestInitialize()
    {
        genericUser = new User("Michael", "Santa", "michSanta@gmail.com", "AustinF2003", "NW 2nd Ave");

        genericCategoryOutcome1 = new Category("Clothes", StatusEnum.Enabled, TypeEnum.Outcome);

        genericCategoryOutcome2 = new Category("Food", StatusEnum.Enabled, TypeEnum.Outcome);

        genericUser.MyCategories.Add(genericCategoryOutcome1);
        genericUser.MyCategories.Add(genericCategoryOutcome2);

        genericMonetaryAccount = new MonetaryAccount("Saving Account", 1000, CurrencyEnum.UY);

        genericCreditAccount = new CreditCardAccount("Credit Account", CurrencyEnum.UY, "Brou", "1233", 1000, DateTime.Now);

        genericUser.AddAccount(genericMonetaryAccount);
        genericUser.AddAccount(genericCreditAccount);

        genericTransactionOutcome1 = new Transaction("Payment of Clothes", 200, CurrencyEnum.UY, TypeEnum.Outcome, genericMonetaryAccount, genericUser.MyCategories);

        genericTransactionOutcome2 = new Transaction("Payment of food", 400, CurrencyEnum.UY, TypeEnum.Outcome, genericCreditAccount, genericUser.MyCategories);

    }

    #endregion

    #region Add Transaction Tests

    [TestMethod]
    public void GivenTransactionToAddAccount_ShouldBeAdded()
    {
        genericUser.MyAccounts[0].AddTransaction(genericTransactionOutcome1);
        Transaction transactionAdded = genericUser.MyAccounts[0].MyTransactions[0];

        Assert.AreEqual(transactionAdded, genericTransactionOutcome1);
    }

    [TestMethod]
    public void GivenTransactionToAddToMonetaryAccount_ShouldModifyAccountAmountCorrectly()
    {
        decimal previousAccountAmount = genericMonetaryAccount.Ammount;
        decimal costOfTransaction = genericTransactionOutcome1.Amount;
        genericUser.MyAccounts[0].UpdateAccountMoney(genericTransactionOutcome1);
        genericUser.MyAccounts[0].AddTransaction(genericTransactionOutcome1);
        MonetaryAccount myMonetary = (MonetaryAccount)genericUser.MyAccounts[0];

        Assert.AreEqual(previousAccountAmount - costOfTransaction, myMonetary.Ammount);
    }

    [TestMethod]
    public void GivenTransactionToAddToCreditAccount_ShouldModifyAccountAmountCorrectly()
    {
        decimal previousAccountCredit = genericCreditAccount.AvailableCredit;
        decimal costOfTransaction = genericTransactionOutcome2.Amount;
        genericUser.MyAccounts[1].UpdateAccountMoney(genericTransactionOutcome2);
        genericUser.MyAccounts[1].AddTransaction(genericTransactionOutcome2);
        CreditCardAccount myCreditCard = (CreditCardAccount)genericUser.MyAccounts[1];

        Assert.AreEqual(previousAccountCredit - costOfTransaction, myCreditCard.AvailableCredit);
    }

    [TestMethod]
    public void GiventTransactionToAddToAccount_ShouldSetTransactionId()
    {
        int idShouldBe = 0;
        genericUser.MyAccounts[0].AddTransaction(genericTransactionOutcome1);
        int idSetted = genericUser.MyAccounts[0].MyTransactions[0].TransactionId;
        Assert.AreEqual(idShouldBe, idSetted);
    }

    #endregion

    #region Modify Transaction Tests

    [TestMethod]
    public void GivenTransactionToBeModified_ShouldBeModified()
    {
        genericUser.MyAccounts[1].UpdateAccountMoney(genericTransactionOutcome2);
        genericUser.MyAccounts[1].AddTransaction(genericTransactionOutcome2);

        List<Category> modifiedListOfCategories = new List<Category>();

        modifiedListOfCategories.Add(genericCategoryOutcome1);

        Transaction modifiedTransaction = new Transaction("Payment of food", 100, CurrencyEnum.USA, TypeEnum.Outcome, genericCreditAccount, genericUser.MyCategories);
        modifiedTransaction.TransactionId = 0;

        genericUser.MyAccounts[1].ModifyTransaction(modifiedTransaction);

        Assert.AreEqual(modifiedTransaction, genericUser.MyAccounts[1].MyTransactions[0]);



    }

    #endregion


}