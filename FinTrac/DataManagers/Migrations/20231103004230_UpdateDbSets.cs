using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataManagers.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDbSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_UserId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Goals_GoalId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_UserId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditCardAccounts_Accounts_AccountId",
                table: "CreditCardAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeHistories_Users_UserId",
                table: "ExchangeHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Users_UserId",
                table: "Goals");

            migrationBuilder.DropForeignKey(
                name: "FK_MonetaryAccounts_Accounts_AccountId",
                table: "MonetaryAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Categories_TransactionCategoryCategoryId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Goals",
                table: "Goals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExchangeHistories",
                table: "ExchangeHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "Transaction");

            migrationBuilder.RenameTable(
                name: "Goals",
                newName: "Goal");

            migrationBuilder.RenameTable(
                name: "ExchangeHistories",
                newName: "ExchangeHistory");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "Account");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_TransactionCategoryCategoryId",
                table: "Transaction",
                newName: "IX_Transaction_TransactionCategoryCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_AccountId",
                table: "Transaction",
                newName: "IX_Transaction_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Goals_UserId",
                table: "Goal",
                newName: "IX_Goal_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeHistories_UserId",
                table: "ExchangeHistory",
                newName: "IX_ExchangeHistory_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_UserId",
                table: "Category",
                newName: "IX_Category_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_GoalId",
                table: "Category",
                newName: "IX_Category_GoalId");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_UserId",
                table: "Account",
                newName: "IX_Account_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction",
                column: "TransactionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Goal",
                table: "Goal",
                column: "GoalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExchangeHistory",
                table: "ExchangeHistory",
                column: "ExchangeHistoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                table: "Account",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Users_UserId",
                table: "Account",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Goal_GoalId",
                table: "Category",
                column: "GoalId",
                principalTable: "Goal",
                principalColumn: "GoalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Users_UserId",
                table: "Category",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCardAccounts_Account_AccountId",
                table: "CreditCardAccounts",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeHistory_Users_UserId",
                table: "ExchangeHistory",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Goal_Users_UserId",
                table: "Goal",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MonetaryAccounts_Account_AccountId",
                table: "MonetaryAccounts",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Account_AccountId",
                table: "Transaction",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Category_TransactionCategoryCategoryId",
                table: "Transaction",
                column: "TransactionCategoryCategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Users_UserId",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_Goal_GoalId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_Users_UserId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditCardAccounts_Account_AccountId",
                table: "CreditCardAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeHistory_Users_UserId",
                table: "ExchangeHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Goal_Users_UserId",
                table: "Goal");

            migrationBuilder.DropForeignKey(
                name: "FK_MonetaryAccounts_Account_AccountId",
                table: "MonetaryAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Account_AccountId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Category_TransactionCategoryCategoryId",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Goal",
                table: "Goal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExchangeHistory",
                table: "ExchangeHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                table: "Account");

            migrationBuilder.RenameTable(
                name: "Transaction",
                newName: "Transactions");

            migrationBuilder.RenameTable(
                name: "Goal",
                newName: "Goals");

            migrationBuilder.RenameTable(
                name: "ExchangeHistory",
                newName: "ExchangeHistories");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "Account",
                newName: "Accounts");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_TransactionCategoryCategoryId",
                table: "Transactions",
                newName: "IX_Transactions_TransactionCategoryCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_AccountId",
                table: "Transactions",
                newName: "IX_Transactions_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Goal_UserId",
                table: "Goals",
                newName: "IX_Goals_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeHistory_UserId",
                table: "ExchangeHistories",
                newName: "IX_ExchangeHistories_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_UserId",
                table: "Categories",
                newName: "IX_Categories_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_GoalId",
                table: "Categories",
                newName: "IX_Categories_GoalId");

            migrationBuilder.RenameIndex(
                name: "IX_Account_UserId",
                table: "Accounts",
                newName: "IX_Accounts_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "TransactionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Goals",
                table: "Goals",
                column: "GoalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExchangeHistories",
                table: "ExchangeHistories",
                column: "ExchangeHistoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_UserId",
                table: "Accounts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Goals_GoalId",
                table: "Categories",
                column: "GoalId",
                principalTable: "Goals",
                principalColumn: "GoalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_UserId",
                table: "Categories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCardAccounts_Accounts_AccountId",
                table: "CreditCardAccounts",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeHistories_Users_UserId",
                table: "ExchangeHistories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Users_UserId",
                table: "Goals",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MonetaryAccounts_Accounts_AccountId",
                table: "MonetaryAccounts",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Categories_TransactionCategoryCategoryId",
                table: "Transactions",
                column: "TransactionCategoryCategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
