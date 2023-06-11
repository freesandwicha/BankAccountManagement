using System;
using SplashKitSDK;

public abstract class Transaction
{
    protected decimal _amount;
    protected bool _executed;
    protected bool _reversed;
    private DateTime _dataStamp;
    public bool Executed
        {
            get{return this._executed;}
        }
    public bool Reversed
        {
            get{return this._reversed;}
        }
    public  DateTime DateStamp
        {
            get{return this._dataStamp;}
        }

     public abstract bool Successed
        {
            get;
        }

    public Transaction(decimal amount)
    {
        this._amount = amount;
    }

    public abstract void Print();
    public virtual void Execute()
    {
         if (this._executed)
        {
            throw new Exception("Cannot execute this transaction as it has already been executed");
        }
        this._dataStamp = DateTime.Now;
        this._executed = true;
    }

    public virtual void Rollback()
    {
        if (!this._executed)
        {
            throw new Exception("Cannot roll back transaction.No excuted.");
        }

        if (this._reversed)
        {
            throw new Exception("Cannot roll back transaction. Already reversed.");
        }

    }
   







}