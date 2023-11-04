using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Transaction_Components;
using BusinessLogic.Category_Components;
using BusinessLogic.User_Components;
using BusinessLogic.Account_Components;
using BusinessLogic.ExchangeHistory_Components;
using BusinessLogic.Goal_Components;
using BusinessLogic.Enums;
using BusinessLogic.Exceptions;

namespace BusinessLogic.Report_Components
{
    public abstract class Report
    {

        #region Monthly report

        public static List<ResumeOfGoalReport> MonthlyReportPerGoal(User loggedUser)
        {
            decimal[] spendingsPerCategory = CategorySpendings(loggedUser, (MonthsEnum)DateTime.Now.Month, loggedUser.MyAccounts);
            decimal totalSpent = 0;
            List<ResumeOfGoalReport> listOfSpendingsResumes = new List<ResumeOfGoalReport>();
            bool goalAchieved = false;

            foreach (var myGoal in loggedUser.MyGoals)
            {
                totalSpent = 0;
                goalAchieved = true;
                foreach (var category in myGoal.CategoriesOfGoal)
                {
                    totalSpent += spendingsPerCategory[category.CategoryId];
                }
                if (totalSpent > myGoal.MaxAmountToSpend) { goalAchieved = false; }
                ResumeOfGoalReport myResume = new ResumeOfGoalReport(myGoal.MaxAmountToSpend, totalSpent, goalAchieved);
                listOfSpendingsResumes.Add(myResume);
            }
            return listOfSpendingsResumes;
        }
        #endregion

        #region Report of all spendings per category detailed

        public static List<ResumeOfSpendigsReport> GiveAllSpendingsPerCategoryDetailed(User loggedUser, MonthsEnum monthGiven)
        {
            decimal[] spendingsPerCategory = CategorySpendings(loggedUser, (MonthsEnum)monthGiven, loggedUser.MyAccounts);
            decimal totalSpentPerCategory = 0;
            decimal percentajeOfTotal = 0;
            Category categoryRelatedToSpending = new Category();
            List<ResumeOfSpendigsReport> listOfSpendingsResumes = new List<ResumeOfSpendigsReport>();

            foreach (var category in loggedUser.MyCategories.Where(t => t != null))
            {
                totalSpentPerCategory = spendingsPerCategory[category.CategoryId];
                percentajeOfTotal = CalulatePercent(spendingsPerCategory, totalSpentPerCategory);
                categoryRelatedToSpending = category;

                ResumeOfSpendigsReport myCategorySpendingsResume = new ResumeOfSpendigsReport(category, totalSpentPerCategory, percentajeOfTotal);

                listOfSpendingsResumes.Add(myCategorySpendingsResume);
            }
            return listOfSpendingsResumes;
        }

        private static decimal CalulatePercent(decimal[] spendingsPerCategory, decimal totalSpentPerCategory)
        {
            if(spendingsPerCategory[spendingsPerCategory.Length - 1] == 0)
            {
                return 0;
            }
            return (totalSpentPerCategory / spendingsPerCategory[spendingsPerCategory.Length - 1]) * 100;
        }

        #endregion

        #region Report of All Outcome Transactions
        public static List<Transaction> GiveAllOutcomeTransactions(User loggedUser)
        {
            List<Transaction> listOfAllOutcomeTransactions = new List<Transaction>();
            foreach (var account in loggedUser.MyAccounts)
            {
                foreach (var transaction in account.MyTransactions)
                {
                    AddToListOfOutcomest(listOfAllOutcomeTransactions, transaction);
                }
            }
            return listOfAllOutcomeTransactions;
        }

        private static void AddToListOfOutcomest(List<Transaction> listOfAllOutcomeTransactions, Transaction transaction)
        {
            if (transaction.Type == TypeEnum.Outcome)
            {
                listOfAllOutcomeTransactions.Add(transaction);
            }
        }

        #endregion

        #region Report Of Spendings For Credit Card

