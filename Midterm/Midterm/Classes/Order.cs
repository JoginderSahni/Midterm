using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Midterm.Classes;

namespace Midterm
{
    class Order
    {
        public List<Clothes> Items { get; set; }
        public DateTime DropOff { get; }   
        public Person Customer { get; set; }
        public PaymentType Payment { get; set; }
        private bool isPaid = false;
        public bool IsPaid { get; }
        public int OrderNumber { get; set; }

        public Order()
        {
            DropOff = DateTime.Now;
        }
        public double GetTotal()
        {
            double t = 0;
            foreach (Clothes c in Items)
            {
                t += c.Price;
            }
            return (Math.Round((t*1.06),2));
        }
        public DateTime PickupTime()
        {
            int maxDays = 0;
            foreach (Clothes article in Items)
            {
                if (article.CompleteTime > maxDays)
                {
                    maxDays = article.CompleteTime;
                }
            }
            DateTime pickup = DropOff.AddDays(maxDays);
            return pickup;
        }
        public void MakePayment(PaymentType p)
        {
            this.Payment = p;

            if (p == PaymentType.Cash)
            {
                Console.WriteLine("Please enter the amount tendered.");
                double cashPaid = double.Parse(Console.ReadLine());
                if (cashPaid > this.GetTotal())
                {
                    Console.WriteLine($"Thanks for your cash payment. Your change is {cashPaid - this.GetTotal()}");
                    isPaid = true;
                }
                else if (cashPaid == this.GetTotal())
                {
                    Console.WriteLine("Thanks for your cash payment. You have no change for this order.");
                    isPaid = true;
                }
                else
                    Console.WriteLine("You don't have enough money. Get out of our store.");
            }
            if (p == PaymentType.Check)
            {
                Console.WriteLine("Please enter your check number: ");
                Console.ReadLine();
                Console.WriteLine("Thanks for your payment!");
                isPaid = true;
            }
            if (p == PaymentType.Card)
            {
                Console.Write("Please enter your card number: ");
                Console.ReadLine();
                Console.Write("Please enter the expiration date: ");
                Console.ReadLine();
                Console.Write("Please enter the 3 digit CVV in the back: ");
                Console.ReadLine();
                Console.WriteLine("Thanks for making your payment!");
                isPaid = true;
            }
        }      
        public void DisplayRecept()
        {
            Console.WriteLine("RECEIPT \r\n");
            Console.WriteLine($"Order Number: {OrderNumber}");
            Console.WriteLine($"Total Number of Items: {Items.Count}");
            Console.WriteLine("Items You Have Dropped Off:");
            foreach(Clothes article in Items)
            {
                Console.WriteLine($"{article.Type} - {article.Price}");
            }
            Console.WriteLine($"Subtotal - {Math.Round(this.GetTotal() / 1.06, 2)}");
            Console.WriteLine($"Tax - {this.GetTotal() - (Math.Round(this.GetTotal() / 1.06, 2))}");
            Console.WriteLine($"Grand Total - {this.GetTotal()}");
            Console.WriteLine($"Payment method used - {Payment.ToString()}");
            Console.WriteLine($"Your order will be ready at {this.PickupTime()}");
            Console.WriteLine("Thank you for using Team2Beat Dry Cleaners!");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
