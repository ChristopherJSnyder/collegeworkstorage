using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment2
{
    public class Node<T>
    {
        public T Element { get; set; }
        public Node<T> Previous { get; set; }
        

        public Node()
        {
            this.Element = default(T);
            this.Previous = default(Node<T>);
            
        }

        public Node(T Element)
        {
            this.Element = Element;

        }

        public Node(T Element, Node<T> Previous, Node<T> Next)
        {

            this.Element = Element;
            this.Previous = Previous;
        }
    }
}
