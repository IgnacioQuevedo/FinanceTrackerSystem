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
        private Account genericAccount;
        private User genericUser;

        [TestInitialize]
        public void TestInitialize()
        {
            genericAccount = new MonetaryAccount("Itau Saving Bank", 5000, CurrencyEnum.UY);

            string firstName = "Austin";
            string lastName = "Ford";
            string email = "austinFord@gmail.com";
            string password = "AustinF2003";
            string address = "NW 2nd Ave";
            genericUser = new User(firstName, lastName, email, password, address);
        }

        #endregion


        #region Add Account

        [TestMethod]
        public void GivenCorrectAccountToAdd_ShouldAddIt()
        {
            int numberOfAccountsAddedBefore = genericUser.MyAccounts.Count;
            genericUser.addAccount(genericAccount);
            
            Assert.AreEqual(numberOfAccountsAddedBefore + 1, genericUser.MyAccounts.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionAccountManagement))]

        public void GivenNameOfAccountAlreadyAdded_ShouldThrowException()
        {
            genericUser.addAccount(genericAccount);

            Account repitedNameAccount = new MonetaryAccount("Itau Saving Bank", 100, CurrencyEnum.UY);
            genericUser.addAccount(repitedNameAccount);
        }


        #endregion

    }
}