        public static List<Transaction> ReportOfSpendingsPerCard(CreditCardAccount creditCard)
        {
            DateTime dateTimInit = GetDateTimInit(creditCard);
            List<Transaction> listOfAllOutcomeTransactions = new List<Transaction>();
            foreach (var transaction in creditCard.MyTransactions.Where(t => t != null))
            {
                if (transaction.Type == TypeEnum.Outcome)

                    if (IsBetweenBalanceDates(creditCard, dateTimInit, transaction))
                    {
                        listOfAllOutcomeTransactions.Add(transaction);
                    }
            }
            return listOfAllOutcomeTransactions;
        }

        private static DateTime GetDateTimInit(CreditCardAccount creditCard)
        {
            return new DateTime(creditCard.ClosingDate.Year, creditCard.ClosingDate.Month - 1, creditCard.CreationDate.Day + 1);
        }

        private static bool IsBetweenBalanceDates(CreditCardAccount creditCard, DateTime dateTimInit, Transaction transaction)
        {
            return transaction.CreationDate.CompareTo(dateTimInit) >= 0 && transaction.CreationDate.CompareTo(creditCard.ClosingDate) <= 0;
        }

        #endregion

        #region  Filtering Lists of spendings
        public static List<Transaction> FilterListOfSpendingsPerRangeOfDate(List<Transaction> listOfSpendings, RangeOfDates rangeOfDates)
        {
            List<Transaction> filteredListOfSpending = listOfSpendings;
            
            if (rangeOfDates.InitialDate <= rangeOfDates.FinalDate)
            {
                filteredListOfSpending = filteredListOfSpending.Where(x =>
                    x.CreationDate >= rangeOfDates.InitialDate && x.CreationDate <= rangeOfDates.FinalDate).ToList();
            }
            else
            {
                throw new ExceptionReport("Error: Initial date is bigger than final date");
            }
            
            return filteredListOfSpending;
        }
        
