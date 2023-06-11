using System;   
using SplashKitSDK;
using System.Collections.Generic;

public class Bank
{
    private List<Account> _accounts =  new List<Account>();
    private List<Transaction> _transactions = new List<Transaction>();

    public void AddAccount(Account account)
    {
        this._accounts.Add(account);
    }

    public  Account GetAccount(string name)
    {
        foreach(Account account in this._accounts)
        {
            if(account.Name.ToLower().Trim() == name.ToLower().Trim())
            //This ensures that even letters with different upper and lower case are still equal.
            {
                return account;
                //If is there only one return... 
            }
        }
        return null; 
    }

    public List<Account> GetAccounts(string name)
    {
        List<Account> res = new List<Account>();
        foreach(Account acc in this._accounts)
        {
            if(acc.Name.ToLower().Trim() == name.ToLower().Trim())
            {
                res.Add(acc);
            }
        }
        return res;
    }


    public void ExecuteTransaction(Transaction transaction)
    
    {
        transaction.Execute();
        this._transactions.Add(transaction);
    }

    public void PrintTransactionHistory()
    {
        foreach(Transaction transaction in this._transactions)
        {
            transaction.Print();
        }
    }

}