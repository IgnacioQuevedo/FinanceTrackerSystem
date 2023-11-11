﻿// <auto-generated />
using System;
using DataManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataManagers.Migrations
{
    [DbContext(typeof(SqlContext))]
    partial class SqlContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BusinessLogic.Account_Components.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountId"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Currency")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AccountId");

                    b.HasIndex("UserId");

                    b.ToTable("Account");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("BusinessLogic.Category_Components.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("BusinessLogic.ExchangeHistory_Components.ExchangeHistory", b =>
                {
                    b.Property<int>("ExchangeHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExchangeHistoryId"));

                    b.Property<int>("Currency")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("ValueDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ExchangeHistoryId");

                    b.HasIndex("UserId");

                    b.ToTable("ExchangeHistory");
                });

            modelBuilder.Entity("BusinessLogic.Goal_Components.Goal", b =>
                {
                    b.Property<int>("GoalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GoalId"));

                    b.Property<int>("CurrencyOfAmount")
                        .HasColumnType("int");

                    b.Property<int>("MaxAmountToSpend")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("GoalId");

                    b.HasIndex("UserId");

                    b.ToTable("Goal");
                });

            modelBuilder.Entity("BusinessLogic.Transaction_Components.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"));

                    b.Property<int?>("AccountId")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Currency")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("TransactionId");

                    b.HasIndex("AccountId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("BusinessLogic.User_Components.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CategoryGoal", b =>
                {
                    b.Property<int>("CategoriesOfGoalCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryGoalsGoalId")
                        .HasColumnType("int");

                    b.HasKey("CategoriesOfGoalCategoryId", "CategoryGoalsGoalId");

                    b.HasIndex("CategoryGoalsGoalId");

                    b.ToTable("CategoryGoal");
                });

            modelBuilder.Entity("BusinessLogic.Account_Components.CreditCardAccount", b =>
                {
                    b.HasBaseType("BusinessLogic.Account_Components.Account");

                    b.Property<decimal>("AvailableCredit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("ClosingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IssuingBank")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Last4Digits")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("CreditCardAccounts", (string)null);
                });

            modelBuilder.Entity("BusinessLogic.Account_Components.MonetaryAccount", b =>
                {
                    b.HasBaseType("BusinessLogic.Account_Components.Account");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.ToTable("MonetaryAccounts", (string)null);
                });

            modelBuilder.Entity("BusinessLogic.Account_Components.Account", b =>
                {
                    b.HasOne("BusinessLogic.User_Components.User", "AccountUser")
                        .WithMany("MyAccounts")
                        .HasForeignKey("UserId");

                    b.Navigation("AccountUser");
                });

            modelBuilder.Entity("BusinessLogic.Category_Components.Category", b =>
                {
                    b.HasOne("BusinessLogic.User_Components.User", "CategoryUser")
                        .WithMany("MyCategories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryUser");
                });

            modelBuilder.Entity("BusinessLogic.ExchangeHistory_Components.ExchangeHistory", b =>
                {
                    b.HasOne("BusinessLogic.User_Components.User", "ExchangeHistoryUser")
                        .WithMany("MyExchangesHistory")
                        .HasForeignKey("UserId");

                    b.Navigation("ExchangeHistoryUser");
                });

            modelBuilder.Entity("BusinessLogic.Goal_Components.Goal", b =>
                {
                    b.HasOne("BusinessLogic.User_Components.User", "GoalUser")
                        .WithMany("MyGoals")
                        .HasForeignKey("UserId");

                    b.Navigation("GoalUser");
                });

            modelBuilder.Entity("BusinessLogic.Transaction_Components.Transaction", b =>
                {
                    b.HasOne("BusinessLogic.Account_Components.Account", "TransactionAccount")
                        .WithMany("MyTransactions")
                        .HasForeignKey("AccountId");

                    b.HasOne("BusinessLogic.Category_Components.Category", "TransactionCategory")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.Navigation("TransactionAccount");

                    b.Navigation("TransactionCategory");
                });

            modelBuilder.Entity("CategoryGoal", b =>
                {
                    b.HasOne("BusinessLogic.Category_Components.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesOfGoalCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessLogic.Goal_Components.Goal", null)
                        .WithMany()
                        .HasForeignKey("CategoryGoalsGoalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BusinessLogic.Account_Components.CreditCardAccount", b =>
                {
                    b.HasOne("BusinessLogic.Account_Components.Account", null)
                        .WithOne()
                        .HasForeignKey("BusinessLogic.Account_Components.CreditCardAccount", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BusinessLogic.Account_Components.MonetaryAccount", b =>
                {
                    b.HasOne("BusinessLogic.Account_Components.Account", null)
                        .WithOne()
                        .HasForeignKey("BusinessLogic.Account_Components.MonetaryAccount", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BusinessLogic.Account_Components.Account", b =>
                {
                    b.Navigation("MyTransactions");
                });

            modelBuilder.Entity("BusinessLogic.User_Components.User", b =>
                {
                    b.Navigation("MyAccounts");

                    b.Navigation("MyCategories");

                    b.Navigation("MyExchangesHistory");

                    b.Navigation("MyGoals");
                });
#pragma warning restore 612, 618
        }
    }
}
