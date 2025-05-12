using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker
{
    class Expense
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
    }

    class Program
    {
        static List<Expense> expenses = new List<Expense>();
        static decimal monthlyBudget = 0;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Expense Tracker ====");
                Console.WriteLine("1. Enter Expense");
                Console.WriteLine("2. Categorize Expenses");
                Console.WriteLine("3. Monthly Budgeting");
                Console.WriteLine("4. View Reports");
                Console.WriteLine("5. Set Monthly Budget");
                Console.WriteLine("6. View All Expenses");
                Console.WriteLine("7. Delete Expense");
                Console.WriteLine("8. Exit");
                Console.WriteLine("==========================");
                Console.Write("Enter your choice (1-8): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        EnterExpense();
                        break;
                    case "2":
                        CategorizeExpenses();
                        break;
                    case "3":
                        MonthlyBudgeting();
                        break;
                    case "4":
                        ViewReports();
                        break;
                    case "5":
                        SetMonthlyBudget();
                        break;
                    case "6":
                        ViewAllExpenses();
                        break;
                    case "7":
                        DeleteExpense();
                        break;
                    case "8":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        static void EnterExpense()
        {
            Console.Clear();
            Console.WriteLine("==== Enter Expense ====");
            Console.Write("Enter the amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());
            Console.Write("Enter the description: ");
            string description = Console.ReadLine();
            Console.Write("Enter the category: ");
            string category = Console.ReadLine();
            DateTime date = DateTime.Now;

            expenses.Add(new Expense
            {
                Amount = amount,
                Description = description,
                Category = category,
                Date = date
            });

            Console.WriteLine("Expense entered successfully.");

            CheckBudgetExceeded(date.Month, date.Year);
        }

        static void CategorizeExpenses()
        {
            Console.Clear();
            Console.WriteLine("==== Categorize Expenses ====");

            if (expenses.Count == 0)
            {
                Console.WriteLine("No expenses found.");
                return;
            }

            Console.WriteLine("Expense List:");
            for (int i = 0; i < expenses.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Amount: {expenses[i].Amount}, Description: {expenses[i].Description}, Category: {expenses[i].Category}");
            }

            Console.Write("Enter the expense number to categorize: ");
            int expenseNumber = int.Parse(Console.ReadLine());

            if (expenseNumber < 1 || expenseNumber > expenses.Count)
            {
                Console.WriteLine("Invalid expense number.");
                return;
            }

            Expense expense = expenses[expenseNumber - 1];

            Console.Write("Enter the new category: ");
            string category = Console.ReadLine();

            expense.Category = category;

            Console.WriteLine("Expense categorized successfully.");
        }

        static void MonthlyBudgeting()
        {
            Console.Clear();
            Console.WriteLine("==== Monthly Budgeting ====");

            if (expenses.Count == 0)
            {
                Console.WriteLine("No expenses found.");
                return;
            }

            Console.Write("Enter the month (1-12): ");
            int month = int.Parse(Console.ReadLine());

            Console.Write("Enter the year: ");
            int year = int.Parse(Console.ReadLine());

            decimal totalExpense = expenses
                .Where(e => e.Date.Month == month && e.Date.Year == year)
                .Sum(e => e.Amount);

            Console.WriteLine($"Total expenses for {month}/{year}: {totalExpense}");

            if (monthlyBudget > 0)
            {
                Console.WriteLine($"Set Budget: {monthlyBudget}");
                if (totalExpense > monthlyBudget)
                    Console.WriteLine("⚠️ Warning: You have exceeded your monthly budget!");
            }
        }

        static void ViewReports()
        {
            Console.Clear();
            Console.WriteLine("==== View Reports ====");

            if (expenses.Count == 0)
            {
                Console.WriteLine("No expenses found.");
                return;
            }

            Console.Write("Enter the category to view expenses: ");
            string category = Console.ReadLine();

            var categoryExpenses = expenses
                .Where(e => e.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (categoryExpenses.Count == 0)
            {
                Console.WriteLine("No expenses found for the category.");
                return;
            }

            Console.WriteLine($"Expenses for Category: {category}");
            foreach (var expense in categoryExpenses)
            {
                Console.WriteLine($"Amount: {expense.Amount}, Description: {expense.Description}, Date: {expense.Date}");
            }
        }

        static void SetMonthlyBudget()
        {
            Console.Clear();
            Console.WriteLine("==== Set Monthly Budget ====");
            Console.Write("Enter the monthly budget amount: ");
            monthlyBudget = decimal.Parse(Console.ReadLine());
            Console.WriteLine($"Monthly budget set to: {monthlyBudget}");
        }

        static void CheckBudgetExceeded(int month, int year)
        {
            if (monthlyBudget <= 0)
                return;

            decimal total = expenses
                .Where(e => e.Date.Month == month && e.Date.Year == year)
                .Sum(e => e.Amount);

            if (total > monthlyBudget)
                Console.WriteLine("⚠️ Alert: You have exceeded your set monthly budget!");
        }

        static void ViewAllExpenses()
        {
            Console.Clear();
            Console.WriteLine("==== All Expenses ====");

            if (expenses.Count == 0)
            {
                Console.WriteLine("No expenses found.");
                return;
            }

            for (int i = 0; i < expenses.Count; i++)
            {
                var e = expenses[i];
                Console.WriteLine($"{i + 1}. Amount: {e.Amount}, Description: {e.Description}, Category: {e.Category}, Date: {e.Date}");
            }
        }

        static void DeleteExpense()
        {
            Console.Clear();
            Console.WriteLine("==== Delete Expense ====");

            if (expenses.Count == 0)
            {
                Console.WriteLine("No expenses to delete.");
                return;
            }

            ViewAllExpenses();

            Console.Write("Enter the number of the expense to delete: ");
            int index = int.Parse(Console.ReadLine());

            if (index < 1 || index > expenses.Count)
            {
                Console.WriteLine("Invalid index.");
                return;
            }

            expenses.RemoveAt(index - 1);
            Console.WriteLine("Expense deleted successfully.");
        }
    }
}
