using System;
using SplashKitSDK;

public class TransferTransaction : Transaction
{
    private Account _fromAccount;
    private Account _toAccount;

    private WithdrawTransaction _theWithdraw;
    private DepositTransaction _theDeposit;


    public override bool Successed
    {
        get
        {
            if (this._theWithdraw.Successed && this._theDeposit.Successed)
                return true;
            else
                return false;
        }
    }

    public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(amount)
    //base will call the constructor from Parent class, as Transaction.Transcation(amout);
    {
        this._fromAccount = fromAccount;
        this._toAccount = toAccount;

        this._theWithdraw = new WithdrawTransaction(fromAccount, amount);
        //According the definition, it is a new object 
        this._theDeposit = new DepositTransaction(toAccount, amount);
    }

    public override void Execute()
    {
        base.Execute();
        this._theWithdraw.Execute();
        //Though the object of WithdrawTransaction to call the Execute()

        if (this._theWithdraw.Successed)
        {
            this._theDeposit.Execute();
            if (this._theDeposit.Successed)
            {
                this._theWithdraw.Rollback();
            }
        }
        else
        {
            throw new Exception("Cannot execute transfer. The withdraw transaction failed.");
        }
    }


    public override void Rollback()
    {
       base.Rollback();

        if (this._theWithdraw.Successed)
            this._theWithdraw.Rollback();

        if (this._theDeposit.Successed)
            this._theDeposit.Rollback();

        if (this._theWithdraw.Reversed && this._theDeposit.Reversed)
            this._reversed = true;
    }

    public  override void Print()
    {
        if (this._theWithdraw.Successed && this._theDeposit.Successed)
        {
            Console.WriteLine("A transfer of " + this._amount + " from " + this._fromAccount.Name + "'s account to "
             + this._toAccount.Name + "'s account was successful.");
            Console.Write("    ");
            this._theWithdraw.Print();
            Console.Write("    ");
            this._theDeposit.Print();
        }
        else
        {
            Console.WriteLine("Transfer was not successful");
            if (this._reversed)
                Console.WriteLine("Transfer was reversed");
        }
    }
}