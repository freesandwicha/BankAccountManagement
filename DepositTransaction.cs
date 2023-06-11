using System;

public class DepositTransaction : Transaction
{
    private Account _account;


    private bool _successed;
  

    public override bool Successed
    {
        get
        { return this._successed; }
    }
  
    public DepositTransaction(Account account, decimal amount):base(amount)
    {
        this._account = account;
    }

    public override void Execute()
    {
       base.Execute();
        this._successed =this._account.Deposit(this._amount);
    }

    public override void Rollback()
    {
        base.Rollback();
        if (this._account.Withdraw(this._amount))
        {
            this._reversed = true;
            this._executed = false;
            this._successed = false;
        }
        else
        {
            this._reversed = false;
            this._executed = true;
            this._successed = true;
        }
    }

    public override void Print()
    {
        if (this._successed)
        {
            Console.WriteLine("A deposit in " + this._amount + " of " + this._account.Name + "'s account was successful!");
        }
        else
        {
            Console.WriteLine("Deposit was Not successful!");
            if (this._reversed)
                Console.WriteLine("Deposit was reversed.");
        }
    }

}

