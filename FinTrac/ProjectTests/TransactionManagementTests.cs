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
    private Transaction genericTransactionOutcome;
    private Transaction genericTransactionIncome;

    [TestInitialize]
    public void TestInitialize()
    {
        genericUser = new User("Michael", "Santa", "michSanta@gmail.com", "AustinF2003", "NW 2nd Ave");

        genericCategoryOutcome1 = new Category("Clothes", StatusEnum.Enabled, TypeEnum.Outcome);

        genericCategoryOutcome2 = new Category("Food", StatusEnum.Enabled, TypeEnum.Outcome);

        genericUser.MyCategories.Add(genericCategoryOutcome1);
        genericUser.MyCategories.Add(genericCategoryOutcome2);

        genericMonetaryAccount = new MonetaryAccount("Saving Account", 1000, CurrencyEnum.UY);

        genericUser.AddAccount(genericMonetaryAccount);

        genericTransactionOutcome = new Transaction("Payment of Clothes", 200, CurrencyEnum.UY, TypeEnum.Outcome, genericMonetaryAccount, genericUser.MyCategories);

        genericTransactionIncome = new Transaction("Payment of food", 400, CurrencyEnum.UY, TypeEnum.Outcome, genericMonetaryAccount, genericUser.MyCategories);

    }

    #endregion

    #region Add Transaction Tests

    [TestMethod]
    public void GivenTransactionToAddToMonetaryAccount_ShouldBeAdded()
    {
        genericUser.MyAccounts[0].AddTransaction(genericTransactionOutcome);
        Transaction transactionAdded = genericUser.MyAccounts[0].MyTransactions[0];

        Assert.AreEqual(transactionAdded, genericTransactionOutcome);
    }

    #endregion


}