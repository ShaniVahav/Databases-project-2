using System.Collections;

using BusinessEntities;


namespace BusinessLogic
{
    public class Logic
    {

        public static void createTables()
        {
            MySqlAccess.MySqlAccess.createTables();
        }

        public static void fillTableOrder(ref ArrayList toppings, ref Dictionary<int, int> fdict, int package)
        {   
            iceCreamOrder a = new(package, fdict, toppings);
            MySqlAccess.MySqlAccess.insertObjectToOrders(a);
        }
    }


        // we add:
        public class create_an_order
        {

            public static void flavours(ref Dictionary<int, int> fDict, ref int iceCreamBallsNumber)
            {
                int max_amount_of_balls = 3;
                int current_amount = 0;
                while (current_amount < max_amount_of_balls)
                {

                    Console.WriteLine("choose between 1-3 balls");
                    Console.WriteLine("0- forward with the order\n" +
                                  "1 - choclate\n" +
                                  "2 - vanila\n" +
                                  "3 - mecupelet\n" +
                                  "4 - Banana\n" +
                                  "5 - orange\n" +
                                  "6 - coconut\n" +
                                  "7 - Oreo\n" +
                                  "8 - coffee\n" +
                                  "9 - strawberry\n" +
                                  "10 - mango\n");
                    int userInput = Int32.Parse(Console.ReadLine());
                    if (userInput == 0)
                        break;
                    fDict[userInput]++;
                    current_amount++;
                    iceCreamBallsNumber++;
                }
            }

            public static void toppings_for_regular(ref Dictionary<int, int> fDict, ref int number, ref ArrayList toppingsArraylist)
            {
            ArrayList toppings = new ArrayList { "peanuts", "chocolate", "maple" };
                if (number < 2)
                    return;
            removeToppings(ref fDict, ref toppings, ref toppingsArraylist);
        }
        public static void removeToppings(ref Dictionary<int, int> fDict, ref ArrayList toppings, ref ArrayList toppingsArraylist)
        {
            if (fDict[3] > 0 || fDict[1] > 0)
                toppings.Remove("chocolate");
            if (fDict[2] > 0)
                toppings.Remove("maple");
            foreach (var item in toppings)
            {
                addingCheak((string)item, ref toppingsArraylist);
            }
        }
    public static void addingCheak(string topping, ref ArrayList toppingsArraylist)
            {
                Console.WriteLine("press 1 - to add " + topping + ", 0 otherwise");
                int userInput = Int32.Parse(Console.ReadLine());
                if (userInput == 1)
                    toppingsArraylist.Add(topping);
            }

        public static void toppings_for_special(ref Dictionary<int, int> fDict, ref ArrayList toppingsArraylist)
        {
            ArrayList toppings = new ArrayList { "peanuts", "chocolate", "maple" };
            removeToppings(ref fDict, ref toppings, ref toppingsArraylist);
        }
            public static void toppings_for_box(ref Dictionary<int, int> fDict, ref ArrayList toppingsArraylist)
            {
                ArrayList toppings = new ArrayList { "peanuts", "chocolate", "maple" };
                removeToppings(ref fDict, ref toppings, ref toppingsArraylist);
        }

        }


        public class edit
        {
            public static void delete()
            {
                Console.WriteLine("The order canceled.");
            }
        
            public static void bill()
            {
                Console.WriteLine("bill");
            }
        }
}
    
