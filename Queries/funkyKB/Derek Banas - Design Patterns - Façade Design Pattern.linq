<Query Kind="Program" />

void Main()
{
    BankAccountFacade accessingBank = new BankAccountFacade(12345678, 1234);

    accessingBank.withdrawCash(50.00);

    accessingBank.withdrawCash(990.00);
}

/*
    Derek Banas: Façade Patterns
    Façade Design Pattern
    [ 📖 http://www.newthinktank.com/2012/09/facade-design-pattern-tutorial/ ]
    [ 📽 https://www.youtube.com/watch?v=pL4mOUDi54o ]
*/

public class WelcomeToBank
{
    public WelcomeToBank()
    {
        Console.WriteLine("Welcome to ABC Bank");
        Console.WriteLine("We are happy to give you your money if we can find it");
    }
}

public class AccountNumberCheck
{
    private int accountNumber = 12345678;

    public int getAccountNumber() { return accountNumber; }

    public bool accountActive(int acctNumToCheck)
    {
        if (acctNumToCheck == getAccountNumber())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

public class SecurityCodeCheck
{
    private int securityCode = 1234;

    public int getSecurityCode() { return securityCode; }

    public bool isCodeCorrect(int secCodeToCheck)
    {
        if (secCodeToCheck == getSecurityCode())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

public class FundsCheck
{

    private double cashInAccount = 1000.00;

    public double getCashInAccount() { return cashInAccount; }

    public void decreaseCashInAccount(double cashWithdrawn) { cashInAccount -= cashWithdrawn; }

    public void increaseCashInAccount(double cashDeposited) { cashInAccount += cashDeposited; }

    public bool haveEnoughMoney(double cashToWithdrawal)
    {
        if (cashToWithdrawal > getCashInAccount())
        {
            Console.WriteLine("Error: You don't have enough money");
            Console.WriteLine("Current Balance: " + getCashInAccount());

            return false;
        }
        else
        {
            decreaseCashInAccount(cashToWithdrawal);

            Console.WriteLine("Withdrawal Complete: Current Balance is " + getCashInAccount());

            return true;
        }
    }

    public void makeDeposit(double cashToDeposit)
    {
        increaseCashInAccount(cashToDeposit);

        Console.WriteLine("Deposit Complete: Current Balance is " + getCashInAccount());
    }
}

// The Facade Design Pattern decouples or separates the client 
// from all of the sub components
// The Facades aim is to simplify interfaces so you don't have 
// to worry about what is going on under the hood
public class BankAccountFacade
{
    private int accountNumber;
    private int securityCode;

    AccountNumberCheck acctChecker;
    SecurityCodeCheck codeChecker;
    FundsCheck fundChecker;

    WelcomeToBank bankWelcome;

    public BankAccountFacade(int newAcctNum, int newSecCode)
    {
        accountNumber = newAcctNum;
        securityCode = newSecCode;

        bankWelcome = new WelcomeToBank();

        acctChecker = new AccountNumberCheck();
        codeChecker = new SecurityCodeCheck();
        fundChecker = new FundsCheck();
    }

    public int getAccountNumber() { return accountNumber; }

    public int getSecurityCode() { return securityCode; }

    public void withdrawCash(double cashToGet)
    {
        if (acctChecker.accountActive(getAccountNumber()) &&
                codeChecker.isCodeCorrect(getSecurityCode()) &&
                fundChecker.haveEnoughMoney(cashToGet))
        {
            Console.WriteLine("Transaction Complete");
        }
        else
        {
            Console.WriteLine("Transaction Failed");
        }
    }

    public void depositCash(double cashToDeposit)
    {
        if (acctChecker.accountActive(getAccountNumber()) &&
                codeChecker.isCodeCorrect(getSecurityCode()))
        {
            fundChecker.makeDeposit(cashToDeposit);

            Console.WriteLine("Transaction Complete");
        }
        else
        {
            Console.WriteLine("Transaction Failed");
        }
    }
}