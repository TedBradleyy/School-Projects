
// TO ENTER ADMIN AREA, ENTER 99 WHEN PROMPTED TO ENTER COINS OR IN MENU
// ADMIN MENU PASSWORD = 1027

using System;
using System.Linq;
using System.Threading;


class Program
{
    struct Products // new struct that will hold data for each item that will be sold, with item Name, Price and number of stock.
    {
        public string Name;
        public double Price;
        public int Stock;
    }
    static void Main()
    {
        Random rndm = new Random();
        Products[] ProductInfo = new Products[10]; // randomises the stock of the items to simulate actual use of the Vending Machine.

        ProductInfo[0] = new Products { Name = "Starburst", Price = 1.00, Stock = rndm.Next(0, 10) };
        ProductInfo[1] = new Products { Name = "Protein Bar", Price = 2.50, Stock = rndm.Next(0, 10) };
        ProductInfo[2] = new Products { Name = "Fruit Pastels", Price = 1.00, Stock = rndm.Next(0, 10) };
        ProductInfo[3] = new Products { Name = "M&Ms", Price = 1.50, Stock = rndm.Next(0, 10) };
        ProductInfo[4] = new Products { Name = "Skittles", Price = 1.25, Stock = rndm.Next(0, 10) };                        // sets base stock of the vending machine
        ProductInfo[5] = new Products { Name = "Salt & Vinegar Crisps", Price = 1.00, Stock = rndm.Next(0, 10) };
        ProductInfo[6] = new Products { Name = "Chewing Gum", Price = 2.00, Stock = rndm.Next(0, 10) };
        ProductInfo[7] = new Products { Name = "KitKat", Price = 1.25, Stock = rndm.Next(0, 10) };
        ProductInfo[8] = new Products { Name = "Twirl", Price = 0.50, Stock = rndm.Next(0, 10) };
        ProductInfo[9] = new Products { Name = "Mars Bar", Price = 1.75, Stock = rndm.Next(0, 10) };

        Console.WriteLine("Welcome to the Aquinas Vending Machine.\n");
        Console.WriteLine("Currently for sale, we have:");
        int itemnumber = 1;
        foreach (Products items in ProductInfo) // writes all products and their information (price and stock), with dashes inbetween to improve looks
        {
            Console.WriteLine($"---------------------------------\n({itemnumber}) {items.Name} (£{items.Price.ToString("0.00")}) STOCK AVAILABLE: {items.Stock}");
            itemnumber++;
        }
        Console.WriteLine("---------------------------------");
        Thread.Sleep(500);
        double user_balance = 0; //defines the user balance as zero

        CoinCounter(ref user_balance, ProductInfo); //calls seperate method to Prompt the user to add coins.
    }

