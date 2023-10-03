using BusinessLogic.Account_Components;
using BusinessLogic.User_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicTests
{
    [TestClass]
    public class AccountManagementTests
    {

        #region initializingAspects
        private Account genericMonetaryAccount;
        private User genericUser;
        private Account genericCreditCardAccount;
        private int numberOfAccountsAddedBefore;

       [TestInitialize]
        public void TestInitialize()
        {
            genericMonetaryAccount = new MonetaryAccount("Itau Saving Bank", 5000, CurrencyEnum.UY);

            string firstName = "Austin";
            string lastName = "Ford";
            string email = "austinFord@gmail.com";
            string password = "AustinF2003";
            string address = "NW 2nd Ave";
            genericUser = new User(firstName, lastName, email, password, address);

            
            string nameToBeSetted = "Prex";
            CurrencyEnum currencyToBeSetted = CurrencyEnum.UY;
            string issuingBankToBeSetted = "Santander";
            string last4DigitsToBeSetted = "1234";
            int availableCreditToBeSetted = 20000;
            DateTime closingDateToBeSetted = new DateTime(2024, 9, 30);
            genericCreditCardAccount = new CreditCardAccount(nameToBeSetted, currencyToBeSetted, issuingBankToBeSetted, last4DigitsToBeSetted, availableCreditToBeSetted, closingDateToBeSetted);

            numberOfAccountsAddedBefore = genericUser.MyAccounts.Count;
        }

        #endregion


        #region Add Monetary Account

        [TestMethod]
        public void GivenCorrectMonetaryAccountToAdd_ShouldAddIt()
        {
            genericUser.AddAccount(genericMonetaryAccount);
            
            Assert.AreEqual(numberOfAccountsAddedBefore + 1, genericUser.MyAccounts.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionAccountManagement))]

        public void GivenNameOfMonetaryAccountAlreadyAdded_ShouldThrowException()
        {
            genericUser.AddAccount(genericMonetaryAccount);

            Account repitedNameAccount = new MonetaryAccount("Itau Saving Bank", 100, CurrencyEnum.UY);
            genericUser.AddAccount(repitedNameAccount);
        }

        #endregion

        #region Add Creadit card Account

        [TestMethod]
        public void GivenCorrectCreditCardAccount_ShouldBeAdded()
        {
            genericUser.AddAccount(genericCreditCardAccount);

            Assert.AreEqual(numberOfAccountsAddedBefore + 1, genericUser.MyAccounts.Count);
        }

        [TestMethod]
        [ExpectedException (typeof(ExceptionAccountManagement))]

        public void GivenNameOfCreditCardAccountAlreadyAdded_ShouldThrowException()
        {
            genericUser.AddAccount(genericCreditCardAccount);

            int availableCreditNewCard = 1000;
            string equalName = "Prex";
            string issuingBankNewCard = "Pimball";
            string last4DigitsNewCard = "1234";
            CurrencyEnum currencyNewCard = CurrencyEnum.UY;
            DateTime closingDateNewCard = new DateTime(2028, 2, 10);

            Account accountWithEqualName = new CreditCardAccount(equalName, currencyNewCard, issuingBankNewCard, 
                last4DigitsNewCard, availableCreditNewCard,closingDateNewCard);

            genericUser.AddAccount(accountWithEqualName);
        }
        #endregion

        #region Asignation of Id

        [TestMethod]
        public void GivenCategoryToAdd_ShouldAssignId()
        {
            genericUser.AddAccount(genericMonetaryAccount);
            Assert.AreEqual(genericUser.MyAccounts.Count, genericMonetaryAccount.AccountId);

            genericUser.AddAccount(genericCreditCardAccount);
            Assert.AreEqual(genericUser.MyAccounts.Count, genericCreditCardAccount.AccountId);
        }

        #endregion

        #region Return Accounts

        [TestMethod]
        public void ShouldBePossibleToReturnList()
        {
            Assert.AreEqual(genericUser.MyAccounts, genericUser.GetAccounts());
        }
        #endregion

        #region Modify Aspects of Account

        [TestMethod]
        public void GivenCategoryToUpdate_ShouldBeModifiedCorrectly()
        {
            Account accountToUpdate = new MonetaryAccount("Itau Saving Bank", 10000, CurrencyEnum.USA);

            genericUser.ModifyAccount(accountToUpdate);
        }


        #endregion
    }
}
