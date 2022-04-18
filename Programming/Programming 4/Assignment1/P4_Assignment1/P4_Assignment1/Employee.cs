using System;

namespace P4_Assignment1
{
    public class Employee : IComparable<Employee>
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Employee(int employeeID)
        {
            this.EmployeeID = employeeID;
            this.FirstName = null;
            this.LastName = null;
        }

        public Employee(int employeeID, string firstName, string lastName)
        {
            this.EmployeeID = employeeID;
            this.FirstName = firstName;
            this.LastName = lastName;
        }
        
        public int CompareTo(Employee other)
        {
            return this.EmployeeID.CompareTo(other.EmployeeID);
        }

        public override string ToString()
        {
            if (FirstName == null)
            {
                FirstName = "null";
            }

            if (LastName == null)
            {
                LastName = "null";
            }
            
            return EmployeeID + " " + FirstName + " " + LastName;
        }
    }

    public class Node<T>
    {
        public T Element { get; set; }
        public Node<T> Previous { get; set; }
        public Node<T> Next { get; set; }

        public Node()
        {
            this.Element = default(T);
            this.Previous = default(Node<T>);
            this.Next = default(Node<T>);
        }

        public Node(T Element)
        {
            this.Element = Element;

        }

        public Node(T Element, Node<T> Previous, Node<T> Next)
        {

            this.Element = Element;
            this.Previous = Previous;
            this.Next = Next;

        }

    }

    public class LinkedList<T>
    {
        public Node<T> Head { get; set; }
        public Node<T> Tail { get; set; }
        public int Size { get; set; }

        public LinkedList()
        {
            
        }

        public void AddFirst (T Element)
        {
           
             Node<T> firstnode = new Node<T>(Element);

            Node<T> Oldhead = Head;
            Head = firstnode; 
            

            if (this.Size == 0)
            {
                this.Tail = firstnode;
            }
            else
            {
                this.Head.Next = Oldhead;
                Oldhead.Previous= Head;
            }
            Size++;
        }


        public void Clear()
        {

            Size = 0;
            Head = null;
            Tail = null;

        }

        public bool IsEmpty()
        {
            return this.Size == 0;
            //if (this.Size == 0)
            //{
            //    return true;
            //}

            //else
            //{
            //    return false;
            //}
        }

        public Node<T> GetHead()
        {
            return this.Head;
        }

        public Node<T> GetTail()
        {
            return this.Tail;
        }

        public T GetFirst()
        {
            if (IsEmpty())
            {
                throw new ApplicationException("No Head to get");
            }
            else
            {
                return Head.Element;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public T  GetLast()
        {
            if (IsEmpty())
            {
                throw new ApplicationException("No Tail to get");
            }
            else
            {
                return Tail.Element;
            }
            
        }

        public int GetSize()
        {
            return this.Size;
        }

        public T SetFirst(T element)
        {
            if (IsEmpty())
            {
                throw new ApplicationException("List is empty.");
            }
            T replaced = Head.Element; 
            this.Head.Element = element;
            return replaced;
        }

        public T SetLast(T element)
        {
            if (IsEmpty())
            {
                throw new ApplicationException("List is empty");
            }
            T replaced = Tail.Element;
            this.Tail.Element = element;
            return replaced;
        }


        public void AddLast(T Element)
        {
            Node<T> newnode = new Node<T>(Element);

            if (this.Size == 0)
            {
                this.Tail = newnode;
                this.Head = newnode;
            }
            else
            {
                Node <T> nodenewtail = new Node<T>(Element);
                Node<T> currenttail = Tail;

                Tail = nodenewtail;
                Tail.Previous = currenttail;
                currenttail.Next = Tail;
               
            }
            Size++;
        }

        public T RemoveFirst()
        {
            if (IsEmpty())
            {
                throw new ApplicationException("List is empty.");
            }
            T removed = Head.Element;
            if (GetSize() == 1)
            {
                Clear();
                //Head = null;
                //Tail = null;
            }
            else
            {
                Head = Head.Next;
                Head.Previous = null;
                Size--;
            }
          
            return removed;
        }

        public T RemoveLast()
        {
            Node<T> oldtail = null;

            if (IsEmpty())
            {
                throw new ApplicationException("List is empty.");
            }
            oldtail = Tail;
            if (Size == 1)
            {           
                Head = null;
                Tail = null;
            }
            else
            {
                Tail = oldtail.Previous;
                Tail.Next = null;
                
            }
            Size--;
            return oldtail.Element;
        }

        public Node<T> GetNodeByPosition(int position)
        {
            if (position < 0 || position > Size)
            {
                throw new ApplicationException("Position cannot be below zero");
            }
            Node<T> current = Head;
           
            for (int currentpos = 1; currentpos < position; currentpos++)
            {
                current = current.Next;
            }

            return current;
        }

        public T Get(int position)
        {
            Node<T> node = GetNodeByPosition(position);
            return node.Element;
        }


        public T Remove(int position)
        {
            if (position == 0)
            {
                throw new ApplicationException("Cannot remove position 0");
            }

            Node<T> nodeToRemove = GetNodeByPosition(position);
            T oldElement = nodeToRemove.Element;
            
            if (nodeToRemove == Head)
            {
                RemoveFirst();

            }  
            else if (nodeToRemove == Tail)
            {
                RemoveLast();
                
            }
            else
            {
                nodeToRemove.Next.Previous = nodeToRemove.Previous;
                nodeToRemove.Previous.Next = nodeToRemove.Next;

                Size--;
             
            }
          

            return oldElement;
        }

        
        public T Set (T elementtoadd, int position)
        {
         
            if (position <= 0)
            {
                throw new ApplicationException("Cannot set below position 0");
            }

            Node<T> replacedNode = GetNodeByPosition(position);
            T replaced = replacedNode.Element;

            replacedNode.Element = elementtoadd;
            return replaced;
            
        }

        public T AddAfter(T elementtoadd, int position)
        {
            Node<T> existingNode = GetNodeByPosition(position);

            // next and prev set in constructor
            Node<T> newnode = new Node<T>(elementtoadd, existingNode, existingNode.Next);

            if (existingNode != Tail)
            {
                existingNode.Next.Previous = newnode;
            }

            else
            {
                Tail = newnode;
            }

            existingNode.Next = newnode;
            Size++;

            return elementtoadd;

            
        }


        public void AddBefore(T elementtoadd, int position)
        {

            Node<T> existingNode = GetNodeByPosition(position);
            Node<T> newNode = new Node<T>(elementtoadd,existingNode.Previous,existingNode);

            if (existingNode != Head)
            {
                existingNode.Previous.Next = newNode;
            }
            else
            {
                Head = newNode;
            }
            existingNode.Previous = newNode;

            Size++;
            
        }







    }
}