    static void BuyProduct(ref double userbalance, Products[] ProductInfo)
    {
        Console.Clear(); //clears console to de-clutter

        int itemnumber = 1; //integer to be used to match the product to the correct code when written e.g 1. Starburst
        Console.WriteLine("Currently for sale, we have:");
        foreach (Products items in ProductInfo) //reminds the user of the available products.
        {
            Console.WriteLine($"---------------------------------\n({itemnumber}) {items.Name} (£{items.Price.ToString("0.00")}) STOCK AVAILABLE: {items.Stock}");
            itemnumber++;
        }
        Console.WriteLine("---------------------------------");

        int userselection = 0;
        bool selectionmade = false; // bool set up that will only set to true when the user makes a valid input.

        while (selectionmade == false) //will repeat unless a valid input is detected
        {
            Console.Write("Please enter your selection: ");
            try //any exceptions are handled rather than an error - increased security
            {
                userselection = int.Parse(Console.ReadLine());
                if (userselection < 1 || userselection > 10) //if the selection is not between 1 and 10, an invalid message is displayed
                {
                    Console.WriteLine("Invalid Selection. Please try again.");
                    Thread.Sleep(1000);
                    BuyProduct(ref userbalance, ProductInfo); //Process restarts
                }
                selectionmade = true; //selection is made and the loop is broken
            }
            catch
            {
                Console.WriteLine("Invalid selection. Please try again.\n"); //exception handled
            }
        }

        if (userbalance < ProductInfo[userselection - 1].Price) //checks if the user has sufficient funds, if not, the user is returned to the menu
        {
            Console.WriteLine("You don't have enough money to buy this item. Returning to Menu...");
            Thread.Sleep(500);
            Menu(ref userbalance, ProductInfo);
            Console.Clear();
            Thread.Sleep(500);
        }

        else
        {
            if (ProductInfo[userselection - 1].Stock <= 0) //displays an out of stock message if no stock
            {
                Console.WriteLine("Unfortunately we are out of stock for this item. Restarting...");
                Thread.Sleep(1000);
                BuyProduct(ref userbalance, ProductInfo);
            }

            else
            {
                Console.WriteLine($"{ProductInfo[userselection - 1].Name} selected... Enjoy!\n"); //if valid entry, the selection is confirmed
                ProductInfo[userselection - 1].Stock--; // one is taken from stock as the user has bought one
                userbalance = userbalance - ProductInfo[userselection - 1].Price; // the price of the item is detucted from the users balance
                Thread.Sleep(1000);
                Console.Clear();
                Menu(ref userbalance, ProductInfo); //returns to menu
            }
        }
    }
    static void CoinCounter(ref double balance, Products[] ProductInfo) //user balance is passed by reference so changes can be made to the user balance when coins are added
    {
        Thread.Sleep(500);
        Console.Write("(We accept the coins: 5p, 10p, 20p, 50p, £1, £2)\n");

        bool usercontinue = true; //bool that will be set to false when the user is finished entering coins

        while (usercontinue == true)
        {
            Console.Write("\nPlease insert your coins. If you are finished, press 0.\n\nENTER COINS: ");
            string usercoin = Console.ReadLine();
            switch (usercoin.ToString())
            {
                case "5p":
                    balance += 0.05;
                    Console.Clear();
                    break;

                case "10p":
                    balance += 0.01;
                    Console.Clear();
                    break;

                case "20p":
                    balance += 0.2;
                    Console.Clear();
                    break;

                case "50p":
                    balance += 0.5;
                    Console.Clear();
                    break;

                case "£1":
                    balance += 1;
                    Console.Clear();
                    break;

                case "£2":
                    balance += 2;
                    Console.Clear();
                    break;

                case "0":
                    usercontinue = false; //when the exit value is entered, the loop is broken as 'usercontinue' is false and the while loop condition is no longer satisfied
                    Console.Clear();
                    break;

                case "99":
                    Admin(ref ProductInfo); // if the admin value is entered, the Admin method is called
                    Console.Clear();
                    break;

                default: //any invalid entries are handled with an error message and the user is prompted to enter a different coin
                    Thread.Sleep(500);
                    Console.WriteLine("\nInvalid coin. Please try Again.\nReminder that we only accept these coins:\n\n5p, 10p, 20p, 50p, £1, £2\n");
                    break;

            }
            Thread.Sleep(500);
            Console.WriteLine("-----------------------\nYour balance is: £" + balance.ToString("0.00") + "\n-----------------------"); // the users final balance is written
            Thread.Sleep(1500);
        }
        BuyProduct(ref balance, ProductInfo); //after the coins are entered, the user can now buy a product
    }
    static void Menu(ref double user_balance, Products[] ProductInfo)
    {
        bool SessionActive = true;

        while (SessionActive == true) //sessionactive will be set to false when the user selects the 'exit' function
        {
            bool modeSelected = false;

            while (modeSelected == false)
            {
                Console.WriteLine("-----------------------\nYour balance is: £" + user_balance.ToString("0.00") + "\n-----------------------\nPlease select what you would like to do.\n1. Buy a product\n2. Insert Coins\n3. Exit");
                string mode = Console.ReadLine().ToString(); // user selects one of the available modes

                switch (mode)
                {
                    case "1":
                        if (user_balance == 0)
                        {
                            Console.WriteLine("Your balance is currently 0. Please enter coins."); //user prompted to enter coins as with a balance of 0 they cannot buy anything
                            CoinCounter(ref user_balance, ProductInfo);
                        }
                        BuyProduct(ref user_balance, ProductInfo); // else the user can buy a product
                        modeSelected = true;
                        break;

                    case "2":
                        Console.Clear();
                        CoinCounter(ref user_balance, ProductInfo); //the user can enter more coins if they want
                        BuyProduct(ref user_balance, ProductInfo); //after coins are entered, the user can enter coins
                        modeSelected = true; // loop is broken
                        break;

                    case "3": //exit value
                        Console.WriteLine("£" + user_balance + " has been refunded."); //refunds the remaining credit
                        Thread.Sleep(1500);
                        Console.Clear();
                        Console.WriteLine("Service restarting in:");
                        for (int i = 5; i > 0; i--) //countdown from 5 seconds before the service restarts
                        {
                            Console.WriteLine(i);
                            Thread.Sleep(1000);
                        }
                        Console.Clear();
                        Console.WriteLine("---------------\nNEXT CUSTOMER PLEASE\n---------------");
                        user_balance = 0; //new user so credit must be set to zero
                        Menu(ref user_balance, ProductInfo); //returns to menu
                        modeSelected = true;
                        SessionActive = false;
                        break;

                    case "99": //admin value 
                        Admin(ref ProductInfo);//admin method called, product info passed by reference so changes can be made
                        break;

                    default:
                        Console.WriteLine("Invalid Input. Please try again."); //any invalid input loops back to beginning
                        Thread.Sleep(1000);
                        Console.Clear();
                        break;

                }
            }
        }
    }
    static void Admin(ref Products[] ItemsForSale)
    {
        Console.Write("Enter Password: ");

        if (Console.ReadLine() == "1027") //only correct password grants entry
        {
            Console.WriteLine("Access granted...\n");
            Thread.Sleep(1000);
            Console.Clear();

            bool AdminFinished = false;
            while (AdminFinished == false)
            {

                try
                {
                    Console.WriteLine("What would you like to do?\n1. Change an Item\n2. Change the price of an item\n3. Add stock to an item.\n4. Exit");
                    int AdminChoice = int.Parse(Console.ReadLine()); //converts input to integer and stores, to be used in the switch statement

                    switch (AdminChoice)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine("Enter the Number of the Product you would like to remove. Current items are:\n");
                            int itemnumber = 1;
                            foreach (Products items in ItemsForSale) // displays all items
                            {
                                Console.WriteLine(itemnumber + ": " + items.Name);
                                itemnumber++;
                            }
                            int ProductToReplace = int.Parse(Console.ReadLine()); //user enters the product they wish to replace

                            Console.Write("\nPlease enter the Name of the new Product:");
                            ItemsForSale[ProductToReplace - 1].Name = Console.ReadLine(); //enter the name of the new product

                            Console.Write("Please Enter the Price of the new Product. Do not include the £ sign, for example '£2.50' is not valid, '2.50' is valid"); //shows the user the accepted format
                            ItemsForSale[ProductToReplace - 1].Price = double.Parse(Console.ReadLine()); //enter the price of the new product

                            Console.Write("How many do we have of the new product?");
                            ItemsForSale[ProductToReplace - 1].Stock = int.Parse(Console.ReadLine()); //user enters the available stock of the new product

                            Console.WriteLine("Confirmed...");
                            Thread.Sleep(1000);
                            AdminFinished = true; //loop is broken and returns to menu
                            break;

                        case 2:
                            Console.Clear();
                            Console.WriteLine("Please choose the number of the item you wish to change the price of:");
                            itemnumber = 1;
                            foreach (Products items in ItemsForSale)
                            {
                                Console.WriteLine(itemnumber + ": " + items.Name); //displays all items available
                                itemnumber++;
                            }

                            int itemForPriceChange = int.Parse(Console.ReadLine());
                            Console.WriteLine("What is the new price of the item? Do not include the £ sign, for example '£2.50' is not valid, '2.50' is valid");
                            ItemsForSale[itemForPriceChange - 1].Price = double.Parse(Console.ReadLine()); // user changes the price of the item
                            Console.WriteLine("Confirmed.");
                            Thread.Sleep(1000);
                            AdminFinished = true;
                            break;

                        case 3:
                            Console.Clear();
                            Console.WriteLine("What item would you like to add stock to?");
                            itemnumber = 1;
                            foreach (Products items in ItemsForSale)
                            {
                                Console.WriteLine(itemnumber + ": " + items.Name); //displays all items available
                                itemnumber++;
                            }
                            int itemForStockChange = int.Parse(Console.ReadLine());
                            Console.WriteLine("How many more stock do we have?");
                            ItemsForSale[itemForStockChange - 1].Stock = ItemsForSale[itemForStockChange - 1].Stock + int.Parse(Console.ReadLine()); //adds current stock to new stock
                            Console.WriteLine("Confirmed.");
                            Thread.Sleep(1000);
                            AdminFinished = true; //loop broken
                            break;

                        case 4:
                            Console.Clear();
                            Console.WriteLine("Returning to menu in:"); //exit value is entered and the user is returned to the menu
                            for (int i = 5; i > 0; i--)
                            {
                                Console.WriteLine(i);
                                Thread.Sleep(1000);
                            }
                            Console.Clear();
                            break;

                        default:
                            Console.WriteLine("Invalid. Please try again."); //any exceptions are handled and loops back
                            Thread.Sleep(1000);
                            Console.Clear();
                            break;

                    }
                }
                catch
                {
                    Console.WriteLine("Invalid. Please try again.");
                    Thread.Sleep(1000);
                    Console.Clear();
                    //any exceptions are handled and loops back
                }
            }

        }
        else
        {
            Console.WriteLine("\nIncorrect Password. Returning to Menu\n"); //incorrect passwords are returned to the menu
            Thread.Sleep(1000);
        }
    }
}