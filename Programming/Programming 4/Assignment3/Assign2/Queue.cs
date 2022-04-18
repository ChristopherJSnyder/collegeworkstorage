using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assign3
{
    public class Queue<T>
    {
        public Node<T> Head { get; set; }
        public Node<T> Tail { get; set; }
        public int Size { get; set; }


        public Queue()
        {
            Head = default(Node<T>);
            Size = 0;
            Tail = default(Node<T>);
        }

        public void Enqueue(T element)
        {
            Node<T> newnode = new Node<T>(element);

            if (Size == 0)
            {
                Tail = newnode;
                Head = newnode;
            }
            
            else if (Size == 1)
            {
                Head.Next = newnode;
                Tail = newnode;
            }
            else
            {
                Tail.Next = newnode;
                Tail = newnode;

            }

            Size++;
        }


        public T Front()
        {
            if (Size == 0)
            {
                throw new ApplicationException("Queue is empty.");
            }

       
            return Head.Element;
        }


        public T Dequeue()
        {
            if (Size == 0)
            {
                throw new ApplicationException("Queue is empty.");
            }
            T elementtoreturn = Head.Element;
            Head = Head.Next;
            Size--;
            return elementtoreturn;
        }


        public void Clear()
        {
            Size = 0;
            Head = null;
            Tail = null;
        }


        public int GetSize()
        {
            return Size;
        }


        public Node<T> GetHead()
        {
            return Head;
        }


        public Node<T> GetTail()
        {
            return Tail;
        }


        public Boolean IsEmpty()
        {
            if (Size == 0)
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
