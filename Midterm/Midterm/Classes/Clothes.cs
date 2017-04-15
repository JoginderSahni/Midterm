using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterm
{
    public class Clothes
    {
        public Clothes()
        { }
        public Clothes(ClothesType type, double price, int time)
        {
            this.Price = price;
            this.Type = type;
            this.CompleteTime = time;
        }
        
        public ClothesType Type { get; set; }
        public double Price { get; set; }
        public int CompleteTime { get; set;}

    }
   
}