        public static List<Transaction> FilterListOfSpendingsByNameOfCategory(List<Transaction> listOfSpendings, string nameOfCategory)
        {
            List<Transaction> filteredListOfSpending = listOfSpendings;
            
            if (!String.IsNullOrEmpty(nameOfCategory))
            {
                filteredListOfSpending = filteredListOfSpending.Where(x => x.TransactionCategory.Name.StartsWith(nameOfCategory, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return filteredListOfSpending;
        }

        public static List<Transaction> FilterListOfSpendingsByAccount(List<Transaction> listOfSpendings,
            Account accountSelected, User userLogged)
        {
            List<Transaction> accountSpendings = AccountSpendings(accountSelected, userLogged);
            List<Transaction> filteredListOfSpending = listOfSpendings;
            filteredListOfSpending = filteredListOfSpending.Intersect(accountSpendings).ToList();
            
            return filteredListOfSpending;
        }

        private static List<Transaction> AccountSpendings(Account accountSelected, User userLogged)
        {
            List<Transaction> accountSpendings = userLogged.MyAccounts[accountSelected.AccountId].MyTransactions
                .Where(x => x.TransactionCategory.Type == TypeEnum.Outcome)
                .ToList();
            return accountSpendings;
        }

        #endregion

        #region Report Of Balance For Monetary Account

        public static decimal GiveAccountBalance(MonetaryAccount account)
        {
            decimal initialMoney = account.ReturnInitialAmount();
            decimal actualBalance = 0;
            decimal accountBalance = 0;
            actualBalance = SummationOfTransactions(account, actualBalance);
            accountBalance = CalculateBalance(initialMoney, actualBalance);
            return accountBalance;
        }

        private static decimal CalculateBalance(decimal initialMoney, decimal actualBalance)
        {
            decimal accountBalance;
            if (actualBalance < 0)
            {
                accountBalance = initialMoney + actualBalance;
            }
            else
            {
                accountBalance = initialMoney - actualBalance;
            }

            return accountBalance;
        }

        private static decimal SummationOfTransactions(MonetaryAccount account, decimal actualBalance)
        {
            foreach (var transaction in account.GetAllTransactions())
            {
                if (DateTime.Compare(transaction.CreationDate, DateTime.Now.Date) <= 0)
                {
                    if (transaction.Type == TypeEnum.Income)

                    {
                        actualBalance += transaction.Amount;
                    }
                    else
                    {
                        actualBalance -= transaction.Amount;
                    }
                }
            }

            return actualBalance;
        }

        #endregion

        #region Methods used by reports
        public static decimal ConvertDollar(Transaction myTransaction, User loggedUser)
        {
            decimal amountToReturn = myTransaction.Amount;
            if (myTransaction.Currency == CurrencyEnum.USA)
            {
                decimal dollarValue = 0;
                foreach (ExchangeHistory exchange in loggedUser.MyExchangesHistory)
                {
                    if (exchange.ValueDate == myTransaction.CreationDate)
                    {
                        dollarValue = exchange.Value;
                        break;
                    }
                }
                amountToReturn = myTransaction.Amount * dollarValue;
            }
            return amountToReturn;
        }

        public static decimal[] CategorySpendings(User loggedUser, MonthsEnum monthSelected, List<Account> listOfAccounts)
        {
            decimal[] spendings = new decimal[loggedUser.MyCategories.Count + 2];

            foreach (var account in listOfAccounts.Where(t => t != null))
            {
                foreach (var transaction in account.MyTransactions.Where(t => t != null))
                {
                    if ((MonthsEnum)transaction.CreationDate.Month == monthSelected
                        && transaction.TransactionCategory.Type == TypeEnum.Outcome)
                    {
                        decimal amountToAdd = ConvertDollar(transaction, loggedUser);
                        LoadArray(spendings, transaction, amountToAdd);
                    }
                }
            }
            return spendings;
        }
        private static void LoadArray(decimal[] arrayToLoad, Transaction transaction, decimal amountToAdd)
        {
            LoadPerCategory(arrayToLoad, transaction, amountToAdd);
            LoadTotalsInArray(arrayToLoad, transaction, amountToAdd);

        }

        private static void LoadTotalsInArray(decimal[] arrayToLoad, Transaction transaction, decimal amountToAdd)
        {
            if (transaction.Type == TypeEnum.Income)
            {
                arrayToLoad[arrayToLoad.Length - 2] += amountToAdd;
            }
            else
            {
                arrayToLoad[arrayToLoad.Length - 1] += amountToAdd;
            }
        }

        private static void LoadPerCategory(decimal[] arrayToLoad, Transaction transaction, decimal amountToAdd)
        {
            arrayToLoad[transaction.TransactionCategory.CategoryId] += amountToAdd;
        }
        #endregion
    }

    #region Class for reports

    public class ResumeOfGoalReport
    {
        public decimal AmountDefined { get; set; }
        public decimal TotalSpent { get; set; }
        public bool GoalAchieved { get; set; }

        public ResumeOfGoalReport(decimal amountDefined, decimal totalSpent, bool goalAchieved)
        {
            AmountDefined = amountDefined;
            TotalSpent = totalSpent;
            GoalAchieved = goalAchieved;
        }
    }

    public class ResumeOfSpendigsReport
    {
        public Category CategoryRelated { get; set; }
        public decimal TotalSpentInCategory { get; set; }
        public decimal PercentajeOfTotal { get; set; }

        public ResumeOfSpendigsReport(Category categoryRelated, decimal totalSpent, decimal percentajeOfTotal)
        {
            CategoryRelated = categoryRelated;
            TotalSpentInCategory = totalSpent;
            PercentajeOfTotal = percentajeOfTotal;
        }

    }

    public class RangeOfDates
    {
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }

        public RangeOfDates(DateTime initialDate, DateTime finalDate)
        {
            InitialDate = initialDate;
            FinalDate = finalDate;
        }

    }

    #endregion
}







