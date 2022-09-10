﻿using System;
using System.Data;
using System.Diagnostics;//used for Stopwatch class

using MySql.Data;
using MySql.Data.MySqlClient;

using MySqlAccess;
using BusinessLogic;
using System.Collections;
using BusinessEntities;

// See https://aka.ms/new-console-template for more information

Console.WriteLine("Please create tables first, by pressing '1'");

Stopwatch stopwatch = new Stopwatch();

int userInput = 0;
int price = 0;
int round_number = 0;

int package = -1;
var toppingsArraylist = new ArrayList();
int iceCreamBallsNumber = 0;                                    /// משתנה של כמות הכדורים 
var fDict = new Dictionary<int, int>();  ///// פה אני יוצר את המילון 
for (int i = 0; i < 10; i++)
{
    fDict.Add(i, 0);
}

Console.WriteLine("1 - create tables");
userInput = Int32.Parse(Console.ReadLine());


if (userInput == 1)
    Logic.createTables();

NEW_ORDER:
round_number = 0;
price = 0;
package = -1;
toppingsArraylist.Clear();
iceCreamBallsNumber = 0;
Console.WriteLine("Hi! Welcome to our Ice Cream shop");
Console.WriteLine("Please choose a task:");
Console.WriteLine("1 - order an ice cream");
Console.WriteLine("2 - exit");
userInput = Int32.Parse(Console.ReadLine());

if (userInput == 2)
{
    System.Environment.Exit(0);
}


// create a sale
Sale s = new Sale(DateTime.Now, price);
MySqlAccess.MySqlAccess.insertObject_Sale(s);

    
ANOTHER_ORDER:
round_number++;
package = -1;
toppingsArraylist.Clear();
iceCreamBallsNumber = 0;                                    /// משתנה של כמות הכדורים   ///// פה אני יוצר את המילון 
for (int i = 0; i < 10; i++)
{
    fDict[i] = 0;
}

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

// calculate the price:
if (package == 1)
{
    switch(iceCreamBallsNumber)
    {
        case 1:
            price += 7;
            break;
        case 2:
            price += 12;
            break;
        case 3:
            price += 18;
            break;
    }
}

if (package == 2)
{
    switch(iceCreamBallsNumber)
    {
        case 1:
            price += 9;
            break;
        case 2:
            price += 14;
            break;
        case 3:
            price += 20;
            break;
    }
}

if (package == 3)
{
    switch(iceCreamBallsNumber)
    {
        case 1:
            price += 12;
            break;
        case 2:
            price += 17;
            break;
        case 3:
            price += 23;
            break;
        DEFAULT:
            price += 23 + (iceCreamBallsNumber-3)*6;
    }
}
price += toppingsArraylist.Count*2;

Console.WriteLine("Please choose a task:");
Console.WriteLine("1 - Pay (After payment, the order cannot be canceled)");
Console.WriteLine("2 - Edit the order");
Console.WriteLine("3 - Delete");
Console.WriteLine("4 - Add another order");
userInput = Int32.Parse(Console.ReadLine());

    switch (userInput)
    {
        case 1:  // chose to pay
            BusinessLogic.Logic.fillTableOrder(ref toppingsArraylist, round_number, ref fDict, package);
            
            // update the price
            MySqlAccess.MySqlAccess.update_price(price);
            Console.WriteLine("The order price is " + price + " nis. Thank you!");
            Console.WriteLine("Please choose a task:");
            Console.WriteLine("1 - Check the bill");
            Console.WriteLine("2 - New Order");
            Console.WriteLine("3 - exit");
            userInput = Int32.Parse(Console.ReadLine());
            switch (userInput)
            {
                case 1:
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
            goto NEW_ORDER;
            break;

        case 4:
            goto ANOTHER_ORDER;
            break;
    }
