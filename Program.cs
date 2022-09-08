using System;
using System.Data;
using System.Diagnostics;//used for Stopwatch class

using MySql.Data;
using MySql.Data.MySqlClient;

using MySqlAccess;
using BusinessLogic;
using System.Collections;
using BusinessEntities;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Console.WriteLine("The current time is " + DateTime.Now);

Stopwatch stopwatch = new Stopwatch();

int userInput = 0;
int price = 0;

int package = -1;
var toppingsArraylist = new ArrayList();
int iceCreamBallsNumber = 0;                                    /// משתנה של כמות הכדורים 
var fDict = new Dictionary<int, int>();  ///// פה אני יוצר את המילון 
for (int i = 0; i < 10; i++)
{
    fDict.Add(i, 0);
}

Console.WriteLine("_____________________");
Console.WriteLine("Please choose a task:");
Console.WriteLine("1 - create empty tables");
userInput = Int32.Parse(Console.ReadLine());

if (userInput == 1)
    Logic.createTables();

Console.WriteLine("Please choose a task:");
Console.WriteLine("1 - order an ice cream");
Console.WriteLine("2 - exit");
userInput = Int32.Parse(Console.ReadLine());

switch (userInput)
{
    case 1:
    NEW_ORDER:
        Console.WriteLine("Please choose a package:");
        Console.WriteLine("1 - regular cone");
        Console.WriteLine("2 - special cone");
        Console.WriteLine("3 - box");
        userInput = Int32.Parse(Console.ReadLine());
        create_an_order.flavours(ref fDict, ref iceCreamBallsNumber);

        switch (userInput)
        {
            case 1:
                package = 1;
                create_an_order.toppings_for_regular(ref fDict, ref iceCreamBallsNumber, ref toppingsArraylist);
                break;
            case 2:
                package = 2;
                create_an_order.toppings_for_special(ref fDict, ref toppingsArraylist);
                break;
            case 3:
                package = 3;
                create_an_order.toppings_for_box(ref fDict, ref toppingsArraylist);
                break;
        }

        Console.WriteLine("Please choose a task:");
        Console.WriteLine("1 - Pay (After payment, the order cannot be canceled)");
        Console.WriteLine("2 - Edit the order");
        Console.WriteLine("3 - Cancel the order and exit");
        userInput = Int32.Parse(Console.ReadLine());

        switch (userInput)
        {
            case 1:
                Sale s = new Sale(DateTime.Now, price);
                MySqlAccess.MySqlAccess.insertObject_Sale(s);
                Console.WriteLine("The order price is " + price + " nis. Thank you!");
                Console.WriteLine("1 - Check the bill");
                Console.WriteLine("2 - New Order");
                Console.WriteLine("3 - exit");
                userInput = Int32.Parse(Console.ReadLine());
                switch (userInput)
                {
                    case 1:
                        BusinessLogic.Logic.fillTableOrder(ref toppingsArraylist, ref fDict, package);
                        edit.bill();
                        break;
                    case 2:
                        goto NEW_ORDER;
                    case 3:
                        Console.WriteLine("Thanks! Hope to see you next time");
                        System.Environment.Exit(0);
                        break;
                }
                break;
            case 2:
                edit.delete();
                goto NEW_ORDER;
                break;
            case 3:
                edit.delete();
                Console.WriteLine("Thank you for your time");
                System.Environment.Exit(0);
                break;
        }
        break;


    case 2:
        Console.WriteLine("Thank you for your time");
        System.Environment.Exit(0);
        break;
}

