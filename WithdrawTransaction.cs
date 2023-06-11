using System;
using SplashKitSDK;
public class WithdrawTransaction : Transaction
{
    private Account _account;
    private bool _successed;

    public override bool Successed
    {
        get
        { return this._successed; }
    }

    public WithdrawTransaction(Account account, decimal amount) : base(amount)
    {
        this._account = account;
    }

    public override void Execute()
    {
        base.Execute();
        this._successed = _account.Withdraw(_amount);
        //To check the object from Account, using the same code.
    }


    public void Rollback()
    {
        base.Rollback();
       

        if (this._account.Deposit(this._amount))
        //Cause the deposit function can return true(The number can be added to the account) or false.
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
            Console.WriteLine("A withdraw of " + this._amount + " from " + this._account.Name + "'s account was successful!");
        }
        else
        {
            Console.WriteLine("Withdraw was Not successful!");
            if (this._reversed)
                Console.WriteLine("Withdraw was reversed.");
        }
    }

}








