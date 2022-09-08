using System.Collections;

using BusinessEntities;


namespace BusinessLogic
{
    public class Logic
    {

        static string[] cars = { "Toyota Prius+", "Hundai i30", "Volvo sx40", "Hundai Ioniq" , "Mazda 5", "Mazda 2", "Mazda Mpv" };
        static string[] colors = { "Yellow", "White", "Black", "Green", "Transparent" };
        static string[] tasks = { "Service 10K", "Wheels", "BodyWork" };
        static string[] desc = { "Replace filets of oil,gasoline,air contidioner", "Change 4 tires", "fix scratchs" };

        static string[] names = { "Arkady", "Aharon", "Zeev", "Yehonatan" };
        static string[] cities = { "Jerusalem", "Tel Aviv", "Beeh Sheva", "Ariel" };

        public static void createTables()
        {
            MySqlAccess.MySqlAccess.createTables();
        }
        public static void fillTables(int num)
        {
            Random r = new Random();

            //generate random values for owners
            for (int i = 0; i < num; i++)
            {
                int rName = r.Next(0, names.Length);
                int rPhone = r.Next(0, 1000);
                int rCity = r.Next(0, names.Length);

                Owner o = new Owner(names[rName], "" + rPhone, cities[rCity]);
                MySqlAccess.MySqlAccess.insertObject(o);
            }

            //generate random values for vehicles
            for (int i = 0; i < num; i++)
            {
                int rCar = r.Next(0, cars.Length);
                int rColor = r.Next(0, colors.Length);
                int rYear = r.Next(1980, 2040);

                Vehicle o = new Vehicle(cars[rCar], colors[rColor], rYear);
                MySqlAccess.MySqlAccess.insertObject(o);
            }

            //generate random values for tasks
            for (int i = 0; i < num; i++)
            {
                int rTask = r.Next(0, tasks.Length);
                int rColor = r.Next(0, colors.Length);
                int rPrice = r.Next(100, 5000);

                VTask o = new VTask(tasks[rTask], desc[rTask], rPrice);
                MySqlAccess.MySqlAccess.insertObject(o);
            }

            //generate random values for vown
            for (int i = 0; i < num; i++)
            {
                int rIdOwner = r.Next(0, num);
                int rIdViecle = r.Next(0, num);
                VOwn o = new VOwn(rIdOwner,rIdViecle);
                MySqlAccess.MySqlAccess.insertObject(o);
            }

             //generate random values for orders
            for (int i = 0; i < num; i++)
            {
                int rIdViecle = r.Next(0, num);
                int rIdTask = r.Next(0, num);

                //generate date string
                int rYear = r.Next(1990, 2020);
                int rMonth = r.Next(1, 13);
                int rDate = r.Next(1, 29);
                string orderDate = "" + rYear + "-" + rMonth + "-"+rDate;

                //generate date string               
                rDate = rDate + r.Next(1, 29);
                if (rDate > 29){
                    rDate = rDate % 29;
                    rMonth = rMonth + 1;
                }

                if (rMonth > 12){
                    rMonth = 1;
                    rYear = rYear + 1;
                }


                string completedDate = "" + rYear + "-" + rMonth + "-"+rDate;

                int rCompleted = r.Next(0, 2);
                int rPayed = r.Next(0, 2);
                
                Order o = new Order(rIdViecle,rIdTask,orderDate,completedDate,rCompleted,rPayed);
                MySqlAccess.MySqlAccess.insertObject(o);
            }

        }

        public static ArrayList getTableData(string tableName)
        {
            ArrayList all = MySqlAccess.MySqlAccess.readAll(tableName);
            ArrayList results = new ArrayList();

            if (tableName == "Owners")
            {
                foreach (Object[] row in all)
                {
                    Owner o = new Owner((string)row[1], ""+row[2], (string)row[3]);
                    results.Add(o);
                }
            }

            if (tableName == "Tasks")
            {
                foreach (Object[] row in all)
                {
                    VTask o = new VTask((string)row[1], (string)row[2], (int)row[3]);
                    results.Add(o);
                }
            }

            if (tableName == "Vehicles")
            {
                foreach (Object[] row in all)
                {
                    Vehicle o = new Vehicle((string)row[1], (string)row[2], (int)row[3]);
                    results.Add(o);
                }
            }

            return results;
        }

    }

    // we add:
     public class create_an_order
    {

        public static void flavours(ref price, ref Dictionary<int, int> fDict, ref int iceCreamBallsNumber)
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
            ///////// ADDED /////////
            switch(current_amount)
            {
                case 1:
                    price = 7;
                    break;
                case 2:
                    price = 12;
                    break;
                case 3:
                    price = 18;
                    break;
            }
            ///////// ADDED /////////
        }

        public static void toppings_for_regular(ref Dictionary<int, int> fDict, ref int number, ref ArrayList toppingsArraylist)
        {
            if (number < 2)
                return;
            addingCheak("peanuts", ref toppingsArraylist);
            if (fDict[3] > 0 || fDict[1] > 0)
                addingCheak("chocolate", ref toppingsArraylist);
            if (fDict[2] > 0)
                addingCheak("maple", ref toppingsArraylist);
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
             addingCheak("peanuts", ref toppingsArraylist);
            if (fDict[3] > 0 || fDict[1] > 0)
                addingCheak("chocolate", ref toppingsArraylist);
            if (fDict[2] > 0)
                addingCheak("maple", ref toppingsArraylist);
        }

         public static void toppings_for_box(ref Dictionary<int, int> fDict, ref ArrayList toppingsArraylist)
        {
             addingCheak("peanuts", ref toppingsArraylist);
            if (fDict[3] > 0 || fDict[1] > 0)
                addingCheak("chocolate", ref toppingsArraylist);
            if (fDict[2] > 0)
                addingCheak("maple", ref toppingsArraylist);
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