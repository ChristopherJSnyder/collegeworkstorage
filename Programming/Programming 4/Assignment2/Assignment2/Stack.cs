using System;
using System.Collections.Generic;
using System.Text;
using Assignment2;


namespace Assignment2
{
    public class Stack<T>
    {
        public Node<T> Head;
        public int size;

        public Stack()
        {
            Head = default(Node <T>);
            size = 0;
        }

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
                Head.Previous  = Oldhead;
                //this.Head.Previous = Oldhead;
                //Oldhead.Previous = Head;
            }

            size++;
        }

        public T Top()
        {
            if (size == 0)
            {
                throw new ApplicationException("List is empty.");
            }
            return Head.Element;
        }


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


        public void Clear()
        {
            size = 0;
            Head = null;
        }


        public int Size()
        {
            return size;
        }

        public Node<T> getHead()
        {
            return Head;
        }

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



