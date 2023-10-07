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

namespace BusinessLogic.Report_Components
{
    public abstract class Report
    {

        public static decimal ConvertDolar(Transaction myTransaction, User loggedUser)
        {
            decimal amountToReturn = myTransaction.Amount;
            if (myTransaction.Currency == CurrencyEnum.USA)
            {
                bool found = false;
                decimal dolarValue = 0;
                DateTime bestDate = DateTime.MinValue;
                foreach (ExchangeHistory exchange in loggedUser.MyExchangesHistory)
                {
                    if (!found && exchange.ValueDate > bestDate && exchange.ValueDate <= myTransaction.CreationDate)
                    {
                        bestDate = exchange.ValueDate;
                        dolarValue = exchange.Value;
                        if (exchange.ValueDate == myTransaction.CreationDate) { found = true; }
                    }
                }
                amountToReturn = myTransaction.Amount * dolarValue;
            }
            return amountToReturn;
        }

        public static decimal[] CategorySpendings(User loggedUser, MonthsEnum monthSelected, List<Account> listOfAccounts)
        {
            decimal[] spendings = new decimal[loggedUser.MyCategories.Count + 2];

            foreach (var account in listOfAccounts)
            {
                foreach (var transaction in account.MyTransactions)
                {
                    if ((MonthsEnum)transaction.CreationDate.Month == monthSelected
                        && transaction.TransactionCategory.Type == TypeEnum.Outcome)
                    {
                        decimal amountToAdd = ConvertDolar(transaction, loggedUser);
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
                    totalSpent = spendingsPerCategory[category.CategoryId] + totalSpent;

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
    }

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
}



