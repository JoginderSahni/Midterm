using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Midterm.Classes;

namespace Midterm
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Order> orderHistory = new List<Order>();
            for  (int i = 10000; ;i++)
            {
                List<Clothes> menu = Program.ImportProducts(); //Imports list from txt and returns "menu" of objects
                Person customer = Program.GreetAndSelectCustomer(); //Imports customer list. Finds previous customer or makes new one, and returns active customer for order
                List<Clothes> orderList = Program.PopulateList(menu); //Returns list of customer objects to go  into order
                Order currentOrder = Program.CreateOrderAndPayAndFinish(orderList, customer);
                currentOrder.OrderNumber = i;
                currentOrder.DisplayRecept();
                orderHistory.Add(currentOrder);
            }
        }
        public static List<Clothes> ImportProducts()
        {
            List<Clothes> menu = new List<Clothes>(); // Default objects, "menu"
            StreamReader list = null;
            try
            {
                list = new StreamReader(new FileStream(@"C:\Users\jsahni\Desktop\Bootcamp\Midterm\Prices.txt", FileMode.Open, FileAccess.Read));
                
                string[] split = new string[3];
                while (list.Peek() != -1)
                {
                    string line = list.ReadLine();
                    split = line.Split('|');
                    ClothesType currentype;
                    Enum.TryParse(split[0], out currentype);
                    Clothes article = new Clothes(currentype, double.Parse(split[1]), int.Parse(split[2]));
                    menu.Add(article);
                }
            }
            catch (IOException)
            {
                Console.Write("IO Exception.");
            }
            finally
            {
                if (list != null)
                {
                    list.Close();
                }
            }
            return menu;
        }
        public static Person GreetAndSelectCustomer()
        {
            List<Person> customers = new List<Person>();
            FileStream fs = new FileStream(@"C:\Users\jsahni\Desktop\Bootcamp\Midterm\Customers.txt", FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr;
            StreamWriter sw;           
            double userPhoneNumber;
            bool existingCustomer = false;
            Person customerHolder = new Person();
            try
            {
                sr = new StreamReader(fs);
                string[] split = new string[3];
                while (sr.Peek() != -1)
                {
                    string line = sr.ReadLine();
                    split = line.Split('|');
                    Person customer = new Person(split[0], split[1], double.Parse(split[2]));
                    customers.Add(customer);
                }
                Console.WriteLine("Welcome to Team2Beat Dry Cleaners!");
                Console.WriteLine("Please enter your 10-digit phone number.");
                userPhoneNumber = double.Parse(Console.ReadLine());
                foreach (Person customer in customers)
                {
                    if (customer.PhoneNumber == userPhoneNumber)
                    {
                        existingCustomer = true;
                        Console.WriteLine($"Welcome back, {customer.FirstName} {customer.LastName}.");
                        customerHolder = customer;
                        break;
                    }
                }
                if (existingCustomer == false)
                {
                    Console.WriteLine("It appears that you are a new customer! To better serve you, we'll create an account for you. Please enter your first name: ");
                    string uFName = Console.ReadLine();
                    Console.WriteLine("Please enter your last name: ");
                    string uLName = Console.ReadLine();
                    Console.WriteLine("Thanks, we've created your account!");
                    sw = new StreamWriter(fs);
                    sw.WriteLine($"{uFName}|{uLName}|{userPhoneNumber}");
                    sw.Flush();
                    customerHolder = new Person(uFName, uLName, userPhoneNumber);
                }

                return customerHolder;
                
            }
            catch (IOException)
            {
                Console.Write("IO Exception.");
            }
            finally
            {
                fs.Close();
            }
            return customerHolder;
        }
        public static List<Clothes> PopulateList(List<Clothes> menu)
        {
            string yn = "yes";
            List<Clothes> order = new List<Clothes>();
            do
            {
                Console.WriteLine("Please type in the number of the item you wish to clean.");
                Console.WriteLine("1: Suit Jackets");
                Console.WriteLine("2: Pants");
                Console.WriteLine("3: Ties");
                Console.WriteLine("4: Dress Shirts");
                Console.WriteLine("5: Sweaters");
                Console.WriteLine("6: Dresses");
                Console.WriteLine("7: Blankets");
                Console.WriteLine("8: Wedding Gowns");
                Console.WriteLine("9: Curtains and Drapes");
                string uInput = Console.ReadLine();
                int uInt;
                int numOfClothes;
                bool isint = int.TryParse(uInput, out uInt);
                if (isint == false)
                {
                    Console.WriteLine("Sorry, I didnt catch that.");
                }
                else
                {
                    Console.WriteLine($"Great, how many {(ClothesType)(uInt - 1)} did you want to have cleaned?");
                    string numHolder = Console.ReadLine();
                    bool innerIsint = int.TryParse(numHolder, out numOfClothes);
                    if (innerIsint == false)
                    {
                        Console.WriteLine("Sorry, I didnt catch that.");
                    }
                    else
                    {
                        Console.WriteLine($"OK, I'll add {numOfClothes} to your order.");
                        for (int i = 0; i < numOfClothes; i++)
                        {

                            Clothes item = new Clothes(
                                menu[uInt - 1].Type,
                                menu[uInt - 1].Price,
                                menu[uInt - 1].CompleteTime
                                );
                            order.Add(item);
                        }
                    }

                }
                Console.WriteLine("Did you want to add more to your order? y/n");
                yn = Console.ReadLine().ToLower();
                if ((yn == "yes") || (yn == "y"))
                    Console.Clear();
                else if ((yn == "no") || (yn == "n"))
                {
                    Console.WriteLine($"Your order contains {order.Count} items.");
                    Console.Write("Do you want to finalize your order and continue to payment? (y/n): ");
                    string innerYN = Console.ReadLine().ToLower();
                    if ((innerYN == "no")||(innerYN == "n"))
                    {
                        yn = "yes";
                    }
                }

            }
            while ((yn == "yes") || (yn == "y"));
            return order;
        }
        public static Order CreateOrderAndPayAndFinish(List<Clothes> list, Person customer)
        {
            Order order = new Order();
            order.Customer = customer;
            order.Items = list;
            Console.WriteLine($"The total for your order is ${order.GetTotal()} with tax. How would you like to pay for it?");
            Console.WriteLine("Enter Cash, Credit, or Check");
            string s = Console.ReadLine().ToLower();
            PaymentType p = PaymentParse.Parse(s);
            order.MakePayment(p);
            System.Threading.Thread.Sleep(3500);
            Console.Clear();
            return order;                                                                                                                                   
        }
    } 
}