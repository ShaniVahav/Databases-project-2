using System.Collections;

using BusinessEntities;


namespace BusinessLogic
{
  
    public class Logic
    {
          public static ArrayList fillTableIN()
        {
            ArrayList a = new ArrayList { "choclate" , "vanila" , "mecupelet",
                                  "Banana" ,
                                   "orange",
                                  "coconut" ,
                                  "Oreo" ,
                                  "coffee" ,
                                  "strawberry",
                                  "mango" };
            
            return a;
        }

        public static void createTables()
        {
            MySqlAccess.MySqlAccess.createTables();
        }

        public static void fillTableOrder(ref ArrayList toppings, int round_number, ref Dictionary<int, int> fdict, int package)
        {   
            iceCreamOrder a = new(package, fdict, toppings);
            MySqlAccess.MySqlAccess.insertObjectToOrders(a, round_number);
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
                                  "1 - chocolate\n" +
                                  "2 - vanilla\n" +
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
            Dictionary<string , int> Tdict= new Dictionary<string, int>(); 
           Tdict.Add("chocolate",14);
           Tdict.Add("peanuts",15);
           Tdict.Add("maple",16);
                if (number < 2)
                    return;
            removeToppings(ref fDict, ref Tdict, ref toppingsArraylist);
        }
        public static void removeToppings(ref Dictionary<int, int> fDict, ref Dictionary<string,int> Tdict,ref ArrayList toppingsArraylist)
        {
            if (fDict[3] > 0 || fDict[1] > 0)
                Tdict.Remove("chocolate");
            if (fDict[2] > 0)
                Tdict.Remove("maple");
            foreach (var item in Tdict) { 
                string key = item.Key;
                int value = item.Value;
                addingCheak(key,value, ref toppingsArraylist);
            }
        }
    public static void addingCheak(string key , int value , ref ArrayList toppingsArraylist)
            {
                Console.WriteLine("press 1 - to add " + key + ", 0 otherwise");
                int userInput = Int32.Parse(Console.ReadLine());
                if (userInput == 1)
                    toppingsArraylist.Add(value);
                Console.WriteLine(toppingsArraylist.ToString());
            } 
             
            
        public static void toppings_for_special(ref Dictionary<int, int> fDict, ref ArrayList toppingsArraylist)
        {
             Dictionary<string , int> Tdict= new Dictionary<string, int>(); 
            Tdict.Add("chocolate",14);
           Tdict.Add("peanuts",15);
           Tdict.Add("maple",16);
            removeToppings(ref fDict, ref Tdict, ref toppingsArraylist);
        }
            public static void toppings_for_box(ref Dictionary<int, int> fDict, ref ArrayList toppingsArraylist)
            {
                toppings_for_special(ref fDict, ref toppingsArraylist);
            }

        }


        public class edit
        {
            public static void delete()
            {
                Console.WriteLine("The order canceled.");
            }
        
            public static void bill(DateTime date, int price)
            {
                Console.WriteLine("Date: " + date);
                Console.WriteLine("Price: " + price + " nis");
                Console.WriteLine("Thank you! Hope to see you next time");
            }
        }
}
    
