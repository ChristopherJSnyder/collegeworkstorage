using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assign4
{
    public class StringKey : IComparable<StringKey>
    {
        public string KeyName;

        public StringKey(string keyname)
        {
            this.KeyName = keyname;
        }

        public int CompareTo(StringKey other)
        {
            return this.KeyName.CompareTo(other.KeyName);
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }

            StringKey stringkey = obj as StringKey;

            if (stringkey == null)
            {
                return false;
            }
            
            return this.KeyName.Equals(stringkey.KeyName);
        }



        public override string ToString()
        {
            return "KeyName: " + KeyName + " HashCode: " + GetHashCode();
        }


        public override int GetHashCode()
        {
            int hash = 0;
            byte[] ascii = Encoding.ASCII.GetBytes(KeyName);

            for (int i = 0; i < ascii.Count(); i++)
            {
                int power = IntPower(31, i);

                hash += ascii[i] * power;
            }

            return Math.Abs(hash);

        }

        /// <summary>
        /// CRANKING
        /// </summary>
        /// <param name="baseNum"></param>
        /// <param name="power"></param>
        /// <returns></returns>
        private int IntPower(int baseNum, int power)
        {
            int result = 1;

            for (int i = 0; i < power; i++)
            {
                result *= baseNum;
            }
            return result;
        }
    }
}
