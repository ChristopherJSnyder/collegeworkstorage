using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assign3
{
    public class Node<T>
    {
        public T Element { get; set; }
        public Node<T> Next { get; set; }
        public Node<T> Previous { get; set; }
        public Node()
        {
            this.Element = default(T);
            this.Next = default(Node<T>);
            this.Previous = default(Node<T>);
        }

        public Node(T Element)
        {
            this.Element = Element;

        }

        public Node(T Element, Node<T> Next)
        {

            this.Element = Element;
            this.Next = Next;
            this.Previous = Previous;

        }
    }


}






