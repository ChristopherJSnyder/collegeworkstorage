using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assign2
{
    public class Stack<T>
    {
        public Node<T> Head;
        public int size;

        /// <summary>
        /// Initialize a new Stack with no head and size 0
        /// </summary>
        public Stack()
        {
            Head = default(Node<T>);
            size = 0;
        }


        /// <summary>
        /// Create a new node and add it to the top of the stack
        /// </summary>
        /// <param name="element">Any datatype to add to a stack</param>
        public void Push(T element)
        {
            Node<T> newnode = new Node<T>(element);

            Node<T> Oldhead = Head;

            if (size == 0)
            {
                Head = newnode;
            }

            else
            {
                Head = newnode;
                Head.Previous = Oldhead;
                
            }

            size++;
        }

        /// <summary>
        /// Retrieve the Head from a stack without touching it
        /// </summary>
        /// <returns>The top item in the stack</returns>
        public T Top()
        {
            if (size == 0)
            {
                throw new ApplicationException("List is empty.");
            }
            return Head.Element;
        }


        /// <summary>
        /// Return the Head from a stack whilst removing it
        /// </summary>
        /// <returns>The removed Head item</returns>
        public T Pop()
        {
            if (size == 0)
            {
                throw new ApplicationException("Cannot remove from an empty list");
            }
            T elementtoreturn = Head.Element;
            Head = Head.Previous;
            //Head.Previous = Head.Previous.Previous;
            size--;
            return elementtoreturn;
        }


        /// <summary>
        /// Fully wipes the stack
        /// </summary>
        public void Clear()
        {
            size = 0;
            Head = null;
        }


        /// <summary>
        /// Returns the number of items inside the stack.
        /// </summary>
        /// <returns>Size of stack</returns>
        public int Size()
        {
            return size;
        }


        /// <summary>
        /// Returns the head node - not just the element inside
        /// </summary>
        /// <returns>Head node</returns>
        public Node<T> getHead()
        {
            return Head;
        }

        /// <summary>
        /// Returns if any given stack is empty.
        /// </summary>
        /// <returns>True if empty, False if not</returns>
        public bool IsEmpty()
        {
            if (size == 0)
            {
                return true;
            }

            else
            {
                return false;
            }
        }




    }

}
