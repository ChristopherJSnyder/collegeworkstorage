using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assign4
{
    public class HashMap<K, V> : Map<K, V>
    {
        public K Key;
        public V Value;
        public int CAPACITY = 11;
        public int Length { get; set; }
        public double LoadFactor;
        public int threshold;
        //public List<K> keys { get; set; }
        //public List<V> values { get; set; }
        public Entry<K,V>[] Table { get; set; }
        public int KeyLength { get; set; }

    
        public const int DEFAULT_CAPACITY = 11;
        public const double DEFAULT_LOADFACTOR = 0.75;

        //public HashMap()
        //{
        //    this.LoadFactor = DEFAULT_LOADFACTOR;
        //    threshold = (int)(DEFAULT_CAPACITY * DEFAULT_LOADFACTOR);
        //    //values = new List<V>();
        //    //keys = new List<K>();

        //    Table = new Entry<K, V>[DEFAULT_CAPACITY];
        //}


        //public HashMap(int size)
        //{
        //    if(size == null || size <= 0)
        //    {
        //        throw new ArgumentException();
        //    }
        //    this.LoadFactor = DEFAULT_LOADFACTOR;
        //    threshold = (int)(size * LoadFactor);
        //    //values = new List<V>();
        //    //keys = new List<K>();
        //    CAPACITY = size;
        //    Table = new Entry<K, V>[size];
        //}


        public HashMap(int size = DEFAULT_CAPACITY, double loadFactor = DEFAULT_LOADFACTOR)
        {
            if (size <= 0 || loadFactor <= 0)
            {
                throw new ArgumentException();
            } 

            this.LoadFactor = loadFactor;
            threshold = (int)(size * loadFactor);
            //values = new List<V>();
            //keys = new List<K>();
            CAPACITY = size;
            Table = new Entry<K, V>[size];
        }


        public bool IsEmpty()
        {
            if (Length == 0)
            {
                return true;
            }

            else
            {
                return false;
            }
        }


        public void Clear()
        {
            this.Length = 0;
        }


        public V Get (K key)
        {
           if (IsEmpty())
            {
                return default(V);
            }

            int bucket = GetMatchingOrNextAvailableBucket(key);
            Entry<K, V> entry = Table[bucket];


            if (entry == null)
            {
                return default(V);
            }

            //    for (int i = 0; i < CAPACITY; i++)
            //    {
            //        if (Table[i] != null)
            //        {
            //            if (Table[i].Key.Equals(key))
            //            {
            //                return Table[i].Value;
            //            }
            //        }
            //    }
            //}

            //if (entry.GetHashCode() != key.GetHashCode())
            //{
            //    for (int i = 0; i < CAPACITY; i++)
            //    {
            //        if (Table[i] != null)
            //        {
            //           if (Table[i].Key.GetHashCode() == key.GetHashCode())
            //            {
            //                return Table[i].Value;
            //            }
            //        }
            //    }
            //}
            return entry.Value;
        }

        
        public V Put(K key, V value)
        {
            if(key == null || value == null)
            {
                throw new ArgumentNullException();
            }
            Entry<K, V> newEntry = new Entry<K, V>(key, value);
            
            if (threshold <= Length + 1 || threshold <= KeyLength + 1)
            {
                Rehash();
            }
            int bucket = GetMatchingOrNextAvailableBucket(key);

            if (Table[bucket] == null)
            {
                // No Collisions
                Table[bucket] = newEntry;
            }
            else
            {
                if (Table[bucket].Key.Equals(key))
                {
                    //Same Key Updating Value and Returning old one
                    V result = Table[bucket].Value;
                    Table[bucket].Value = value;
                    return result;
                }
                else
                {
                    Table[bucket] = newEntry;
                    //Collison
                    //Probe(newEntry);
                }
            }
            
            Length++;
            KeyLength++;
            //keys.Add(key);
            //values.Add(value);
            
            return default(V);
        }


        public V Remove(K key)
        { 
            if (key == null)
            {
                throw new ArgumentNullException();
            }

            if (IsEmpty())
            {
                return default(V);
            }


            int bucket = GetMatchingOrNextAvailableBucket(key);
            Entry<K, V> entry = Table[bucket];
            V oldvalue = entry.Value;
            entry.Value = default(V);

            Length--;

            return oldvalue;
        }

        public int Size()
        {
            return this.Length;
        }

        public IEnumerator<K> Keys()
        {
            List<K> keys = new List<K>();

            foreach (var Entry in Table)
            {
                if (Entry != null && Entry.Key != null)
                {
                    keys.Add(Entry.Key);
                }
            }

            return keys.GetEnumerator();
        }

        public IEnumerator<V> Values()
        {
            List<V> values = new List<V>();

            foreach (var Entry in Table)
            {
                if (Entry != null && Entry.Value != null)
                {
                    values.Add(Entry.Value);
                }
            }

            return values.GetEnumerator();
        }

        public void Rehash()
        {
            int newSize = Resize();
            Entry<K, V>[] oldtable = Table.Where(x => x != null && x.Value != null).ToArray();
            Table = new Entry<K, V>[newSize];
            CAPACITY = newSize;
            Length = 0;
            threshold = (int)(CAPACITY * LoadFactor);
            //values = new List<V>();
            //keys = new List<K>();

            for (int i = 0; i < oldtable.Length; i++)
            {
                Put(oldtable[i].Key, oldtable[i].Value);
            }

        }

        public int Resize()
        {
            int newSize = (CAPACITY * 2) + 1;
            int square = 0;
            bool found = false;

            while (found == false)
            {
                square = (int)Math.Sqrt(newSize);

                for (int i = 3; i <= square; i++)
                {
                    if (newSize % i == 0)
                    {
                        break;
                    }

                    if (i == square)
                    {
                        return newSize;
                    }
                }
                newSize++;
            }


            return newSize;
        }


        public void Probe(Entry<K, V> entry)
        {
            Boolean placed = false;
            for ( int i = entry.Key.GetHashCode() % CAPACITY; i < CAPACITY; i++)
            {
                if (Table[i] == null)
                {
                    Table[i] = entry;
                    placed = true;
                    i = CAPACITY;
                }
            }

            if(!placed)
            {
                for (int i = 0; i < CAPACITY; i++)
                {
                    if (Table[i] == null)
                    {
                        Table[i] = entry;
                        placed = true;
                        i = CAPACITY;
                    }
                }
            }
        }

        public int GetMatchingOrNextAvailableBucket(K key)
        {
            int result = 0;
            Boolean placed = false;
            for (int i = key.GetHashCode() % CAPACITY; i < CAPACITY && !placed; i++)
            {
                if (Table[i] == null || Table[i].Key.Equals(key))
                {
                    placed = true;
                    result = i;
                }
            }

            if (!placed)
            {
                for (int i = 0; i < CAPACITY && !placed; i++)
                {
                    if (Table[i] == null || Table[i].Key.Equals(key))
                    {
                        result = i;
                        placed = true;
                    }
                }
            }
            return result;
        }
    }

    //strinkey key = new stringkey (itemname)
    // Item item = new item ("axe", 10, 5.2")
    // Hasmap <stringkey, item> map = []
    // map.put(key, item)
}
