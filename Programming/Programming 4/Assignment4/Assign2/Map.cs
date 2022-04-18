using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assign4
{
    public interface Map<K, V>
        
    {

        int Size();

        bool IsEmpty();

        void Clear();

        V Get(K Key);

        V Put(K key, V value);

        V Remove(K key);

        IEnumerator<K> Keys();

        IEnumerator<V> Values();
        
    }
}
