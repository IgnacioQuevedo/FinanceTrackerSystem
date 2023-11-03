using BusinessLogic.Account_Components;
using BusinessLogic.Category_Components;
using BusinessLogic.User_Components;
using BusinessLogic.Transaction_Components;
using BusinessLogic.Enums;

namespace DataManagers
{
    public class Repository
    {
        public List<User> Users { get; set; }

        public Repository()
        {
            Users = new List<User>();
            DefaultUser();
        }



        private void DefaultUser()
        {
            User userTest = new User("Ignacio", "Quevedo", "nachitoquevedo@gmail.com", "Nacho200304!", "");
            userTest.UserId = 0;
            Users.Add(userTest);
            Category usertCategory = new Category("Food", StatusEnum.Enabled, TypeEnum.Outcome);
            Category usertCategory2 = new Category("Party", StatusEnum.Enabled, TypeEnum.Outcome);
            userTest.AddCategory(usertCategory);
            userTest.AddCategory(usertCategory2);
            CreditCardAccount myCredit = new CreditCardAccount("Brou Credits", CurrencyEnum.UY, new DateTime(2023, 9, 12, 0, 0, 0), "Brou", "1111", 1000, DateTime.Now);
            userTest.AddCreditAccount(myCredit);
            Transaction transaction = new Transaction("Spent on food", 300, DateTime.Now.Date, CurrencyEnum.UY, TypeEnum.Outcome, usertCategory);
            Transaction transaction2 = new Transaction("Spent on party", 400, DateTime.Now.Date, CurrencyEnum.UY, TypeEnum.Outcome, usertCategory2);
            myCredit.AddTransaction(transaction);
            myCredit.AddTransaction(transaction2);


        }


    }
}
