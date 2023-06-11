using System;

public class Account

{
    private decimal _balance;
    private string _name;

    public Account(string name, decimal startingBalance)

    {
        this._name = name;
        this._balance = startingBalance;
    }

    public Boolean Deposit(decimal amountToAdd)
    {
        if (amountToAdd > 0)
        {
            _balance = _balance + amountToAdd;
            return true;
        }
        else
            return false;
    }

    public Boolean Withdraw(decimal amountToWithdraw)
    {
        if ((amountToWithdraw > 0) && (_balance >= amountToWithdraw))
        {
            _balance = _balance - amountToWithdraw;
            return true;
        }
        else
            return false;
    }

    public string Name
    {
        get

        { return _name; }
    }

    public void Print()
    {
        Console.WriteLine("The account name is " + _name);
        Console.WriteLine("The account balance is " + _balance);
    }
}