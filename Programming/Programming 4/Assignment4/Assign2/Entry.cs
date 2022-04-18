using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assign4
{
   public class Entry <K, V>
    {
        public V Value { get; set; }
        public K Key { get; set; }


        public Entry (K key, V value)
        {
            this.Value = value;
            this.Key = key;
        }


    }
}
