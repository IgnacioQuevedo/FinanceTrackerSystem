using BusinessLogic.Dtos_Components;

namespace Controller.IControllers
{
    public interface IReportController
    {
        public List<ResumeOfGoalReportDTO> GiveMonthlyReportPerGoal(UserDTO userLoggedDTO);
        public List<TransactionDTO> GiveAllOutcomeTransactions(UserDTO userConnectedDTO);
        public List<ResumeOfCategoryReportDTO> GiveAllSpendingsPerCategoryDetailed(UserDTO userLoggedDTO, MonthsEnumDTO monthGiven);
        public decimal GiveAccountBalance(MonetaryAccountDTO account);
        public List<TransactionDTO> ReportOfSpendingsPerCard(CreditCardAccountDTO creditCard);
        public List<CategoryDTO> GetAllCategories(int userConnectedId);
        public List<TransactionDTO> FilterListByRangeOfDate(List<TransactionDTO> listOfSpendingsDTO, RangeOfDatesDTO rangeOfDates);
        public List<TransactionDTO> FilterListByNameOfCategory(List<TransactionDTO> listOfSpendingsDTO,
            string nameOfCategory);
        public List<TransactionDTO> FilterByAccountAndTypeOutcome(AccountDTO accountSelected);


    }
}