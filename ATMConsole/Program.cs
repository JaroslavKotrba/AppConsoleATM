// -----------------------------------------------------------------------------
// ATM Console Application

// cd "/Users/hp/Library/CloudStorage/OneDrive-Personal/Documents/DotNet/ATMConsole/ATMConsole"
// dotnet run

using System;
using MySql.Data.MySqlClient;

public partial class cardHolder
{   
    // -----------------------------------------------------------------------------
    // Options (Main)

    public static void Main(String[] args)
    {   
        // Change appearance
        Console.ForegroundColor = ConsoleColor.Blue;

        // Options
        void printOptions()
        {
            Console.WriteLine("\nPlease choose from one of the following options: ");
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Show Balance");
            Console.WriteLine("4. Show User Data");
            Console.WriteLine("5. Exit");
        }

        // Deposit
        void deposit(cardHolder currentUser)
        {
            Console.WriteLine("\nHow much CZK would you like to deposit: ");
            double deposit = Double.Parse(Console.ReadLine());
            currentUser.setBalance(currentUser.getBalance() + deposit);
            Console.WriteLine("Your new balance is: " + currentUser.getBalance() + " CZK");
        }

        // Withdraw
        void withdraw(cardHolder currentUser)
        {
            Console.WriteLine("\nHow much CZK would you like to withdraw: ");
            double withdrawl = Double.Parse(Console.ReadLine());
            // check if the user has enough money
            if (currentUser.getBalance() < withdrawl)
            {
                Console.WriteLine("Insufficient balance!");
            }
            else
            {
                // double newBalance = currentUser.getBalance() - withdrawl;
                currentUser.setBalance(currentUser.getBalance() - withdrawl);
                Console.WriteLine("\nYour new balance is: " + currentUser.getBalance() + " CZK");
            }
        }

        // Balance
        void balance(cardHolder currentUser)
        {
            Console.WriteLine("\nCurrent balance: " + currentUser.getBalance() + " CZK");
        }

        // User
        void check_data(cardHolder currentUser)
        {
            Console.WriteLine(
                "\nCard number: " + currentUser.getNum() +
                "; Pin: " + currentUser.getPin() +
                "; Name: " + currentUser.getFirstName() + " " + currentUser.getLastName());
        }

        // -----------------------------------------------------------------------------
        // Database

        // Connection SQL
        List<cardHolder> cardHolders = new List<cardHolder>();
        Connect("root", "8bulwark5", "bank");

            string query = "SELECT * FROM customers";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                string card_number = dataReader.GetString(1);
                int pin_number = dataReader.GetInt16(2);
                string first_name = dataReader.GetString(3);
                string last_name = dataReader.GetString(4);
                float balance_db = dataReader.GetFloat(5);

                cardHolders.Add(new cardHolder(dataReader.GetString(1), dataReader.GetInt16(2), dataReader.GetString(3), dataReader.GetString(4), dataReader.GetFloat(5)));
            }

        con.Close();

        // Connection local data
        // List<cardHolder> cardHolders = new List<cardHolder>();
        // cardHolders.Add(new cardHolder("123456789", 1234, "aroslav", "Kotrba", 1200.5));
        // cardHolders.Add(new cardHolder("987654321", 4321, "Anna", "ndrackova", 2200.5));

        // -----------------------------------------------------------------------------
        // Validation and use

        Console.WriteLine("\nWelcome to BARCLAYS");
        Console.WriteLine("Please insert your debit card: ");
        String debitCardNum = "";
        cardHolder currentUser;

        // Validation of your CARD
        while (true)
        {
            try
            {
                debitCardNum = Console.ReadLine();
                // Check agains the database
                currentUser = cardHolders.FirstOrDefault(a => a.cardNum == debitCardNum);
                if (currentUser != null) { break; }
                else { Console.WriteLine("Card not recognized. Please try again"); }
            }
            catch { Console.WriteLine("Card not recognized. Please try again"); }
        }

        Console.WriteLine("Please enter your PIN: ");
        int userPin = 0;

        // Validation of your PIN
        while (true)
        {
            try
            {   // reading double
                userPin = int.Parse(Console.ReadLine());
                // Check agains the database
                if (currentUser.getPin() == userPin) { break; }
                else { Console.WriteLine("Incorrect PIN. Please try again"); }
            }
            catch { Console.WriteLine("Incorrect PIN. Please try again"); }
        }

        // WELCOME
        Console.WriteLine("\n------------------");
        Console.WriteLine("BARCLAYS BANK ----");
        Console.WriteLine("------------------");

        Console.WriteLine("\nWelcome " + currentUser.getFirstName());
        int option = 0;
        do
        {
            printOptions();
            try
            {
                option = int.Parse(Console.ReadLine());
            }
            catch { Console.WriteLine("Not working!"); }
            if (option == 1) { deposit(currentUser); }
            else if (option == 2) { withdraw(currentUser); }
            else if (option == 3) { balance(currentUser); }
            else if (option == 4) { check_data(currentUser); }
            else if (option == 5) { break; }
            else { Console.WriteLine("NOT A VALID OPTION!"); }

        }
        while (option != 5);
        Console.WriteLine("\nEXIT");
        Console.WriteLine("Thank you! Have a nice day");
    }
}