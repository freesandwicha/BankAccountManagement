using System;
using SplashKitSDK;

namespace BankProgram
{
    public enum MenuOption
    {
        add_account,
        Withdraw,
        Deposit,
        Transfer,
        Print,
        PrintTransaction,
        Quit
    }

    public class Program
    {
        public static void Main()
        {
            //Account myAccount = new Account("HAN", 10000);
            //Account XiasAccount = new Account("XIA", 20000);
            Bank bank = new Bank();
            MenuOption userSelection;
            do
            {
                userSelection = readUserOption();
                switch (userSelection)
                {
                    //Then switch MenuOption  to some words...
                    case MenuOption.add_account:
                        {
                            doAddAcount(bank);
                            break;
                        }
                    case MenuOption.Withdraw:
                        {
                            doWithDraw(bank);
                            break;
                        }
                    case MenuOption.Deposit:
                        {
                            doWithDeposit(bank);
                            break;
                        }
                    case MenuOption.Transfer:
                        {
                            doWithTransfer(bank);
                            break;
                        }
                    case MenuOption.Print:
                        {
                            doWithPrint(bank);
                            break;
                        }
                    case MenuOption.PrintTransaction:
                        {
                            bank.PrintTransactionHistory();
                            break;
                        }

                    case MenuOption.Quit:
                        {
                            Console.WriteLine("Quit");
                            break;
                        }
                }
            }
            while (userSelection != MenuOption.Quit);
        }

        private static MenuOption readUserOption()
        {
            int option;

            Console.WriteLine("Please choose one from the following options :");
            Console.WriteLine("*********************");
            Console.WriteLine("1, Add account");
            Console.WriteLine("2, Withdraw");
            Console.WriteLine("3, Deposit");
            Console.WriteLine("4, Transfer");
            Console.WriteLine("5, Print");
            Console.WriteLine("6, PrintTransaction");
            Console.WriteLine("7, Quit");
            Console.WriteLine("*********************");
            do
            {
                //To make sure the menu is always available.
                try
                {
                    option = Convert.ToInt32(Console.ReadLine());
                    //Let the content converted to a number, because it is easier to manager numbers instead of string.
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception : " + ex.Message);
                    Console.WriteLine("Wrong section! Please try again!");
                    option = -1;
                    //When some exceptions were found, this code will meet the condition of cycle. 
                }
                if (option > 7 || option < 1)
                {
                    Console.WriteLine("Please select a vaild number between 1 and 7:");
                }
            } while (option > 7 || option < 1);

            return (MenuOption)(option - 1);


        }


        private static Account findAccount(Bank fromBank)
        {
            Console.Write("Enter account name: ");
            String name = Console.ReadLine();
            Account result = fromBank.GetAccount(name);
            if (result == null)
            {
                Console.WriteLine($"No account found with name {name}");
            }
            return result;
        }

        private static void doAddAcount(Bank bank)
        {
            Console.Write("Please enter the account name: ");
            string accountName = Console.ReadLine();

            while (true)
            {
                try
                {
                    Console.Write("Please enter the opening balance:");
                    decimal openingBalance = Convert.ToDecimal(Console.ReadLine());

                    if (openingBalance < 0)
                    {
                        throw new Exception();
                    }
                    bank.AddAccount(new Account(accountName, openingBalance));
                    break;
                }
                catch
                {
                    Console.WriteLine("Invalid opening balance. Please enter a valid balance");
                }
            }
        }


        private static void doWithDraw(Bank bank)
        {
            Account account = findAccount(bank);
            if (account == null)
            {
                return;
                //If account is null, then 'return' can jump from this whole method.
            }
            decimal amount;
            Console.Write("How much would you like to withdraw in " + account.Name + " ?");
            try
            {
                amount = Convert.ToDecimal(Console.ReadLine());
                WithdrawTransaction withdrawTransaction = new WithdrawTransaction(account, amount);
                //Call method via different objects 
                bank.ExecuteTransaction(withdrawTransaction);

                if (!withdrawTransaction.Successed)
                {
                    throw new Exception("Withdraw is not successful");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private static void doWithTransfer(Bank bank)
        {
            try
            {
                Account fromAccount = findAccount(bank);
                if (fromAccount == null)
                {
                    return;
                }
                Account toAccount = findAccount(bank);
                if (toAccount == null)
                {
                    return;
                }

                if (fromAccount == toAccount)
                {
                    throw new Exception("Same account...");
                }

                Console.WriteLine("How much would you like to tranfer into " + toAccount.Name + "'s account?");

                decimal amount = Convert.ToDecimal(Console.ReadLine());
                TransferTransaction transferTransaction = new TransferTransaction(fromAccount, toAccount, amount);
                try
                {
                       //Call method via different objects 
                    bank.ExecuteTransaction(transferTransaction);
                    if (!transferTransaction.Successed)
                    {
                        throw new Exception("Transfer was not successful!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        private static void doWithDeposit(Bank bank)
        {
            Account account = findAccount(bank);
            if (account == null)
            {
                return;
            }
            decimal amount;
            Console.Write("How much would you like to deposit into " + account.Name + "'s account?");
            try
            {
                amount = Convert.ToDecimal(Console.ReadLine());
                DepositTransaction depositTransaction = new DepositTransaction(account, amount);
                //Call method via different objects 
                bank.ExecuteTransaction(depositTransaction);
                if (!depositTransaction.Successed)
                {
                    throw new Exception("Deposit was not successful.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void doWithPrint(Bank bank)
        {
            Account account = findAccount(bank);
            if (account == null)
            {
                return;
            }
            account.Print();
        }
    }
}


