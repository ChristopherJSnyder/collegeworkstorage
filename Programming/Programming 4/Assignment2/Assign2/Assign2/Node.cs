using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assign2
{
    public class Node<T>
    {
        public T Element { get; set; }
        public Node<T> Previous { get; set; }

        /// <summary>
        /// Create new node
        /// </summary>
        public Node()
        {
            this.Element = default(T);
            this.Previous = default(Node<T>);

        }

        /// <summary>
        /// Create new node with the given element inside
        /// </summary>
        /// <param name="Element">Element to add</param>
        public Node(T Element)
        {
            this.Element = Element;

        }

        /// <summary>
        /// Create a new node with given element inside, and given element, previous, and next nodes
        /// </summary>
        /// <param name="Element">Element to add</param>
        /// <param name="Previous">The node before this one</param>
        /// <param name="Next">The node after this one</param>
        public Node(T Element, Node<T> Previous, Node<T> Next)
        {

            this.Element = Element;
            this.Previous = Previous;
        }
    }
}
