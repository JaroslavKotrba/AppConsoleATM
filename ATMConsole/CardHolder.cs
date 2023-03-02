using System;
using MySql.Data.MySqlClient;

public partial class cardHolder
{
    String cardNum;
    int pin;
    String firstName;
    String lastName;
    double balance;

    public cardHolder(string cardNum, int pin, string firstName, string lastName, double balance)
    {
        this.cardNum = cardNum;
        this.pin = pin;
        this.firstName = firstName;
        this.lastName = lastName;
        this.balance = balance;
    }

    // -----------------------------------------------------------------------------
    // Get

    public String getNum()
    {
        return cardNum;
    }

    public int getPin()
    {
        return pin;
    }

    public String getFirstName()
    {
        return firstName;
    }

    public String getLastName()
    {
        return lastName;
    }

    public double getBalance()
    {
        return balance;
    }

    // -----------------------------------------------------------------------------
    // Set

    public void setNum(String newCardNum)
    {
        cardNum = newCardNum;
    }

    public void setPin(int newPin)
    {
        pin = newPin;
    }

    public void setFirstName(String newFirstName)
    {
        firstName = newFirstName;
    }

    public void setLastName(String newLastName)
    {
        lastName = newLastName;
    }

    public void setBalance(double newBalance)
    {
        balance = newBalance;
    }

    // -----------------------------------------------------------------------------
    // MySQL

    static MySqlConnection con;

    public static void Connect(string user, string password, string database)
    {
        con = new MySqlConnection();

        try
        {
            con.ConnectionString = "server = localhost; User Id = " + user + "; " +
                "Persist Security Info = True; database = " + database + "; " + "Password = " + password;
            con.Open();
            Console.WriteLine("Succesfully connected!");

        }

        catch (Exception e)
        {
            Console.WriteLine("Not Successful! due to " + e.ToString());
        }
    }
}

