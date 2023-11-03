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
                name: "FK_Account_Users_UserId",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_Goal_GoalId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_Users_UserId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeHistory_Users_UserId",
                table: "ExchangeHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Goal_Users_UserId",
                table: "Goal");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Account_AccountId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_AccountId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Goal_UserId",
                table: "Goal");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeHistory_UserId",
                table: "ExchangeHistory");

            migrationBuilder.DropIndex(
                name: "IX_Category_GoalId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_UserId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Account_UserId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Goal");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ExchangeHistory");

            migrationBuilder.DropColumn(
                name: "GoalId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Account");

            migrationBuilder.AddColumn<int>(
                name: "TransactionAccountAccountId",
                table: "Transaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GoalUserUserId",
                table: "Goal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExchangeHistoryUserUserId",
                table: "ExchangeHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryUserUserId",
                table: "Category",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccountUserUserId",
                table: "Account",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CategoryGoal",
                columns: table => new
                {
                    CategoriesOfGoalCategoryId = table.Column<int>(type: "int", nullable: false),
                    CategoryGoalsGoalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryGoal", x => new { x.CategoriesOfGoalCategoryId, x.CategoryGoalsGoalId });
                    table.ForeignKey(
                        name: "FK_CategoryGoal_Category_CategoriesOfGoalCategoryId",
                        column: x => x.CategoriesOfGoalCategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryGoal_Goal_CategoryGoalsGoalId",
                        column: x => x.CategoryGoalsGoalId,
                        principalTable: "Goal",
                        principalColumn: "GoalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_TransactionAccountAccountId",
                table: "Transaction",
                column: "TransactionAccountAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Goal_GoalUserUserId",
                table: "Goal",
                column: "GoalUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeHistory_ExchangeHistoryUserUserId",
                table: "ExchangeHistory",
                column: "ExchangeHistoryUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_CategoryUserUserId",
                table: "Category",
                column: "CategoryUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_AccountUserUserId",
                table: "Account",
                column: "AccountUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryGoal_CategoryGoalsGoalId",
                table: "CategoryGoal",
                column: "CategoryGoalsGoalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Users_AccountUserUserId",
                table: "Account",
                column: "AccountUserUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Users_CategoryUserUserId",
                table: "Category",
                column: "CategoryUserUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeHistory_Users_ExchangeHistoryUserUserId",
                table: "ExchangeHistory",
                column: "ExchangeHistoryUserUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Goal_Users_GoalUserUserId",
                table: "Goal",
                column: "GoalUserUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Account_TransactionAccountAccountId",
                table: "Transaction",
                column: "TransactionAccountAccountId",
                principalTable: "Account",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Users_AccountUserUserId",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_Users_CategoryUserUserId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeHistory_Users_ExchangeHistoryUserUserId",
                table: "ExchangeHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Goal_Users_GoalUserUserId",
                table: "Goal");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Account_TransactionAccountAccountId",
                table: "Transaction");

            migrationBuilder.DropTable(
                name: "CategoryGoal");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_TransactionAccountAccountId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Goal_GoalUserUserId",
                table: "Goal");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeHistory_ExchangeHistoryUserUserId",
                table: "ExchangeHistory");

            migrationBuilder.DropIndex(
                name: "IX_Category_CategoryUserUserId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Account_AccountUserUserId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "TransactionAccountAccountId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "GoalUserUserId",
                table: "Goal");

            migrationBuilder.DropColumn(
                name: "ExchangeHistoryUserUserId",
                table: "ExchangeHistory");

            migrationBuilder.DropColumn(
                name: "CategoryUserUserId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "AccountUserUserId",
                table: "Account");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Transaction",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Goal",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ExchangeHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GoalId",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Account",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_AccountId",
                table: "Transaction",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Goal_UserId",
                table: "Goal",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeHistory_UserId",
                table: "ExchangeHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_GoalId",
                table: "Category",
                column: "GoalId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_UserId",
                table: "Category",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_UserId",
                table: "Account",
                column: "UserId");

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
                name: "FK_Transaction_Account_AccountId",
                table: "Transaction",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "AccountId");
        }
    }
}
