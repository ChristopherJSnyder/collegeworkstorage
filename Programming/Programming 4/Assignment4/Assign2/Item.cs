using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assign4
{
    public class Item : IComparable<Item>
    {
        public string Name { get; set; }
        public double GoldPieces { get; set; }
        public double Weight { get; set; }


        public Item(string name, int value, double weight)
        {
            this.Name = name;
            this.GoldPieces = value;
            this.Weight = weight;
        }



        public int CompareTo(Item other)
        {
            return this.Name.CompareTo(other.Name);
        }


        public override bool Equals(object obj)
        {
            Item item;

            try
            {
                item = (Item)obj;
            }
            catch (Exception)
            {
                return false;
            }


            if(this == obj)
            {
                return true;
            }

            else if(obj == null)
            {
                return false;
            }
          
            return this.Name == item.Name && this.Weight == item.Weight && this.GoldPieces == item.GoldPieces;
        }


        public override string ToString()
        {
            return Name + " is worth " + GoldPieces + "gp and weighs " + Weight + "kg";
        }
    }
}
