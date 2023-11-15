using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Transaction_Components;
using BusinessLogic.Category_Components;
using BusinessLogic.User_Components;
using BusinessLogic.Account_Components;
using BusinessLogic.Dtos_Components;
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
            decimal[] spendingsPerCategory =
                CategorySpendings(loggedUser, (MonthsEnum)DateTime.Now.Month, loggedUser.MyAccounts);
            decimal totalSpent = 0;
            List<ResumeOfGoalReport> listOfSpendingsResumes = new List<ResumeOfGoalReport>();
            bool goalAchieved = false;

            foreach (var myGoal in loggedUser.MyGoals)
            {
                totalSpent = 0;
                goalAchieved = true;
                foreach (var category in myGoal.CategoriesOfGoal)
                {
                    totalSpent += spendingsPerCategory[category.CategoryId - 1];
                }

                if (totalSpent > myGoal.MaxAmountToSpend)
                {
                    goalAchieved = false;
                }

                ResumeOfGoalReport myResume = new ResumeOfGoalReport(myGoal.MaxAmountToSpend, totalSpent, goalAchieved);
                listOfSpendingsResumes.Add(myResume);
            }

            return listOfSpendingsResumes;
        }

        #endregion

        #region Report of all spendings per category detailed

        public static List<ResumeOfCategoryReport> GiveAllSpendingsPerCategoryDetailed(User loggedUser,
            MonthsEnum monthGiven)
        {
            decimal[] spendingsPerCategory =
                CategorySpendings(loggedUser, (MonthsEnum)monthGiven, loggedUser.MyAccounts);
            decimal totalSpentPerCategory = 0;
            decimal percentajeOfTotal = 0;
            Category categoryRelatedToSpending = new Category();
            List<ResumeOfCategoryReport> listOfSpendingsResumes = new List<ResumeOfCategoryReport>();

            foreach (var category in loggedUser.MyCategories)
            {
                totalSpentPerCategory = spendingsPerCategory[category.CategoryId - 1];
                percentajeOfTotal = CalulatePercent(spendingsPerCategory, totalSpentPerCategory);
                categoryRelatedToSpending = category;

                ResumeOfCategoryReport myCategorySpendingsResume =
                    new ResumeOfCategoryReport(category, totalSpentPerCategory, percentajeOfTotal);

                listOfSpendingsResumes.Add(myCategorySpendingsResume);
            }

            return listOfSpendingsResumes;
        }

        private static decimal CalulatePercent(decimal[] spendingsPerCategory, decimal totalSpentPerCategory)
        {
            if (spendingsPerCategory[spendingsPerCategory.Length - 1] == 0)
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

        private static void AddToListOfOutcomest(List<Transaction> listOfAllOutcomeTransactions,
            Transaction transaction)
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
            foreach (var transaction in creditCard.MyTransactions)
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
            DateTime dateToSet = DateTime.MinValue;
            if (creditCard.ClosingDate.Month == 1)
            {
                dateToSet = new DateTime(creditCard.ClosingDate.Year - 1, 12,
                 creditCard.ClosingDate.Day + 1);
            }
            else
            {
                dateToSet = new DateTime(creditCard.ClosingDate.Year, creditCard.ClosingDate.Month - 1,
                creditCard.ClosingDate.Day + 1);
            }

            return dateToSet;
        }

        private static bool IsBetweenBalanceDates(CreditCardAccount creditCard, DateTime dateTimInit,
            Transaction transaction)
        {
            return transaction.CreationDate.CompareTo(dateTimInit) >= 0 &&
                   transaction.CreationDate.CompareTo(creditCard.ClosingDate) <= 0;
        }

        #endregion

        #region Filtering Lists of spendings

        public static List<Transaction> FilterListByRangeOfDate(List<Transaction> listOfSpendings,
            RangeOfDates rangeOfDates)
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

        public static List<Transaction> FilterListByNameOfCategory(List<Transaction> listOfSpendings,
            string nameOfCategory)
        {
            List<Transaction> filteredListOfSpending = listOfSpendings;

            if (!String.IsNullOrEmpty(nameOfCategory))
            {
                filteredListOfSpending = filteredListOfSpending.Where(x =>
                    x.TransactionCategory.Name.StartsWith(nameOfCategory, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return filteredListOfSpending;
        }

        public static List<Transaction> FilterListByAccountAndOutcome(Account accountSelected)
        {
            List<Transaction> accountSpendings = accountSelected.GetAllTransactions()
                .Where(x => x.TransactionCategory.Type == TypeEnum.Outcome && x.AccountId == accountSelected.AccountId)
                .ToList();

            return accountSpendings;
        }

        #endregion

        #region Report Of Balance For Monetary Account

        public static decimal GiveAccountBalance(MonetaryAccount account, decimal initialMoney)
        {
            decimal actualBalance = 0;
            decimal accountBalance = 0;
            actualBalance = SummationOfTransactions(account, actualBalance);
            accountBalance = CalculateBalance(initialMoney, actualBalance);
            return accountBalance;
        }

        private static decimal CalculateBalance(decimal initialMoney, decimal actualBalance)
        {
            decimal accountBalance;
            accountBalance = initialMoney + actualBalance;
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


        #region Report Of Movements In X Days

        public static MovementInXDays GetMovementInXDays(List<Account> accounts, RangeOfDates rangeOfDates)
        {
            MovementInXDays movements = new MovementInXDays(rangeOfDates);
            int month = rangeOfDates.InitialDate.Month;

            foreach (var account in accounts)
            {
                foreach (var transaction in account.MyTransactions)
                {
                    if (transaction.CreationDate.Month == month &&
                        transaction.CreationDate.Day >= rangeOfDates.InitialDate.Day &&
                        transaction.CreationDate.Day <= rangeOfDates.FinalDate.Day)
                    {
                        if (transaction.Type == TypeEnum.Income)
                        {
                            movements.Incomes[transaction.CreationDate.Day - 1] += transaction.Amount;
                        }
                        else
                        {
                            movements.Spendings[transaction.CreationDate.Day - 1] += transaction.Amount;
                        }
                    }
                }
            }

            return movements;
        }

        #endregion

        #region Methods used by reports

        public static decimal ConvertDollarOrEuro(Transaction myTransaction, User loggedUser)
        {
            decimal amountToReturn = myTransaction.Amount;
            bool found = false;
            if (myTransaction.Currency == CurrencyEnum.USA || myTransaction.Currency == CurrencyEnum.EUR)
            {
                decimal exchangeValue = 0;
                foreach (ExchangeHistory exchange in loggedUser.MyExchangesHistory)
                {
                    if (exchange.ValueDate == myTransaction.CreationDate && exchange.Currency == myTransaction.Currency)
                    {
                        exchangeValue = exchange.Value;
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    amountToReturn = myTransaction.Amount * exchangeValue;
                }
                else
                {
                    throw new ExceptionReport("There is no exchange registered for transaction");
                }

            }

            return amountToReturn;
        }

        public static decimal[] CategorySpendings(User loggedUser, MonthsEnum monthSelected,
            List<Account> listOfAccounts)
        {
            decimal[] spendings = new decimal[loggedUser.MyCategories.Count + 2];

            foreach (var account in listOfAccounts)
            {
                foreach (var transaction in account.MyTransactions)
                {
                    if ((MonthsEnum)transaction.CreationDate.Month == monthSelected
                        && DateTime.Now.Year == transaction.CreationDate.Year && transaction.TransactionCategory.Type == TypeEnum.Outcome)
                    {
                        decimal amountToAdd = ConvertDollarOrEuro(transaction, loggedUser);
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
            arrayToLoad[transaction.TransactionCategory.CategoryId - 1] += amountToAdd;
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

    public class ResumeOfCategoryReport
    {
        public Category CategoryRelated { get; set; }
        public decimal TotalSpentInCategory { get; set; }
        public decimal PercentajeOfTotal { get; set; }

        public ResumeOfCategoryReport(Category categoryRelated, decimal totalSpent, decimal percentajeOfTotal)
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

    public class MovementInXDays
    {
        private int _amountOfDays;
        public decimal[] Spendings { get; set; }
        public decimal[] Incomes { get; set; }

        public RangeOfDates RangeOfDates { get; set; }

        public MovementInXDays()
        {
        }

        public MovementInXDays(RangeOfDates rangeOfDates)
        {
            _amountOfDays = rangeOfDates.FinalDate.Day - rangeOfDates.InitialDate.Day + 1;
            ValidateDates(rangeOfDates.FinalDate, rangeOfDates.InitialDate);

            Incomes = new decimal[31];
            Spendings = new decimal[31];
            RangeOfDates = rangeOfDates;
        }

        private void ValidateDates(DateTime finalDate, DateTime initialDate)
        {
            bool monthsAreEqual = finalDate.Month == initialDate.Month;
            bool yearsAreEqual = finalDate.Year == initialDate.Year;

            if (_amountOfDays < 0 || !monthsAreEqual || !yearsAreEqual)
            {
                throw new ExceptionReport("Seems that there is an error on the DATES, validate that FINAL DATE " +
                                          "is greater than the INITIAL one. Also check that there are in the SAME YEAR " +
                                          "and references the SELECTED MONTH from below this alert.");
            }
        }
    }

    #endregion
}